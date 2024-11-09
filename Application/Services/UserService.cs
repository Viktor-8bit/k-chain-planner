


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

    public async Task<Result<User>> CreateUser(User user)
    {
        var result = await userRepository.CreateUser(user);
        return result;
    }

    public async Task<Result<User>> DeleteUser(int userId)
    {
        var result = await userRepository.DeleteUser(userId);
        if (result.IsFailure)
            return Result.Failure<User>(result.Error);
        return Result.Success(result.Value);
    } 
}