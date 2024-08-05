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
        public static void CreateDatabase()
        {
            try
            {
                //Kết nối đến DB
                using (var dbcontext = new ProductsContext())
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
                using (var dbcontext = new ProductsContext())
                {
                    //Lấy tên DB
                    string databasename = dbcontext.Database.GetDbConnection().Database;

                    Console.WriteLine("Xoá " + databasename);

                     //Thông báo nếu xoá DB thành công và không thành công
                    bool result = dbcontext.Database.EnsureDeleted();
                    string resultstring = result ? "xoá thành công" : "khônh xoá được";
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

        static void Main(string[] args)
        {
            //CreateDatabase();
            //DropDatabase();
        }
    }
}