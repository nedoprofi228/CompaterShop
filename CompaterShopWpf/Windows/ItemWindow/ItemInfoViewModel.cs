using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CompaterShopWpf;
using CompaterShopWpf.Core.Entities;

public class ItemInfoViewModel : INotifyPropertyChanged
{
    private Item _item;
    private int _count = 1;

    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            OnPropertyChanged(nameof(Item));
        }
    }

    public int Count
    {
        get => _count;
        set
        {
            if(int.TryParse(value.ToString(), out _count))
            {
                OnPropertyChanged(nameof(Count));
            }
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
        User user = SharedData.currentUser;
        ItemDTO? itemDto = user.Backet.ItemsDtos.Find(i => i.Item.Id == Item.Id);
        
        if(itemDto != null)
        {
            itemDto.Count += _count;
            MessageBox.Show($"предмет добавлен в корзину");
            return;
        }
        
        user.Backet.ItemsDtos.Add(new ItemDTO()
        {
            Item = Item,
            Count = _count
        });
        MessageBox.Show($"предмет добавлен в корзину");
    }

    private void RemoveFromBacket()
    {
        ItemDTO itemDto = SharedData.currentUser.Backet.ItemsDtos.Find(i => i.Item.Id == Item.Id);
        SharedData.currentUser.Backet.ItemsDtos.Remove(itemDto);
        
        MessageBox.Show("предмет удален из козины");
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}