using System.ComponentModel.DataAnnotations;

namespace APIProdutos.Data.Dtos;

public class CategoryDto
{
    public int CategoryId { get; set; }

    [StringLength(100)]
    public string CategoryName { get; set; }

    [StringLength(300)]
    public string Description { get; set; }
}
