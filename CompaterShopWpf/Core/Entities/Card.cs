namespace CompaterShopWpf.Core.Entities;

public class Card
{
    public long Id { get; set; }
    public string CardNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public long UserId { get; set; }
    public User User { get; set; }
    
}