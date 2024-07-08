using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIProdutos.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(300)]
    public string Description { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string PathImage { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    // Many-to-Many relationship with Loja
    public ICollection<ProductStore> ProductStores { get; set; }
}
