using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;

namespace CompaterShopWpf.Windows.AdminMenuWindow.EditCategory;

public class EditItemViewModel
{
    private ApplicationContext _dbContext = ApplicationContext.GetInstance();
    private Item _item { get; set; }

    public string Name
    {
        get => _item.Name;
        set
        {
            _item.Name = value;
            OnPropertyChanged(nameof(Item));
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Description
    {
        get => _item.Description;
        set
        {
            _item.Description = value;
            OnPropertyChanged(nameof(Item));
            OnPropertyChanged(nameof(Description));
        }
    }

    public string Price
    {
        get => _item.Price.ToString();
        set
        {
            decimal price;
            if (!decimal.TryParse(value, out price))
            {
                return;
            }

            if (price < 0)
            {
                MessageBox.Show("цена должна быть положительной");
                return;
            }
            
            _item.Price = price;
            OnPropertyChanged(nameof(Item));
            OnPropertyChanged(nameof(Price));
        }
    }
    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            OnPropertyChanged(nameof(Item));
        }
    }

    

    public EditItemViewModel(Item item)
    {
        Item = item;
        Price = item.Price.ToString();
        Name = item.Name;
        Description = item.Description;
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}