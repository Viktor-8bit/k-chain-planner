


namespace Application.Services;

using CSharpFunctionalExtensions;
using Core.Common;
using Core.Interfaces;

public class UserService(IUserRepository userRepository)
{
    public async Task<IEnumerable<User>?> GetUsers() => 
        await userRepository.GetUsers();
    
    public async Task<User?> GetUserById(int userId) => 
        await userRepository.GetUserById(userId);
    
    public async Task<Result<User>> CreateUser(User user) => 
        await userRepository.CreateUser(user);
    
    public async Task<Result<User>> DeleteUser(int userId) => 
        await userRepository.DeleteUser(userId);
}