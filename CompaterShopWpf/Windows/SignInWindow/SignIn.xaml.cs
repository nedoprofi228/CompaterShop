using System.Windows;

namespace CompaterShopWpf.Windows;

public partial class SignIn : Window
{
    SignInViewModel viewModel;
    public SignIn()
    {
        InitializeComponent();
        viewModel = new SignInViewModel(this);
        
        DataContext = viewModel;
    }
}