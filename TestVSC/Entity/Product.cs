using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestVSC
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID {set; get;}

        [Required]
        [StringLength(50)]
        public string? Name {set; get;}

        [StringLength(50)]
        public string? Provider {set; get;}
    }
}