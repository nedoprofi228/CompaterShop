using System.Windows;

namespace CompaterShopWpf.Windows.OrderStatisticWindow;

public partial class OrderStatistic : Window
{
    private OrderStatisticVM _orderStatisticVM { get; set; }
    public OrderStatistic()
    {
        InitializeComponent();
        _orderStatisticVM = new OrderStatisticVM();
        DataContext = _orderStatisticVM;
    }
}