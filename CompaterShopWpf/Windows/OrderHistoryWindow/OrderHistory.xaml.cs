using System.Windows;

namespace CompaterShopWpf.Windows.OrderHistoryWindow;

public partial class OrderHistory : Window
{
    public OrderHistory(long userId)
    {
        OrderHistoryViewModel viewModel = new OrderHistoryViewModel(userId);
        InitializeComponent();
        SetDataContext(viewModel);
    }

    public OrderHistory()
    {
        OrderHistoryViewModel viewModel = new OrderHistoryViewModel();
        InitializeComponent();
        SetDataContext(viewModel);
    }

    private void SetDataContext(OrderHistoryViewModel viewModel)
    {
        DataContext = viewModel;
        if(viewModel.Orders.Count != 0)
            EmptyBacketMsg.Visibility = Visibility.Collapsed;
    }
}