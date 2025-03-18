using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;


namespace CompaterShopWpf.Windows
{
    public class FilterViewModel
    {
        private readonly ApplicationContext _context = ApplicationContext.GetInstance();
        private ObservableCollection<Category> _categories;
        private decimal _minPrice;
        private decimal _maxPrice;
        private List<Category> _selectedCategories;

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public List<Category> SelectedCategories
        {
            get => _selectedCategories;
            set
            {
                _selectedCategories = value;
                OnPropertyChanged(nameof(SelectedCategories));
            }
        }

        public decimal MinPrice
        {
            get => _minPrice;
            set
            {
                _minPrice = value;
                OnPropertyChanged(nameof(MinPrice));
            }
        }

        public decimal MaxPrice
        {
            get => _maxPrice;
            set
            {
                _maxPrice = value;
                OnPropertyChanged(nameof(MaxPrice));
            }
        }

        public FilterViewModel(Catalog catalog)
        {
            Categories = new ObservableCollection<Category>(_context.Categories
                .Where(c => c.CatalogId == catalog.Id)
                .ToList());
            
            SelectedCategories = new List<Category>();
            MinPrice = 0;
            MaxPrice = decimal.MaxValue;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}