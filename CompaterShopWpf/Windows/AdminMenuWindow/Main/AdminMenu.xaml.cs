using System.Windows;

namespace CompaterShopWpf.Windows.AdminMenuWindow;

public partial class AdminMenu : Window
{
    private AdminMenuViewModel _adminMenuViewModel;
    public AdminMenu()
    {
        _adminMenuViewModel = new AdminMenuViewModel();
        InitializeComponent();
        DataContext = _adminMenuViewModel;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();  
    }
}