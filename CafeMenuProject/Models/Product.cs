using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeMenuProject.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal PRICE { get; set; }

        [Required]
        public int CategoryId { get; set; }

      

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
