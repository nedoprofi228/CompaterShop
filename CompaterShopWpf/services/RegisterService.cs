using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;


namespace CompaterShopWpf.services;

public class RegisterService
{
    public static bool Register(string name, string login, string password)
    {
        ApplicationContext _dbContext = ApplicationContext.GetInstance();
        
        User? user = _dbContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        if (user != null)
            throw new Exception("Пользователь с таким логином уже существует");
        
        string role = "User";
        
        if(password == Config.AdminPassword)
            role = "Admin";
        
        User newUser = new User()
        {
            Login = login,
            Password = password,
            Name = name,
            Role = role
        };
        
        SharedData.currentUser = newUser;
        _dbContext.Users.Add(newUser);
        return _dbContext.SaveChanges() > 0;
    }
}