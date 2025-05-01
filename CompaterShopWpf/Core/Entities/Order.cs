using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CompaterShopWpf.Core.Entities;

public class Order
{
    [Key]
    public long Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal OrderPrice { get; set; } = decimal.Zero;
    
    public long UserId { get; set; } 
    public User User { get; set; }
    
    public long CardId { get; set; }
    public Card Card { get; set; }
    
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    [NotMapped]
    public IEnumerable<ItemDTO> Items { get; set; } = [];
}