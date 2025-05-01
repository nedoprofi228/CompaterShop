using System.Collections.ObjectModel;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CompaterShopWpf.Windows.OrderHistoryWindow;

public class OrderHistoryModel
{
    ApplicationContext _dbContext = ApplicationContext.GetInstance();
    
    public ObservableCollection<Order> GetOrdersHistory()
    {
        return new ObservableCollection<Order>(_dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Card)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Item)
            .Include(o => o.OrderItems));
    }

    public ObservableCollection<Order> GetOrdersHistoryByUserId(long userId)
    {
        return new ObservableCollection<Order>(_dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Card)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Item)
            .Where(o => o.UserId == userId));
    }

    
}