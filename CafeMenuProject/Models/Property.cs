using System.ComponentModel.DataAnnotations;

namespace CafeMenuProject.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; } // Özellik adı

        [Required]
        [StringLength(255)]
        public string Value { get; set; } // Özellik değeri
    }
}
