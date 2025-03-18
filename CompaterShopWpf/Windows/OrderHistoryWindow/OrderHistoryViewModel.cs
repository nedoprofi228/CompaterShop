using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CompaterShopWpf.Core.Entities;

namespace CompaterShopWpf.Windows.OrderHistoryWindow;

public class OrderHistoryViewModel
{
    OrderHistoryModel _orderHistoryModel = new OrderHistoryModel();
    private Order _selectedOrder;

    public ObservableCollection<Order> Orders { get; set; }

    public Order SelectedOrder
    {
        get => _selectedOrder;
        set
        {
            _selectedOrder = value;
            OnPropertyChanged();
        }
    }

    public OrderHistoryViewModel(long userId)
    {
        Orders = new ObservableCollection<Order>(_orderHistoryModel.GetOrdersHistoryByUserId(userId));
    }

    public OrderHistoryViewModel()
    {
        Orders = new ObservableCollection<Order>(_orderHistoryModel.GetOrdersHistory());
    }
    

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}