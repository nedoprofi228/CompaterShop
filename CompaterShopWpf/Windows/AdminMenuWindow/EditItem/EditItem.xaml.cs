using System.Windows;

namespace CompaterShopWpf.Windows.AdminMenuWindow.EditCategory;

public partial class EditItem : Window
{
    EditItemViewModel _editItemViewModel;
    public EditItem(EditItemViewModel editItemViewModel)
    {
        _editItemViewModel = editItemViewModel;
        InitializeComponent();
        DataContext = _editItemViewModel;
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