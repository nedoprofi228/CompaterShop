using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;

namespace CompaterShopWpf.Windows.AdminMenuWindow.EditCatalogWindow;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

public class EditNameViewModel
{
    private readonly ApplicationContext _dbContext = ApplicationContext.GetInstance();
    public string Name { get; set; }
    
}