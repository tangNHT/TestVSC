using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace OnlineShop
{
    public class OnlineShopDBContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(bulder => {
            bulder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            //bulder.AddFilter(DbLoggerCategory.Database.Name, LogLevel.Information);
            bulder.AddConsole();
        });
        public DbSet<Product> products {set; get;}
        public DbSet<Category> categories {set; get;}

        private const string connectionString = @"
                Data Source=DESKTOP-AF7B9H9\ARCTECDATABASE2,1433;
                Initial Catalog=OnlineShopDatabase;
                User ID=sa;Password=administrator;
                Integrated Security=True;
                Encrypt=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}