using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CompaterShopWpf.Core.Entities;

namespace CompaterShopWpf.Windows.FilterWindow;

public partial class Filter : Window
{
    private FilterViewModel _filterViewModel;
    public Filter(Catalog catalog, FilterViewModel filterViewModel)
    {
        InitializeComponent();
        _filterViewModel = filterViewModel;
        DataContext = filterViewModel;
    }
    
    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Добавляем новые выбранные элементы
        foreach (Category item in e.AddedItems)
            if (!_filterViewModel.SelectedCategories.Contains(item))
                _filterViewModel.SelectedCategories.Add(item);
        

        // Удаляем снятые с выбора элементы
        foreach (Category item in e.RemovedItems)
            _filterViewModel.SelectedCategories.Remove(item);
    }
}