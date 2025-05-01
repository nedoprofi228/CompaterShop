using System.ComponentModel.DataAnnotations;

namespace CompaterShopWpf.Core.Entities;

public class OrderItem
{
    [Key]
    public long Id { get; set; }

    public int Count { get; set; }
    
    public long OrderId { get; set; }
    public Order Order { get; set; }
    
    public long ItemId { get; set; }
    public Item Item { get; set; }
    
    
}