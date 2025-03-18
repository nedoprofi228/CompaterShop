using System.Collections.ObjectModel;
using System.Windows;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using CompaterShopWpf.Windows.AdminMenuWindow.EditCatalogWindow;
using CompaterShopWpf.Windows.AdminMenuWindow.EditCategory;
using CompaterShopWpf.Windows.OrderHistoryWindow;

namespace CompaterShopWpf.Windows.AdminMenuWindow;

public class AdminMenuModel
{
    ApplicationContext _dbContext = ApplicationContext.GetInstance();
    AdminService _adminService = new AdminService();
    public Item AddItem(Category category)
    {
        Item newItem = new Item()
        {
            Name = "новый товар",
            CategoryId = category.Id,
        };
        newItem.Id = _adminService.AddItem(newItem);
        
        return newItem;
    }

    public bool DeleteItem(Item item)
    {
        if(_adminService.DeleteItem(item.Id))
            return true;

        MessageBox.Show("ошибка удаления");
        return false;
    }

    public Item EditItem(Item item)
    {
        try
        {
            EditItemViewModel editItemViewModel = new EditItemViewModel(item);
            EditItem editItem = new EditItem(editItemViewModel);
            if(editItem.ShowDialog() == true)
            {
                Item newItem = editItemViewModel.Item;
                _adminService.UpdateItem(item);
                return newItem;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }

        return item;
    }

    public Catalog AddCatalog()
    {
        Catalog newCatalog = new Catalog()
        {
            Name = "новый каталог",
            Categories = []
        };
        newCatalog.Id = _adminService.AddCatalog(newCatalog);
        
        return newCatalog;
    }

    public bool DeleteCatalog(Catalog catalog)
    {
        try
        {
            _adminService.DeleteCatalog(catalog.Id);
            return true;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        
        return false;
    }

    public Catalog EditCatalog(Catalog catalog)
    {
        try
        {
            EditNameViewModel editNameViewModel = new EditNameViewModel();
            EditName editName = new EditName(editNameViewModel);
            
            if(editName.ShowDialog() == true)
            {
                Catalog newCatalog = new Catalog()
                {
                    Id = catalog.Id,
                    Name = editNameViewModel.Name,
                    Categories = catalog.Categories
                };
                _adminService.UpdateCatalog(newCatalog);
                return newCatalog;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        
        return catalog;
    }

    public Category Addcategory(Catalog catalog)
    {
        Category newCategory = new Category()
        {
            CategoryName = "Новая категория",
            CatalogId = catalog.Id,
            Items = []
        };
        newCategory.Id = _adminService.AddCategory(newCategory);
        
        return newCategory;
    }

    public bool DeleteCategory(Category category)
    {
        try
        {
            _adminService.DeleteCategory(category.Id);
            return true;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }

        MessageBox.Show("ошибка удаления");
        return false;
    }

    public Category? Editcategory( Category? category)
    {
        
        if(category == null)
            return null;
        
        try
        {
            EditNameViewModel editNameViewModel = new EditNameViewModel();
            EditName editName = new EditName(editNameViewModel);
            
            if(editName.ShowDialog() == true)
            {
                Category newCategory = new Category()
                {
                    Id = category.Id,
                    CategoryName = editNameViewModel.Name,
                    CatalogId = category.CatalogId,
                    Items = category.Items
                };
                _adminService.UpdateCategory(newCategory);
                return newCategory;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        
        return category;
    }
    
    public ObservableCollection<Catalog> LoadCatalogs()
    {
        var catalogs = _dbContext.Catalogs.ToList();
        foreach (var catalog in catalogs)
        {
            catalog.Categories = _dbContext.Categories.Where(c => c.CatalogId == catalog.Id).ToList();
            
            foreach (var category in catalog.Categories)
            {
                category.Items = _dbContext.Items.Where(i => i.CategoryId == category.Id).ToList();
            }
        }
        
        return new ObservableCollection<Catalog>(catalogs);
    }

    public void OpenOrdersHistory()
    {
        OrderHistory orderHistoryWindow = new OrderHistory();
        orderHistoryWindow.Show();
    }
}