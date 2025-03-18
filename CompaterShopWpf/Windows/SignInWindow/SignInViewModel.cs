using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CompaterShopWpf.Windows;

namespace CompaterShopWpf.Windows;

public class SignInViewModel : INotifyPropertyChanged
{
    private SignInModel _signInModel;
    
    private string _password; 
    private string _login;
    
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged("Password");
        }
    }
    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged("Login");
        }
    }
    
    public ICommand EnterCommand { get; }
    public ICommand ToRegisterCommand { get; }
    
    public SignInViewModel(Window window)
    {
        _signInModel = new SignInModel(window);
        
        EnterCommand = new DelegateCommand(() =>
        {
            if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
                return;

            try
            {
                _signInModel.SignIn(Login, Password);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        });
        
        ToRegisterCommand = new DelegateCommand(() =>
        {
            _signInModel.OpenSignUpWindow();
        });
    }
    
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        if(PropertyChanged != null )
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}