using CompaterShopWpf.DataBase;

namespace CompaterShopWpf.services;

public class LoginService
{
    public static bool Login(string login, string password)
    {
        ApplicationContext dbContext = ApplicationContext.GetInstance();

        SharedData.currentUser = dbContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        if(SharedData.currentUser == null)
            return false;
        
        SharedData.currentUser.Cards = dbContext.Cards.Where(c => c.UserId == SharedData.currentUser.Id).ToList();
        
        return true;
    }
}