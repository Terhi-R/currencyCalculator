using Microsoft.EntityFrameworkCore;
using currencyCalculator.Business.Models;
using Microsoft.Extensions.Configuration;
using currencyCalculator.App;

public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                                .AddUserSecrets<ApplicationDbContext>()
                                .Build();

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }
        public DbSet<currencyCalculator.Business.Models.CurrencyRate> CurrencyRate { get; set; }
    }
