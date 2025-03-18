using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompaterShopWpf.Core.Entities;

namespace CompaterShopWpf.Windows.BacketWindow;

public partial class BacketInfo : Window
{
    BacketInfoViewModel _backetInfoViewModel;
    public BacketInfo()
    {
        _backetInfoViewModel = new BacketInfoViewModel();
        InitializeComponent();
        DataContext = _backetInfoViewModel;
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            _backetInfoViewModel.SelectedItem = (Item)e.AddedItems[0];
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}