using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID {set; get;}

        [Required]
        [StringLength(50)]
        [Column("Tensanpham", TypeName = "ntext")]
        public string? ProductName {set; get;}
        [Column(TypeName = "money")]
        public decimal Price {set; get;}

        public int CateId {get; set;}
        //Foreign Key
        [ForeignKey("CateId")]
        //[Required]
        public Category? Category {get; set;}   //FK -> PK

        //public int CateId2 {get; set;}
        //Foreign Key
        // [ForeignKey("CateId2")]
        // //[Required]
        // public Category? Category2 {get; set;}   //FK -> PK

        //Hiển thị thông tin của bảng Products
        public void PrintInfo() => 
        Console.WriteLine($"{ProductID} - {ProductName} - {Price}");
    }
}