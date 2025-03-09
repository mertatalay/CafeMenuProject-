using System;
using System.Security.Cryptography;
using System.Text;

namespace CafeMenuProject.Helpers
{
    public static class PasswordHelper
    {
        // **Salt Üretme Fonksiyonu**
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; // 16 byte uzunluğunda salt üret
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // **Şifreyi Hashleme Fonksiyonu**
        public static byte[] HashPassword(string password, byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = salt; // HMACSHA512'ye salt'ı key olarak veriyoruz
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // **Şifre Doğrulama Fonksiyonu**
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            byte[] hashBytes = HashPassword(enteredPassword, saltBytes);

            return Convert.ToBase64String(hashBytes) == storedHash;
        }
    }
}
