using Microsoft.AspNetCore.Mvc;
using CafeMenuProject.Data;
using CafeMenuProject.Models;
using System.Linq;

namespace CafeMenuProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kategori listeleme
        public IActionResult Index()
        {
            var categories = _context.CATEGORY.ToList();
            return View(categories);
        }

        // Kategori detay sayfası
        public IActionResult Details(int id)
        {
            var category = _context.CATEGORY.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Kategori ekleme formu
        public IActionResult Create()
        {
            return View();
        }

        // Kategori ekleme işlemi
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.CATEGORY.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // Kategori silme işlemi
        public IActionResult Delete(int id)
        {
            var category = _context.CATEGORY.Find(id);
            if (category != null)
            {
                // Önce bu kategoriye ait ürünleri sil
                var products = _context.PRODUCT.Where(p => p.CategoryId == id).ToList();
                _context.PRODUCT.RemoveRange(products);

                // Şimdi kategoriyi sil
                _context.CATEGORY.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
