using System.ComponentModel.DataAnnotations;

namespace OnlineShop
{
    public class CategoryDetail
    {
        [Key]
        public int CategoryDetailId {get; set;}
        public int UserId {get; set;}
        public DateTime Created {get; set;}
        public DateTime Updated {get; set;}
        public int CountProduct {get; set;}

        public Category category {get; set;}
    }
}