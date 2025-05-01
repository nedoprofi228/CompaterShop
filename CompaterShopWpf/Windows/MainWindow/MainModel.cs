using System.Collections.ObjectModel;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using CompaterShopWpf.Windows.AdminMenuWindow;
using CompaterShopWpf.Windows.BacketWindow;
using CompaterShopWpf.Windows.FilterWindow;
using CompaterShopWpf.Windows.OrderHistoryWindow;

namespace CompaterShopWpf.Windows;

public class MainModel
{
    private ApplicationContext _dbContext = ApplicationContext.GetInstance();
    
    public ObservableCollection<Item>? OpenFilter(Catalog catalog)
    {
        var filterViewModel = new FilterViewModel(catalog);
        var filterWindow = new Filter(catalog, filterViewModel);
        if (filterWindow.ShowDialog() == true)
        {
            return ApplyFilter(catalog, filterViewModel.SelectedCategories, filterViewModel.MinPrice, filterViewModel.MaxPrice);
        }
        
        return null;
    }

    public ObservableCollection<Item> ApplyFilter(Catalog catalog, List<Category> categories, decimal minPrice, decimal maxPrice)
    {
        if (categories.Count == 0)
            categories = _dbContext.Categories.Where(c => c.CatalogId == catalog.Id).ToList();
        
        var categoryIds = categories.Select(c => c.Id).ToList();
        var products = _dbContext.Items
            .Where(p => categoryIds.Contains(p.CategoryId) && p.Price >= minPrice && p.Price <= maxPrice)
            .ToList();
        return new ObservableCollection<Item>(products);
    }

    public ObservableCollection<Item> GetItemsByCatalog(Catalog catalog)
    {
        var categories = _dbContext.Categories.Where(c => c.CatalogId == catalog.Id).ToList();
        var categoryIds = categories.Select(c => c.Id).ToList();
        var items = _dbContext.Items
            .Where(i => categoryIds.Contains(i.CategoryId))
            .ToList();
        
        return new ObservableCollection<Item>(items);
    }

    public void OpenBacket()
    {
        BacketInfo backetInfoWindow = new BacketInfo();
        backetInfoWindow.ShowDialog();
    }

    public void OpenAdminMenuCommand()
    {
        AdminMenu adminMenuWindow = new AdminMenu();
        adminMenuWindow.ShowDialog();
    }

    public void OpenOrderHistory()
    {
        OrderHistory orderHistoryWindow = new OrderHistory(SharedData.currentUser.Id);
        orderHistoryWindow.ShowDialog();
    }
}