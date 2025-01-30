using BudgetTracker.Database.DbContexts;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Utitlities.Enums;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Database.Implementations;

public class UserRepository : IUserRepository
{
    private readonly BudgetTrackerDbContext _context;
    public UserRepository(BudgetTrackerDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateUser(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (existingUser != null)
        {
            return false;
        }
        var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "RegularUser");
        if (defaultRole == null)
        {
            throw new Exception("Not found");
        }
        if (user.RoleId == 0)
        {
            user.RoleId = defaultRole.RoleId;
        }

        await _context.Users.AddAsync(user);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
    public async Task<User> AuthenticateUser(string username)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<User> UpdateUser(int id, User updatedUser)
    {
        var existing = await _context.Users.FindAsync(id);
        if (existing == null)
        {
            throw new Exception("User not found");
        }
        existing.UserName = updatedUser.UserName;
        existing.Email = updatedUser.Email;
        existing.Password = updatedUser.Password;
        existing.FirstName = updatedUser.FirstName;
        existing.LastName = updatedUser.LastName;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }
   
    
    public async Task<User> GetUser(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<IEnumerable<User>> GetUserSignUpsAsync(DateTime startDate, DateTime endDate)  
    {   
        var userSignUps = await _context.Users  
            .Where(u => u.RegistrationDate >= startDate && u.RegistrationDate <= endDate) // Filter by sign-up date  
            .ToListAsync(); 

        return userSignUps; 
    }  

   
}




