using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInventorySystem.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Column (TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
