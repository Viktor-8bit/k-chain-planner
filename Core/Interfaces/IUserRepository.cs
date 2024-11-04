

using Core.Common;
using CSharpFunctionalExtensions;

namespace Core.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>?> GetUsers(); 
    public Task<User?> GetUserById(int userId);
    public Task<Result<User>> CreateUser(User user);
    public Task<Result<User>> DeleteUser(int userId);
}