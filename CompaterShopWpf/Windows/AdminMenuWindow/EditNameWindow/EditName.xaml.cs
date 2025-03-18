using System.Windows;

namespace CompaterShopWpf.Windows.AdminMenuWindow.EditCatalogWindow;

public partial class EditName : Window
{
    EditNameViewModel _editNameViewModel;
    public EditName(EditNameViewModel editNameViewModel)
    {
        _editNameViewModel = editNameViewModel;
        InitializeComponent();
        DataContext = _editNameViewModel;
    }

    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        this.Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        this.Close();
    }
    
}