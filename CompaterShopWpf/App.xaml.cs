using System.Configuration;
using System.Data;
using System.Windows;
using CompaterShopWpf.DataBase;

namespace CompaterShopWpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        ApplicationContext.GetInstance();
    }
}