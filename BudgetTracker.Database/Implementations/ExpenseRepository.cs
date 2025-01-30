using BudgetTracker.Database.DbContexts;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Utitlities.Enums;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Database.Implementations;

public class ExpenseRepository : IExpenseRepository
{
    private readonly BudgetTrackerDbContext _context;

    public ExpenseRepository(BudgetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<List<Expense>> GetAllExpensesAsync(int userId)
    {
        return await _context.Expenses
            .Where(e => e.UserId == userId)
            .Include(e => e.Category)
            .Include(e => e.recurrenceInterval)
            .ToListAsync();
    }
    public async Task<Expense> GetExpenseByIdAsync(int expenseId)
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .Include(e => e.recurrenceInterval)
            .FirstOrDefaultAsync(e => e.ExpenseId == expenseId);
    }

    public async Task<Expense> CreateExpenseAsync(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }
    public async Task<Expense> UpdateExpenseAsync(int id, Expense expense)
    {
        var existing = await _context.Expenses.FindAsync(id);
        if (existing == null)
        {
            throw new Exception("Id not Found");
        }
        existing.Amount = expense.Amount;
        existing.Datespent = expense.Datespent;
        existing.IsRecurring = expense.IsRecurring;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteExpenseAsync(int expenseId)
    {
        var expense = await _context.Expenses.FindAsync(expenseId);
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Category> GetCategory(CategoryType categoryType)
    {
        string CategoryName = categoryType.ToString();
        return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == CategoryName);
    }

    public async Task<RecurrenceIntervalEntity> GetRecurrenceInterval(RecurrenceInterval recurrenceInterval)
    {
        string Name = recurrenceInterval.ToString();
        return await _context.recurrenceIntervalEntities.FirstOrDefaultAsync(e => e.Name == Name);
    }
}
