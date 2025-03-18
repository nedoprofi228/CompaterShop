using System.Windows;

namespace CompaterShopWpf.Windows.NewCardWindow;

public partial class NewCard : Window
{
    private NewCardViewModel _viewModel;
    public NewCard(NewCardViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        DataContext = _viewModel;
    }
    
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.Save())
        {
            DialogResult = true;
            Close();
        }
        
    }
    private void Cancle_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}