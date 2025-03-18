using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;


namespace CompaterShopWpf.services;

public class RegisterService
{
    public static bool Register(string name, string login, string password)
    {
        ApplicationContext dbContext = ApplicationContext.GetInstance();
        
        User? user = dbContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
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
        dbContext.Users.Add(newUser);
        return dbContext.SaveChanges() > 0;
    }
}