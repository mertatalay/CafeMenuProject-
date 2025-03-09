using Microsoft.AspNetCore.Mvc;
using CafeMenuProject.Data;
using CafeMenuProject.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CafeMenuProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            // Kategoriye göre ürün sayısı
            var categoryCounts = _context.PRODUCT
                .GroupBy(p => p.Category.CategoryName)
                .Select(g => new { CategoryName = g.Key, Count = g.Count() })
                .ToList();

            // Toplam kullanıcı sayısı
            var totalUsers = _context.Users.Count();

            // Toplam kategori sayısı
            var totalCategories = _context.CATEGORY.Count();

            // Son eklenen 5 ürün
            var recentProducts = _context.PRODUCT
                .OrderByDescending(p => p.CreatedDate)
                .Take(5)
                .ToList();

            // Döviz kuru API'sinden veri çekme
            string exchangeRate = await GetExchangeRate();

            // ViewBag değerlerini NULL kontrolü ile atama
            ViewBag.CategoryCounts = categoryCounts.Any() ? categoryCounts : null;
            ViewBag.TotalUsers = totalUsers > 0 ? totalUsers : 0;
            ViewBag.TotalCategories = totalCategories > 0 ? totalCategories : 0;
            ViewBag.RecentProducts = recentProducts.Any() ? recentProducts : null;
            ViewBag.ExchangeRate = !string.IsNullOrEmpty(exchangeRate) ? exchangeRate : "Güncellenemedi";

            return View();
        }

        // Döviz kuru API çağrısı
        private async Task<string> GetExchangeRate()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Alternatif döviz kuru API'si
                    var response = await client.GetStringAsync("https://api.exchangerate-api.com/v4/latest/USD");
                    var json = JObject.Parse(response);
                    return json["rates"]["TRY"].ToString(); // ✅ USD/TRY kuru
                }
                catch
                {
                    return "Güncellenemedi";
                }
            }
        }
    }
}
