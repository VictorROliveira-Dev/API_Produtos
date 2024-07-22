using System.ComponentModel.DataAnnotations;

namespace APIProdutos.Data.Dtos;

public class ProductDto
{
    public Guid ProductId { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(300)]
    public string Description { get; set; }

    public int Amount { get; set; }

    public decimal Price { get; set; }

    public IFormFile Image { get; set; }

    public int CategoryId { get; set; }
}