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

namespace TestVSC
{
    class Program
    {
        public static async Task CreateDatabase()
        {
            try
            {
                //Kết nối đến DB
                using (var dbcontext = new ProductDBContext())
                {
                    //Lấy tên DB
                    string databasename = dbcontext.Database.GetDbConnection().Database;

                    Console.WriteLine("Tạo " + databasename);

                    //Thông báo nếu tạo DB thành công và không thành công
                    bool result = await dbcontext.Database.EnsureCreatedAsync();
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

        public static async Task DropDatabase()
        {
            try
            {
                //Kết nối đến DB
                using (var dbcontext = new ProductDBContext())
                {
                    //Lấy tên DB
                    string databasename = dbcontext.Database.GetDbConnection().Database;

                    Console.WriteLine("Xoá " + databasename);

                     //Thông báo nếu xoá DB thành công và không thành công
                    bool result = await dbcontext.Database.EnsureDeletedAsync();
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

        public static async Task InsertProduct ()
        {
            using var dbcontext = new ProductDBContext();
            
            #region Them nhieu san pham
            var newProduct = new object[]{
                new Product() {ProductName = "San pham 2", Provider = "Cong ty A"},
                new Product() {ProductName = "San pham 3", Provider = "Cong ty B"},
                new Product() {ProductName = "San pham 4", Provider = "Cong ty C"},
            };
            #endregion

            #region Them moi mot san pham
            var newOneProduct = new Product();
            newOneProduct.ProductName = "Sản phẩm 1";
            newOneProduct.Provider = "Công ty 1";
            #endregion

            dbcontext.AddRange(newProduct);
            int number_rows = await dbcontext.SaveChangesAsync();
            Console.WriteLine($"Đã chèn dòng {number_rows}");
        }

        public static async Task ReadProduct ()
        {
            using var dbcontext = new ProductDBContext();

            var products = await dbcontext.products.ToListAsync();
            products.ForEach(product => product.PrintInfo());
        }

        public static async Task UpdateProduct (int id, Product product)
        {
            using var dbcontext = new ProductDBContext();

            var findProduct = (from x in dbcontext.products 
                                where x.ProductID == id
                                select x).FirstOrDefault();
            if (findProduct != null)
            {
                findProduct.ProductName = product.ProductName;
                findProduct.Provider = product.Provider;
                int number_rows = await dbcontext.SaveChangesAsync();
                Console.WriteLine($"Đã cập nhật {number_rows} dòng");
            }
            else
                Console.WriteLine($"Đã cập nhật không thành công");
        }

        public static async Task DeleteProduct (int id)
        {
            using var dbcontext = new ProductDBContext();

            var findProduct = (from x in dbcontext.products 
                                where x.ProductID == id
                                select x).FirstOrDefault();
            if (findProduct != null)
            {
                dbcontext.Remove(findProduct);
                int number_rows = await dbcontext.SaveChangesAsync();
                Console.WriteLine($"Đã xoá {number_rows} dòng");
            }
            else
                Console.WriteLine($"Xoá không thành công");
        }

        static async Task Main(string[] args)
        {
            //await DropDatabase();
            //await CreateDatabase();
            
            //await InsertProduct();
            await ReadProduct();

            // Product product = new Product{ ProductName = "Laptop", Provider = "Công ty I",};
            // await UpdateProduct(2, product);

            //await DeleteProduct(3);
        }
    }
}