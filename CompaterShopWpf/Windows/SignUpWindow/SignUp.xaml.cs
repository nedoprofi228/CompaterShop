using System.Windows;

namespace CompaterShopWpf.Windows;

public partial class SignUp : Window
{
    SignUpViewModel signUpViewModel;
    public SignUp()
    {
        InitializeComponent();
        signUpViewModel = new SignUpViewModel(this);
        DataContext = signUpViewModel;
    }
}