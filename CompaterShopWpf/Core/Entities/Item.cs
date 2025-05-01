using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CompaterShopWpf.Core.Entities;

public class Item
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get;  set; } = string.Empty;
    public decimal Price { get; set; } = decimal.Zero;
    public long CategoryId { get;  set; } 
    public Category? Category { get;  set; }
    public List<OrderItem> Orders { get; set; } = [];

}