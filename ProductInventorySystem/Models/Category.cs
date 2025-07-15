using System.ComponentModel.DataAnnotations;

namespace ProductInventorySystem.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public string CategoryName { get; set; }

    public ICollection<Product>? Products { get; set; }
}
