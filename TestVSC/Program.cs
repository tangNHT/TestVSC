using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop
{
    class Program
    {
        public static void CreateDatabase()
        {
            try
            {
                //Kết nối đến DB
                using (var dbcontext = new OnlineShopDBContext())
                {
                    //Lấy tên DB
                    string databasename = dbcontext.Database.GetDbConnection().Database;

                    Console.WriteLine("Tạo " + databasename);

                    //Thông báo nếu tạo DB thành công và không thành công
                    bool result = dbcontext.Database.EnsureCreated();
                    string resultstring = result ? "tạo thành công" : "đã có trước đó";
                    Console.WriteLine($"CSDL {databasename} : {resultstring}");
                }
            }

            catch (SqlException ex)
                {
                    //Thông báo lỗi khi kết nối DB
                    Console.WriteLine("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                    foreach (SqlError error in ex.Errors)
                    {
                        Console.WriteLine($"Error Code: {error.Number}, Message: {error.Message}");
                    }
                }
        }

        public static void DropDatabase()
        {
            try
            {
                //Kết nối đến DB
                using (var dbcontext = new OnlineShopDBContext())
                {
                    //Lấy tên DB
                    string databasename = dbcontext.Database.GetDbConnection().Database;

                    Console.WriteLine("Xoá " + databasename);

                     //Thông báo nếu xoá DB thành công và không thành công
                    bool result = dbcontext.Database.EnsureDeleted();
                    string resultstring = result ? "xoá thành công" : "không xoá được";
                    Console.WriteLine($"CSDL {databasename} : {resultstring}");
                }
            }

            catch (SqlException ex)
                {
                    //Thông báo lỗi khi kết nối DB
                    Console.WriteLine("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                    foreach (SqlError error in ex.Errors)
                    {
                        Console.WriteLine($"Error Code: {error.Number}, Message: {error.Message}");
                    }
                }
        }

        public static void UpdateDatabase()
        {
            //Kết nối đến DB
            using (var dbcontext = new OnlineShopDBContext())
                {
                    //Lấy tên DB
                    string databasename = dbcontext.Database.GetDbConnection().Database;

                    Console.WriteLine("Cập nhật " + databasename);

                     //Thông báo nếu xoá DB thành công và không thành công
                    try
                    {
                        // Áp dụng các migration để cập nhật DB
                        dbcontext.Database.Migrate();
                        Console.WriteLine($"CSDL {databasename} : cập nhật thành công");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"CSDL {databasename} : không cập nhật được. Lỗi: {ex.Message}");
                    }
                }
        }
        
        #region Tam bo
        public static async Task InsertProduct ()
        {
            using var dbcontext = new OnlineShopDBContext();
            
            #region Them nhieu san pham
            var newProducts = new object[]{
                new Product() {ProductName = "San pham 2", Price = 1500},
                new Product() {ProductName = "San pham 3", Price = 1500},
                new Product() {ProductName = "San pham 4", Price = 1500},
            };
            #endregion

            #region Them mot san pham
            var newOneProduct = new Product();
            newOneProduct.ProductName = "Sản phẩm 1";
            newOneProduct.Price = 1400;
            #endregion

            //Thêm vào bảng Products một mảng sản phẩm
            dbcontext.AddRange(newProducts);

            //Thêm vào bảng Products một sản phẩm
            dbcontext.Add(newOneProduct);
            int number_rows = await dbcontext.SaveChangesAsync();
            Console.WriteLine($"Đã chèn dòng {number_rows}");
        }

        public static void ReadProduct()
        {
            using var dbcontext = new OnlineShopDBContext();

            //Hiển thị tất cả sản phẩm trong bảng Products
            var products = dbcontext.products.ToList();
            products.ForEach(product => product.PrintInfo());
        }

        public static async Task UpdateProduct (int id, Product product)
        {
            using var dbcontext = new OnlineShopDBContext();

            //Tìm kiếm sản phẩm theo Id
            var findProduct = (from x in dbcontext.products 
                                where x.ProductID == id
                                select x).FirstOrDefault();
            
            if (findProduct != null)
            {
                //Tìm thấy sản phẩm thì sẽ cập nhật
                findProduct.ProductName = product.ProductName;
                findProduct.Price = product.Price;
                int number_rows = await dbcontext.SaveChangesAsync();
                Console.WriteLine($"Đã cập nhật {number_rows} dòng");
            }
            else
                Console.WriteLine($"Đã cập nhật không thành công");
        }

        public static async Task DeleteProduct (int id)
        {
            using var dbcontext = new OnlineShopDBContext();

            //Tìm kiếm sản phẩm theo Id
            var findProduct = (from x in dbcontext.products 
                                where x.ProductID == id
                                select x).FirstOrDefault();
            if (findProduct != null)
            {
                //Tìm thấy sản phẩm thì xoá
                dbcontext.Remove(findProduct);
                int number_rows = await dbcontext.SaveChangesAsync();
                Console.WriteLine($"Đã xoá {number_rows} dòng");
            }
            else
                Console.WriteLine($"Xoá không thành công");
        }
        #endregion

        public static void InsertCategory(Category category)
        {
            using var dbcontext = new OnlineShopDBContext();
            dbcontext.categories.Add(category);
            dbcontext.SaveChanges();
        }

        public static void InsertProduct(Product product)
        {
            using var dbcontext = new OnlineShopDBContext();
            dbcontext.products.Add(product);
            dbcontext.SaveChanges();
        }
        
        static void Main(string[] args)
        {
            //UpdateDatabase();

            // var dbcontext = new OnlineShopDBContext();
            // var result = from p in dbcontext.products
            //             join c in dbcontext.categories on p.ProductID equals c.CategoryID
            //             select new {
            //                 ten = p.ProductName,
            //                 danhmuc = c.Name,
            //                 gia = p.Price
            //             };
            // result.ToList().ForEach(a => Console.WriteLine(a));

            // var products = from p in dbcontext.products
            //                 where p.ProductName.Contains("o")
            //                 orderby p.Price descending
            //                 select p;
            // products.ToList().ForEach(p => p.PrintInfo());

            DropDatabase();
            CreateDatabase();

            //await InsertProduct();
            //await ReadProduct();

            // Product product = new Product{ ProductName = "Laptop", Price = 1600,};
            // await UpdateProduct(2, product);

            //await DeleteProduct(3);

            // Category category1 = new Category(){Name = "Dien thoai", Description = "Cac loai dien thoai"};
            // Category category2 = new Category(){Name = "Do uong", Description = "Cac loai do uong"};
            // InsertCategory(category1);
            // InsertCategory(category2);

            // Product product1 = new Product(){ProductName = "Iphone", Price = 1000000, CateId = 1};
            // Product product2 = new Product(){ProductName = "SamSung", Price = 1000000, CateId = 1};
            // Product product3 = new Product(){ProductName = "Ruou vang", Price = 500000, CateId = 2};
            // InsertProduct(product1);
            // InsertProduct(product2);
            // InsertProduct(product3);

            //ReadProduct();
        }
    }
}