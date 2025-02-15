using BankUI.Models;

namespace BankUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserDto>> GetAllProducts()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("api/products");
        }

        public async Task<UserDto> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/products/{id}");
        }

        public async Task AddProduct(UserDto product, MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync("api/products", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProduct(UserDto product, MultipartFormDataContent content)
        {
            var response = await _httpClient.PutAsync($"api/products/{product.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
