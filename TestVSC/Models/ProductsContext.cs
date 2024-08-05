using Microsoft.EntityFrameworkCore;

namespace TestVSC
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> products {set; get;}

        private const string connectionString = @"
                Data Source=DESKTOP-AF7B9H9\ARCTECDATABASE2,1433;
                Initial Catalog=mydata;
                User ID=sa;Password=administrator;
                Integrated Security=True;
                Encrypt=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}