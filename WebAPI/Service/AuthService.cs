using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace WebAPI.Service;

public class AuthService : IAuthService
{

    private readonly IUserDao userDao;
    
    public AuthService(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    /*
    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }*/
    
    public async Task<UserBasicDto> GetUserByName(string username)
    {
        User? user = await userDao.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception($"User with username: {username} not found");
        }

        return new UserBasicDto(user.Id, user.Username, user.Password, user.Email,user.Domain,user.Name, user.Role, user.Age, user.SecurityLevel);
    }
    
    public Task<User> ValidateUser(string username, string password)
    {
        return userDao.Validation(username, password);
    }

    /*
    public async Task<User> ValidateUser(UserLoginDto dto)
    {
        User? existingUser = await userDao.GetByUsernameAsync(dto.Username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(dto.Password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
        return existingUser;
    }
    public async Task<User?> ValidateUser(UserLoginDto dto)
    {
        return await userDao.Validation(dto.Username, dto.Password);
    }*/

    /*
    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        users.Add(user);
        
        return Task.CompletedTask;
    }*/
    
    /*public async Task<User> RegisterUser(UserRegistrationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            Username = dto.Username,
            Password = dto.Password
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }*/
    
    private static void ValidateData(UserRegistrationDto userToCreate)
    {
        string userName = userToCreate.Username;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
    
    public Task RegisterUser(UserRegistrationDto dto)
    {

        if (string.IsNullOrEmpty(dto.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(dto.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        User newUser = new User();

        newUser.Username = dto.Username;
        newUser.Password = dto.Password;

        userDao.CreateAsync(newUser);
        
        return Task.CompletedTask;
    }
}