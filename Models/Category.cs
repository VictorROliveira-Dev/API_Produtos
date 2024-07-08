using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace APIProdutos.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; }

    [Required]
    [MaxLength(300)]
    public string Description { get; set; }

    // One-to-Many relationship with Product
    public ICollection<Product> Products { get; set; }
}
