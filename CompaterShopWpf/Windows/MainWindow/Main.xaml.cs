using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.Windows.ItemWindow;
using StoreApp.ViewModels;

namespace CompaterShopWpf.Windows;

public partial class Main : Window
{
    private MainViewModel _viewModel = new MainViewModel();
    public Main()
    {
        InitializeComponent();
        DataContext = _viewModel;
        
        if(SharedData.currentUser.Role != "Admin")
            AdminMenuBtn.Visibility = Visibility.Collapsed;
    }
    
    private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            if (sender is TreeViewItem item && item.DataContext is Catalog catalog)
            {
                viewModel.SelectedCatalog = catalog;
            }
        }
    }
    private void ProductList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var listView = sender as ListView;
        if (listView != null)
        {
            var selectedItem = listView.SelectedItem as Item;
            if (selectedItem != null)
            {
                ProductList_OnSelected(selectedItem);
                listView.SelectedItem = null;
            }
        }
    }
    
    private void ProductList_OnSelected(Item item)
    {
        ItemInfo itemInfoWidow = new ItemInfo(item);
        itemInfoWidow.ShowDialog();
    }

    private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            _viewModel.SearchText = textBox.Text;
        }
    }
}