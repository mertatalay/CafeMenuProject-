using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CafeMenuProject.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ad gereklidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string HashPassword { get; set; }

        [BindNever] // Formdan gelmesini engeller, kod içinde atarız.
        public string SaltPassword { get; set; }
    }
}
