using System.Windows;
using CompaterShopWpf.Core.Entities;

namespace CompaterShopWpf.Windows.ItemWindow;

public partial class ItemInfo : Window
{
    public ItemInfoViewModel _ItemInfoViewModel;
    public ItemInfo(Item item)
    {
        _ItemInfoViewModel = new ItemInfoViewModel(item);
        InitializeComponent();
        DataContext = _ItemInfoViewModel;
    }
}