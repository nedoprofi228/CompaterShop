using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CompaterShopWpf.Core.Entities;

public class User
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public List<Card> Cards { get; set; } = [];
    public List<Order> OrderHistory { get; set; } = [];
    
    [NotMapped]
    public Backet Backet { get; set; } = new Backet();
    
    
}