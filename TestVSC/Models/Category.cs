using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryID {set; get;}
        [Required]
        [StringLength(100)]
        public string? Name {set; get;}
        [Column(TypeName = "ntext")]
        public string? Description {set; get;}

        public virtual List<Product> Products {set; get;}

        public CategoryDetail categoryDetail {set; get;}
    }
}