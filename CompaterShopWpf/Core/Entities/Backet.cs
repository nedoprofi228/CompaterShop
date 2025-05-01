using System.Windows.Documents;


namespace CompaterShopWpf.Core.Entities;

public class Backet
{
    public List<ItemDTO>? ItemsDtos { get; set; } = [];

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var itemDto in ItemsDtos)
                totalPrice += itemDto.Item.Price * itemDto.Count;
            
            return totalPrice;
        }
    }

}