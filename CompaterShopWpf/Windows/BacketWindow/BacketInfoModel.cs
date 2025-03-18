using System.Windows;
using System.Windows.Controls;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using CompaterShopWpf.Windows.NewCardWindow;

namespace CompaterShopWpf.Windows.BacketWindow;

public class BacketInfoModel
{
    ApplicationContext _dbContext = ApplicationContext.GetInstance();
    public bool BuyItems(Card card)
    {
        User user = SharedData.currentUser;
        if (card.Balance < user.Backet.TotalPrice)
        {
            MessageBox.Show("Недостаточно средств");
            return false;
        }

        if (user.Backet.Items.Count == 0)
        {
            MessageBox.Show("Корзина пуста");
            return false;
        }
    
        Order order = new Order()
        {
            OrderDate = DateTime.Now,
            OrderPrice = user.Backet.TotalPrice,
            User = user,
            Card = card,
            Items = user.Backet.Items.ToList()
        };
    
        card.Balance -= user.Backet.TotalPrice;
        user.Backet.Items.Clear();
    
        _dbContext.Orders.Add(order);
        _dbContext.Cards.Update(card);
        _dbContext.SaveChanges();

        MessageBox.Show("Товары успешно куплены");
        return true;
    }
    public Card? OpenAddCard()
    {
        User user = SharedData.currentUser;
        
        NewCardViewModel newCardViewModel = new NewCardViewModel();
        NewCard newCardWindow = new NewCard(newCardViewModel);
        if (newCardWindow.ShowDialog() == true)
        {
            Card newCard = new Card()
            {
                UserId = user.Id,
                CardNumber = newCardViewModel.CardNumber,
                Balance = newCardViewModel.Balance
            };
            
            SharedData.currentUser.Cards.Add(newCard);
            _dbContext.Cards.Add(newCard);
            _dbContext.SaveChanges();
            return newCard;
        }
        
        return null;
    }
    
}