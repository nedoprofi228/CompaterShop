using System.Windows;
using CompaterShopWpf.services;

namespace CompaterShopWpf.Windows;

public class SignInModel(Window owner)
{
    public void SignIn(string login, string password)
    {
        
        if (LoginService.Login(login, password))
        {
            Main catalogWindow = new Main();
            catalogWindow.Show();
            owner.Close();
        }
    }

    public void OpenSignUpWindow()
    {
        SignUp signUp = new SignUp();
        signUp.Show();
        owner.Close();
    }
}