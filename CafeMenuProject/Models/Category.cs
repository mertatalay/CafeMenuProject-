using System.ComponentModel.DataAnnotations;

namespace CafeMenuProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

       
        public string?  CategoryName { get; set; }

        public int? ParentCategoryId { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public int? CreatorUserId { get; set; }
    }
}
