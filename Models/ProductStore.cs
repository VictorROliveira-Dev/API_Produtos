namespace APIProdutos.Models;

public class ProductStore
{
    public Guid StoreId { get; set; }
    public Store Store { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
