


using CSharpFunctionalExtensions;

namespace Core.Common;

public class User
{
    public User(string name, string hashPassword)
    {
        Name = name;
        HashPassword = hashPassword;
    }
    
    public static Result<User> CreateUser(string userName, string hashPassword)
    {
        if (string.IsNullOrEmpty(userName))
            return Result.Failure<User>("Имя пользователя не может быть пустым");
        
        var user = new User(userName, hashPassword);
        return Result.Success(user);
    }
    
    public int Id { get; private set; }
    public string Name { get; set; }
    public string HashPassword { get; set; }
    
}