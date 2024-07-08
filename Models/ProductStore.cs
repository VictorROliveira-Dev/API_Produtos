namespace APIProdutos.Models;

public class ProductStore
{
    public int StoreId { get; set; }
    public Store Store { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
