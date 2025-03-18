using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;

namespace CompaterShopWpf.Windows.AdminMenuWindow.EditCategory;

public class EditItemViewModel
{
    private ApplicationContext _dbContext = ApplicationContext.GetInstance();
    
    private Item _item { get; set; }

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
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}