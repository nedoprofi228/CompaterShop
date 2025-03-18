using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CompaterShopWpf;
using CompaterShopWpf.Core.Entities;

public class ItemInfoViewModel : INotifyPropertyChanged
{
    private Item _item;

    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            OnPropertyChanged(nameof(Item));
        }
    }

    public ICommand AddToBacketCommand { get; }
    public ICommand RemoveFromBacketCommand { get; }

    public ItemInfoViewModel(Item item)
    {
        Item = item;
        AddToBacketCommand = new DelegateCommand(() => AddToBacket());
        RemoveFromBacketCommand = new DelegateCommand(() => RemoveFromBacket());
    }

    private void AddToBacket()
    {
        SharedData.currentUser.Backet.Items.Add(Item);
        
        MessageBox.Show($"предмет добавлен в корзину");
    }

    private void RemoveFromBacket()
    {
        SharedData.currentUser.Backet.Items.Remove(Item);
        
        MessageBox.Show("предмет удален из козины");
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}