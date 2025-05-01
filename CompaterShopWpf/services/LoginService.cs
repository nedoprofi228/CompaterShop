using CompaterShopWpf.DataBase;

namespace CompaterShopWpf.services;

public class LoginService
{
    public static bool Login(string login, string password)
    {
        ApplicationContext _dbContext = ApplicationContext.GetInstance();

        SharedData.currentUser = _dbContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        if(SharedData.currentUser == null)
            return false;
        
        SharedData.currentUser.Cards = _dbContext.Cards.Where(c => c.UserId == SharedData.currentUser.Id).ToList();
        
        return true;
    }
}