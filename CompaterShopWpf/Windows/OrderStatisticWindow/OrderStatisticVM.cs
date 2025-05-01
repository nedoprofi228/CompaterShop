using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CompaterShopWpf.Windows.OrderStatisticWindow;

public class OrderStatisticVM : INotifyPropertyChanged
{
    private ApplicationContext _dbContext;
    private List<Order> allOrders;

    private List<string> _periods = new List<string>
    {
        "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь",
        "декабрь", "год"
    };

    public List<string> Periods => _periods;

    private string _selectedPeriod;

    public string SelectedPeriod
    {
        get => _selectedPeriod;
        set
        {
            _selectedPeriod = value;
            OnPropertyChanged();
            UpdateFilteredOrders();
            UpdatePieChartData();
        }
    }

    private ObservableCollection<OrderViewModel> _filteredOrders = new ObservableCollection<OrderViewModel>();
    public ObservableCollection<OrderViewModel> FilteredOrders => _filteredOrders;

    private List<string> _diagramDataTypes = new List<string> { "выручка", "кол-во проданных товаров" };
    public List<string> DiagramDataTypes => _diagramDataTypes;

    private string _selectedDiagramDataType;

    public string SelectedDiagramDataType
    {
        get => _selectedDiagramDataType;
        set
        {
            _selectedDiagramDataType = value;
            OnPropertyChanged();
            UpdatePieChartData();
        }
    }

    private ObservableCollection<PieChartItem> _pieChartData = new ObservableCollection<PieChartItem>();
    public ObservableCollection<PieChartItem> PieChartData => _pieChartData;

    public OrderStatisticVM()
    {
        _dbContext = ApplicationContext.GetInstance();
        allOrders = _dbContext.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.User) // Загружаем данные пользователя
            .ToList();

        foreach (var order in allOrders)
        {
            var items = _dbContext.OrderItems
                .Where(o => o.OrderId == order.Id)
                .Include(o => o.Item)
                .Select( oi => new ItemDTO(){Item = oi.Item, Count = oi.Count});
            
            order.Items = items;
        }
        
        SelectedPeriod = "год";
        SelectedDiagramDataType = "выручка";
    }

    private void UpdateFilteredOrders()
    {
        var (start, end) = GetDateRange(SelectedPeriod);
        var filtered = allOrders
            .Where(o => o.OrderDate >= start && o.OrderDate <= end)
            .Select(o => new OrderViewModel
            {
                OrderDate = o.OrderDate,
                TotalPrice = o.Items.Sum(i => i.Item.Price), // Сумма цен товаров
                ItemsString = string.Join(", ", o.Items.Select(i => $"{i.Count} {i.Item.Name}")), // Список товаров
                UserName = o.User?.Name ?? "Неизвестный" // Имя пользователя или заглушка
            })
            .ToList();

        _filteredOrders.Clear();
        foreach (var order in filtered)
        {
            _filteredOrders.Add(order);
        }
    }

    private void UpdatePieChartData()
    {
        var allItems = allOrders
            .Where(o => FilteredOrders.Any(fo => fo.OrderDate == o.OrderDate))
            .SelectMany(o => o.Items);
        var grouped = allItems.GroupBy(i => i.Item.Id);

        _pieChartData.Clear();

        if (SelectedDiagramDataType == "кол-во проданных товаров")
        {
            var data = grouped.Select(g => new PieChartItem
            {
                ItemName = g.First().Item.Name, Value = g.Sum(i => i.Count)
            });
            
            foreach (var item in data)
            {
                _pieChartData.Add(item);
            }
        }
        else if (SelectedDiagramDataType == "выручка")
        {
            var data = grouped.Select(g => new PieChartItem
                { ItemName = g.First().Item.Name, Value = (double)(g.Sum(i => i.Count) * g.First().Item.Price) });
            foreach (var item in data)
            {
                _pieChartData.Add(item);
            }
        }
    }

    private (DateTime start, DateTime end) GetDateRange(string period)
    {
        int year = DateTime.Now.Year;
        int month;
        switch (period)
        {
            case "январь": month = 1; break;
            case "февраль": month = 2; break;
            case "март": month = 3; break;
            case "апрель": month = 4; break;
            case "май": month = 5; break;
            case "июнь": month = 6; break;
            case "июль": month = 7; break;
            case "август": month = 8; break;
            case "сентябрь": month = 9; break;
            case "октябрь": month = 10; break;
            case "ноябрь": month = 11; break;
            case "декабрь": month = 12; break;
            case "год": return (new DateTime(year, 1, 1), new DateTime(year, 12, 31));
            default: throw new ArgumentException("Invalid period");
        }

        var start = new DateTime(year, month, 1);
        var end = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        return (start, end);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class OrderViewModel
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string ItemsString { get; set; }
    public string UserName { get; set; }
}

public class PieChartItem
{
    public string ItemName { get; set; }
    public double Value { get; set; }
}