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
        public DbSet<CategoryDetail> categoryDetails {get; set;}

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            //var entity = modelBuilder.Entity(typeof(Product));
            // Sử dụng Entity để gọi các Fluent API cho đối tượng Product

            //var entity = modelBuilder.Entity<Product>();

            modelBuilder.Entity<Product>(entity => {
                // Table mapping
                entity.ToTable("Product"); // Tên bảng
                // PK
                entity.HasKey(p => p.ProductID); // Khoá chính

                // Index
                entity.HasIndex(p => p.Price).HasDatabaseName("index-Product-price");

                // Relative
                entity.HasOne(p => p.Category)  // Cột
                    .WithMany()
                    .HasForeignKey("CateId")    // Tên khoá ngoài
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(p => p.Category2)
                    .WithMany(c => c.Products)
                    .HasForeignKey("CateId2")
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(p => p.ProductName)
                    .HasColumnName("TenSanPham")    // Tên cột
                    .HasColumnType("nvarchar")      // Kiểu dữ liệu
                    .HasMaxLength(60)               // Độ dài
                    .IsRequired(true)               // not null
                    .HasDefaultValue("Ten san pham");// Giá trị mặc định
            });

            modelBuilder.Entity<CategoryDetail>(entity => {
                entity.ToTable("CategoryDetail")
                    .HasOne(c => c.category)
                    .WithOne(d => d.categoryDetail)
                    .HasForeignKey<CategoryDetail>(c => c.CategoryDetailId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}