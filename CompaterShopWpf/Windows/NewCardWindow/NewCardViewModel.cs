using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;

namespace CompaterShopWpf.Windows.NewCardWindow;

public class NewCardViewModel
{
    private readonly ApplicationContext _dbContext = ApplicationContext.GetInstance();
    private string _cardNumber;
    private decimal _balance;
    private string _errorMessage;
    
    public string CardNumber
    {
        get => _cardNumber;
        set
        {
            _cardNumber = value;
            OnPropertyChanged(nameof(CardNumber));
        }
    }

    public decimal Balance
    {
        get => _balance;
        set
        {
            _balance = value;
            OnPropertyChanged(nameof(Balance));
        }
    }
    

    public bool Save()
    {
        CardNumber = CardNumber.Replace(" ", String.Empty);
        
        // Валидация номера карты
        if (string.IsNullOrEmpty(CardNumber) || CardNumber.Length != 16 || !long.TryParse(CardNumber, out _))
        {
            MessageBox.Show("Номер карты должен быть 16-значным числом.");
            return false;
        }

        // Валидация баланса
        if (Balance < 0)
        {
            MessageBox.Show("Баланс не может быть отрицательным.");
            return false;
        }

        // Проверка уникальности номера карты
        if (_dbContext.Cards.Any(c => c.CardNumber == CardNumber))
        {
            MessageBox.Show("Карта с таким номером уже существует.");
            return false;
        }
        
        MessageBox.Show("карта успешно добавлена");
        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
