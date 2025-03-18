using System.ComponentModel.DataAnnotations;


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

    public List<Item> Items { get; set; } = [];
}