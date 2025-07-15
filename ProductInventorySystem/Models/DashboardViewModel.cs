using ProductInventorySystem.Models;
using System.Collections.Generic;

namespace ProductInventorySystem.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
    }
}