
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;

namespace CompaterShopWpf.Windows.AdminMenuWindow;

public class AdminMenuViewModel : INotifyPropertyChanged
{
    private readonly ApplicationContext _dbContext = ApplicationContext.GetInstance();
    private AdminMenuModel _adminMenuModel = new AdminMenuModel();
    private ObservableCollection<Catalog> _catalogs;
    
    private Catalog? _selectedCatalog;
    private ObservableCollection<Category> _selectedCatalogCategories;
    
    private Category? _selectedCategory;
    private ObservableCollection<Item> _selectedCategoryItems;
    
    private Item? _selectedItem;
    
    public AdminMenuViewModel()
    {
        Catalogs = _adminMenuModel.LoadCatalogs();

        // Инициализация команд
        AddCatalogCommand = new DelegateCommand(() =>
        {
            Catalogs.Add(_adminMenuModel.AddCatalog());
            OnPropertyChanged(nameof(Catalogs));
        });
        
        EditCatalogCommand = new DelegateCommand(() =>
        {
            var catalog = Catalogs.FirstOrDefault(c => c.Id == SelectedCatalog.Id);
            catalog = _adminMenuModel.EditCatalog(SelectedCatalog);
            Catalogs = new ObservableCollection<Catalog>(Catalogs);
        });
        
        DeleteCatalogCommand = new DelegateCommand((() =>
        {
            if(_adminMenuModel.DeleteCatalog(SelectedCatalog))
            {
                Catalogs.Remove(SelectedCatalog);
                OnPropertyChanged(nameof(Catalogs));
            }
        }));
        
        AddCategoryCommand = new DelegateCommand(() =>
        {
            if(SelectedCatalog == null)
                return;
            
            SelectedCatalogCategories.Add(_adminMenuModel.Addcategory(SelectedCatalog));
            OnPropertyChanged(nameof(SelectedCatalogCategories));
        });
        
        EditCategoryCommand = new DelegateCommand(() =>
        {
            if(SelectedCategory == null)
                return;
            
            var category = SelectedCatalogCategories.FirstOrDefault(c => c.Id == SelectedCategory.Id);
            category = _adminMenuModel.Editcategory(SelectedCategory);
            _selectedCatalogCategories = new ObservableCollection<Category>(SelectedCatalogCategories);
            OnPropertyChanged(nameof(SelectedCatalogCategories));
        });
        
        DeleteCategoryCommand = new DelegateCommand(() =>
        {
            if(SelectedCategory == null)
                return;
            
            if(_adminMenuModel.DeleteCategory(SelectedCategory))
            {
                SelectedCatalogCategories.Remove(SelectedCategory);
                OnPropertyChanged(nameof(SelectedCatalogCategories));
            }
        });
        
        AddItemCommand = new DelegateCommand(() =>
        {
            if(SelectedCategory == null)
                return;
            
            SelectedCategoryItems.Add(_adminMenuModel.AddItem(SelectedCategory));
            
            OnPropertyChanged(nameof(SelectedCategoryItems));
        });
        
        EditItemCommand = new DelegateCommand(() =>
        {
            if(SelectedItem == null)
                return;
            
            var item = SelectedCategoryItems.FirstOrDefault(i => i.Id == SelectedItem.Id);
            item = _adminMenuModel.EditItem(SelectedItem);
            SelectedCategory = SelectedCategory;
        });
        DeleteItemCommand = new DelegateCommand(() =>
        {
            if(SelectedCategory == null)
                return;
            
            if(_adminMenuModel.DeleteItem(SelectedItem))
            {
                SelectedCategoryItems.Remove(SelectedItem);
                OnPropertyChanged(nameof(SelectedCategoryItems));
            }
        });

        OpenOrdersHistoryCommand = new DelegateCommand(() =>
        {
            _adminMenuModel.OpenOrdersHistory();
        });

        OpenOrdersStatisticCommand = new DelegateCommand(() =>
        {
            _adminMenuModel.OpenOpenOrdersStatistic();
        });
    }

    public Item? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
        }
    }
    
    public ObservableCollection<Catalog> Catalogs
    {
        get => _catalogs;
        set 
        { 
            _catalogs = value;
            OnPropertyChanged(); 
        }
    }

    public Catalog? SelectedCatalog
    {
        get => _selectedCatalog;
        set
        {
            _selectedCatalog = value;
            OnPropertyChanged();
            _selectedCatalogCategories = SelectedCatalog != null
                ? new ObservableCollection<Category>(SelectedCatalog.Categories)
                : new ObservableCollection<Category>();

            OnPropertyChanged(nameof(SelectedCatalogCategories));
            SelectedCategory = null; // Сбрасываем выбранную категорию
            SelectedItem = null;
        }
    }

    public ObservableCollection<Category> SelectedCatalogCategories => _selectedCatalogCategories;

    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            _selectedCategory = value;
            OnPropertyChanged();
            _selectedCategoryItems = SelectedCategory != null 
                ? new ObservableCollection<Item>(SelectedCategory.Items) 
                : new ObservableCollection<Item>();
            OnPropertyChanged(nameof(SelectedCategoryItems));
            
            SelectedItem = null;
        }
    }

    public ObservableCollection<Item> SelectedCategoryItems => _selectedCategoryItems;

    // Команды
    public ICommand AddCatalogCommand { get; }
    public ICommand EditCatalogCommand { get; }
    public ICommand DeleteCatalogCommand { get; }
    public ICommand AddCategoryCommand { get; }
    public ICommand EditCategoryCommand { get; }
    public ICommand DeleteCategoryCommand { get; }
    public ICommand AddItemCommand { get; }
    public ICommand EditItemCommand { get; }
    public ICommand DeleteItemCommand { get; }
    public ICommand OpenOrdersHistoryCommand { get; }
    public ICommand OpenOrdersStatisticCommand { get; }
    

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

