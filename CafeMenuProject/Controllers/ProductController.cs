using CafeMenuProject.Data;
using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CafeMenuProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // 📌 Ürün Listeleme (Sadece aktif ürünler gösterilecek)
        public IActionResult Index()
        {
            var products = _context.PRODUCT
         .Where(p => !p.IsDeleted) // Sadece silinmemiş ürünleri listele
         .Include(p => p.Category) // Kategori bilgilerini de al
         .ToList();
            return View(products);
        }

        // 📌 Ürün Detay Sayfası
        public IActionResult Details(int id)
        {
            var product = _context.PRODUCT
                .Include(p => p.Category)
                .FirstOrDefault(c => c.ProductId == id && !c.IsDeleted);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // 📌 Ürün ekleme formu
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.CATEGORY, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.CATEGORY, "CategoryId", "CategoryName");
                TempData["ErrorMessage"] = "Lütfen tüm alanları doldurun!";
                return View(product);
            }

            try
            {
                _context.PRODUCT.Add(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Ürün başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hata oluştu: " + ex.Message;
            }

            ViewBag.Categories = new SelectList(_context.CATEGORY, "CategoryId", "CategoryName");
            return View(product);
        }


        // 📌 Ürün Silme İşlemi (Soft Delete)
        public IActionResult Delete(int id)
        {
            var product = _context.PRODUCT.Find(id);
            if (product != null)
            {
                product.IsDeleted = true; // Ürünü soft delete olarak işaretle
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Ürün bulunamadı.";
            }

            return RedirectToAction("Index");
        }
    }
}
