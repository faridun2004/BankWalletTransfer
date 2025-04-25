using System.Collections.Concurrent;

namespace Bank.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        public static readonly ConcurrentDictionary<string, ConcurrentQueue<DateTime>>
            RequestTimes = new ConcurrentDictionary<string, ConcurrentQueue<DateTime>>();
        private static readonly TimeSpan TimeWindow = TimeSpan.FromMinutes(1);
        public static readonly int MaxRequests = 20;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (ipAddress == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid request.");
                return;
            }

            var currentTime = DateTime.UtcNow;
            var requestQueue = RequestTimes.GetOrAdd(ipAddress, new ConcurrentQueue<DateTime>());

            while (requestQueue.TryPeek(out var oldestTime) && currentTime - oldestTime > TimeWindow)
            {
                requestQueue.TryDequeue(out _);
            }
            if (requestQueue.Count >= MaxRequests)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            requestQueue.Enqueue(currentTime);

            await _next(context);
        }
    }
}
