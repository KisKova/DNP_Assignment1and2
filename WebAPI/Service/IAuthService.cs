using Shared.Dtos;
using Shared.Models;

namespace WebAPI.Service;

public interface IAuthService
{
    Task RegisterUser(UserRegistrationDto dto);
    
    Task<UserBasicDto> GetUserByName(string userName);

    Task<User> ValidateUser(string username, string password);
}