using Microsoft.AspNetCore.Mvc;
using ProductInventorySystem.Repositories;
using ProductInventorySystem.Models;
using ProductInventorySystem.ViewModels;

namespace ProductInventorySystem.MVCControllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                Products = await _productRepository.GetAllAsync(),
                Categories = await _categoryRepository.GetAllAsync()
            };
            return View(viewModel);
        }
    }
}
