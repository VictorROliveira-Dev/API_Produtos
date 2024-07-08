using System.ComponentModel.DataAnnotations;

namespace APIProdutos.Models;

public class Store
{
    [Key]
    public int StoreId { get; set; }

    [Required]
    [MaxLength(100)]
    public string StoreName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Address { get; set; }

    // Many-to-Many relationship with Product
    public ICollection<ProductStore> ProductStores { get; set; }
}
