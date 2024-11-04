

using Core.Common;
using Core.Interfaces;
using CSharpFunctionalExtensions;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationContext _DbContext) : IUserRepository
{
    public async Task<IEnumerable<User>?> GetUsers() => 
        _DbContext.Users.ToList<User>();

    public async Task<User?> GetUserById(int userId) => 
        _DbContext.Users.FirstOrDefault(c => c.Id == userId);

    public async Task<Result<User>> CreateUser(User user)
    {
        var checkUser = _DbContext.Users
            .SingleOrDefault(u => u.Name.ToLower() == user.Name.ToLower());
  
        if (checkUser != null) 
            return Result.Failure<User>("Такой пользователь уже существует");
        
        _DbContext.Users.Add(user);
        await _DbContext.SaveChangesAsync();
        return Result.Success(user);
    }


    public async Task<Result<User>> DeleteUser(int userId)
    {
        var toDeleteUser = _DbContext.Users
            .SingleOrDefault(u => u.Id == userId);
        
        if (toDeleteUser == null) 
            return Result.Failure<User>("Такой пользователь не существует");
        
        _DbContext.Users.Remove(toDeleteUser);
        await _DbContext.SaveChangesAsync();
        return Result.Success(toDeleteUser);
    }
}