using System.ComponentModel.DataAnnotations;


namespace CompaterShopWpf.Core.Entities;

public class Category
{
    [Key]
    public long Id { get; set; }
    public string CategoryName { get; set; } = String.Empty;
    
    public long CatalogId { get; set; }
    public Catalog Catalog { get; set; }
    public List<Item> Items { get; set; } = [];
}