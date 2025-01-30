
using BudgetTracker.Database.Entities;

namespace BudgetTracker.Database.Interfaces;

public interface IUserRepository
{
    Task<bool> CreateUser(User user);
    Task<User> AuthenticateUser(string username);
    Task<User> UpdateUser(int id, User updatedUser);
    Task<bool> DeleteUser(int id);
    Task<List<User>> GetAllUsers();
    Task<User> GetUser(string userName);
    Task<User> GetUserById(int id);
    Task<IEnumerable<User>> GetUserSignUpsAsync(DateTime startDate, DateTime endDate);
  




}
