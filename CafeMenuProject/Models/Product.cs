using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeMenuProject.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(200)]
        public string? ProductName { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public decimal? PRICE { get; set; }

        public string? ImagePath { get; set; } // Ürün resmi yolu

        public bool? IsDeleted { get; set; } = false;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public int? CreatorUserId { get; set; }
    }
}
