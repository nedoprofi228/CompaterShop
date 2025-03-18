using System.Windows.Documents;


namespace CompaterShopWpf.Core.Entities;

public class Backet
{
    public List<Item>? Items { get; set; } = [];

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var item in Items)
                totalPrice += item.Price;
            
            return totalPrice;
        }
    }

}