using BudgetTracker.Database.DbContexts;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Utitlities.Enums;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Database.Implementations;

public class BudgetLimitRepository : IBudgetLimitRepository
{
    private readonly BudgetTrackerDbContext _context;

    public BudgetLimitRepository(BudgetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<List<BudgetLimit>> GetAllBudgetLimitsAsync(int userId)
    {
        return await _context.BudgetLimits
            .Include(bl => bl.Category)
            .Where(bl => bl.UserId == userId)
            .ToListAsync();
    }
    public async Task<BudgetLimit> GetBudgetLimitByIdAsync(int budgetLimitId)
    {
        return await _context.BudgetLimits
            .Include(bl => bl.Category)
            .FirstOrDefaultAsync(bl => bl.BudgetLimitId == budgetLimitId);
    }

    public async Task<BudgetLimit> CreateBudgetLimitAsync(BudgetLimit budgetLimit)
    {
        _context.BudgetLimits.Add(budgetLimit);
        await _context.SaveChangesAsync();
        return budgetLimit;
    }

    public async Task<BudgetLimit> UpdateBudgetLimitAsync(int id, BudgetLimit budgetLimit)
    {
        var existing = await _context.BudgetLimits.FindAsync(id);
        if (existing == null)
        {
            throw new Exception("Id not found");
        }
        existing.SetLimit = budgetLimit.SetLimit;
        existing.CurrentSpending = budgetLimit.CurrentSpending;
        await _context.SaveChangesAsync();
        return existing;
    }
    public async Task<bool> DeleteBudgetLimitAsync(int budgetLimitId)
    {
        var budgetLimit = await _context.BudgetLimits.FindAsync(budgetLimitId);
        _context.BudgetLimits.Remove(budgetLimit);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<Category> GetCategory(CategoryType categoryType)
    {
        string CategoryName = categoryType.ToString();
        return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == CategoryName);
    }
   

}
