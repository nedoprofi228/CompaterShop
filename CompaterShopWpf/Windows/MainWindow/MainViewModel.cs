using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using CompaterShopWpf.Windows;
using CompaterShopWpf.Windows.FilterWindow;
using CompaterShopWpf.Windows.ItemWindow;
using Microsoft.EntityFrameworkCore;


namespace StoreApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel _model = new MainModel();
        private readonly ApplicationContext _dbContext = ApplicationContext.GetInstance();
        private List<Item> _itemsByFilter = [];
        private ObservableCollection<Item> _items;
        private ObservableCollection<Catalog> _catalogs;
        private Catalog _selectedCatalog;
        private bool _isFilterApplied;
        private string _searchText = string.Empty;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Items = !String.IsNullOrEmpty(_searchText)
                    ? new ObservableCollection<Item>(_itemsByFilter.Where(i => i.Name.Contains(_searchText)))
                    : new ObservableCollection<Item>(_itemsByFilter);
            }
        }
        public ObservableCollection<Catalog> Catalogs
        {
            get => _catalogs;
            set
            {
                _catalogs = value;
                OnPropertyChanged(nameof(Catalogs));
            }
        }
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public Catalog SelectedCatalog
        {
            get => _selectedCatalog;
            set
            {
                _selectedCatalog = value;
                OnPropertyChanged(nameof(SelectedCatalog));
                if (value != null && !_isFilterApplied)
                {
                    Items = _model.GetItemsByCatalog(value);
                    _itemsByFilter = Items.ToList();
                }
            }
        }

        public ICommand OpenFilterCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand OpenBacketCommand { get; }
        public ICommand OpenAdminMenuCommand { get; }
        public ICommand OpenOrderHistoryCommand { get; }

        public MainViewModel()
        {
            Catalogs = new ObservableCollection<Catalog>(_dbContext.Catalogs);
            Items = new ObservableCollection<Item>();
            
            OpenFilterCommand = new DelegateCommand(() =>
            {
                if(_selectedCatalog != null)
                {
                    var res = _model.OpenFilter(SelectedCatalog);
                    if(res != null)
                    {
                        Items = res;
                        _itemsByFilter = Items.ToList();
                        _isFilterApplied = true;
                    }
                }
            });
            ClearFilterCommand = new DelegateCommand(() =>
            {
                if (_selectedCatalog != null)
                    Items = _model.GetItemsByCatalog(_selectedCatalog);
                else
                    Items = new ObservableCollection<Item>(); // Очистка списка, если каталог не выбран
                
                _isFilterApplied = false;
            });

            OpenBacketCommand = new DelegateCommand(() =>
            {
                _model.OpenBacket();
            });

            OpenAdminMenuCommand = new DelegateCommand(() =>
            {
                _model.OpenAdminMenuCommand();
                Catalogs = new ObservableCollection<Catalog>(_dbContext.Catalogs);
                
            });

            OpenOrderHistoryCommand = new DelegateCommand(() =>
            {
                _model.OpenOrderHistory();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}