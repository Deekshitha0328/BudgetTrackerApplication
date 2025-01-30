using System;
using BudgetTracker.Services.DTOs;

namespace BudgetTracker.Services.Interfaces;

public interface IUserService
{
    Task<bool> CreateUser(UserDTO userDTO);
    Task<string> LoginUser(LoginDTO loginDTO);
    Task<UserDTO> UpdateUser(int id, UpdateDTO updatedUser);  
    Task<bool> DeleteUser(int id);  
    Task<List<UserDTO>> GetAllUsers();  

}
