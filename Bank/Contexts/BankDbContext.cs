using Bank.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bank.Contexts
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }
       


        public DbSet<CurrencyExchange> CurrencyExchanges { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }

}
