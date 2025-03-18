using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CompaterShopWpf.DataBase;


namespace CompaterShopWpf.Windows;

public class SignUpViewModel : INotifyPropertyChanged
{
    private SignUpModel _signUpModel;

    private string _password; 
    private string _login;
    private string _username;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged("Username");
        }
    }
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
    public ICommand ToSignInCommand { get; }
    public ICommand OpenAgreeeCommand { get; }

    public SignUpViewModel(Window window)
    {
        _signUpModel = new SignUpModel(window);
        
        EnterCommand = new DelegateCommand(() =>
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || String.IsNullOrEmpty(Username))
                return;

            try
            {
                _signUpModel.SignUp(Username, Login, Password);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        });

        ToSignInCommand = new DelegateCommand(() =>
        {
            _signUpModel.OpenSignInWindow();
        });

        OpenAgreeeCommand = new DelegateCommand(() =>
        {
            _signUpModel.OpenUserAgreement();
        });
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        if(PropertyChanged != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}