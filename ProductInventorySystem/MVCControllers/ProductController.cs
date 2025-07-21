using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductInventorySystem.Models;
using ProductInventorySystem.Repositories;

namespace ProductInventorySystem.MVCControllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreatePartial()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");
            return PartialView("_CreatePartial", new Product());
        }
        [HttpPost]
        public async Task<IActionResult> CreatePartial(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return Json(new { success = true });
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");
            return PartialView("_CreatePartial", product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName");
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            ViewBag.CategoryName = category?.CategoryName ?? "Unknown";

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> EditPartial(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return PartialView("_EditPartial", product);
        }

        [HttpPost]
        public async Task<IActionResult> EditPartial(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return Json(new { success = true });
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            return PartialView("_EditPartial", product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DetailsPartial(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return PartialView("_DetailsPartial", product);
        }

        [HttpGet]
        public async Task<IActionResult> GetTablePartial()
        {
            var products = await _productRepository.GetAllAsync();
            return PartialView("_ProductTablePartial", products);
        }
    }
}
