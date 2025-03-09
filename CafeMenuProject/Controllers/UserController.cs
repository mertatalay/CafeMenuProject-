using Microsoft.AspNetCore.Mvc;
using CafeMenuProject.Data;
using CafeMenuProject.Models;
using CafeMenuProject.Helpers;
using System.Linq;
using System;

namespace CafeMenuProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                // **Girdi Kontrolleri**
                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    ModelState.AddModelError("Username", "Kullanıcı adı boş olamaz.");
                }

                if (string.IsNullOrWhiteSpace(user.HashPassword) || user.HashPassword.Length < 6)
                {
                    ModelState.AddModelError("HashPassword", "Şifre en az 6 karakter olmalıdır.");
                }

                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Bu kullanıcı adı zaten alınmış!");
                }

                // Eğer doğrulama hataları varsa, tekrar formu göster
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                // **Salt ve Hash Üretme**
                try
                {
                    var salt = PasswordHelper.GenerateSalt();
                    var hashedPassword = PasswordHelper.HashPassword(user.HashPassword, salt);

                    user.SaltPassword = Convert.ToBase64String(salt); // **Gerçek Salt**
                    user.HashPassword = Convert.ToBase64String(hashedPassword);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Salt veya Hash üretirken hata oluştu: {ex.Message}");

                    // **Sorunun kaynağını anlamak için geçici çözüm:**
                    user.SaltPassword = Convert.ToBase64String(new byte[16]); // **Boş Salt Atama**
                }

                // **Kullanıcıyı Kaydet**
                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Kullanıcı başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Kullanıcı eklenirken hata oluştu: {ex.Message}");
                ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin.");
                return View(user);
            }
        }

    }
}
