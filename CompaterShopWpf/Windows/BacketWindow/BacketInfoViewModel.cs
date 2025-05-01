using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CompaterShopWpf;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.Windows.BacketWindow;

public class BacketInfoViewModel : INotifyPropertyChanged
{
    private ObservableCollection<ItemDTO> _items;
    private Backet _backet = SharedData.currentUser.Backet; // Предполагаемая модель корзины
    private BacketInfoModel _backetInfoModel = new ();
    private ObservableCollection<Card> _cards;
    private Card? _selectedCard;

    public Card? SelectedCard
    {
        get => _selectedCard;
        set
        {
            _selectedCard = value;
            OnPropertyChanged(nameof(SelectedCard));
        }
    }

    public ObservableCollection<Card> Cards
    {
        get => _cards;
        set
        {
            _cards = value;
            OnPropertyChanged(nameof(Cards));
        }
    }

    // Свойство Items
    public ObservableCollection<ItemDTO> Items
    {
        get => _items;
        set
        {
            // Отписываемся от старой коллекции, если она была
            if (_items != null)
            {
                _items.CollectionChanged -= Items_CollectionChanged;
            }
            _items = value;
            // Подписываемся на изменения новой коллекции
            if (_items != null)
            {
                _items.CollectionChanged += Items_CollectionChanged;
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(TotalPrice)); // Уведомляем UI об изменении TotalPrice
        }
    }

    // Вычисляемое свойство TotalPrice
    public decimal TotalPrice => Items?.Sum(i => i.Item.Price * i.Count) ?? 0;

    // Свойство для выбранного элемента
    public Item? SelectedItem
    {
        set
        {
            if (value != null)
            {
                ItemDTO item = _backet.ItemsDtos.Find(i => i.Item.Id == value.Id);
                _backet.ItemsDtos.Remove(item); // Удаляем из модели
                Items.Remove(item);         // Удаляем из ViewModel (TotalPrice обновится автоматически)
            }
        }
    }

    public ICommand BuyBacketCommand { get; }
    public ICommand AddNewCardCommand { get; }
    public ICommand OpenAgreementCommand { get; }
    
    // Конструктор
    public BacketInfoViewModel()
    {
        Cards = new ObservableCollection<Card>(SharedData.currentUser.Cards);
        Items = new ObservableCollection<ItemDTO>(_backet.ItemsDtos); // Инициализация из модели
        BuyBacketCommand = new DelegateCommand(() =>
        {
            if (SelectedCard == null )
                return;
            
            if(_backetInfoModel.BuyItems(SelectedCard))
                Items = new ObservableCollection<ItemDTO>();
        });

        AddNewCardCommand = new DelegateCommand(() =>
        {
            var card = _backetInfoModel.OpenAddCard();
            if (card == null)
                return;
            
            Cards = new ObservableCollection<Card>(SharedData.currentUser.Cards);
            SelectedCard = card;
        });
    }

    // Обработчик изменения коллекции
    private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(TotalPrice)); // Уведомляем UI при изменении Items
    }

    // Реализация INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}



