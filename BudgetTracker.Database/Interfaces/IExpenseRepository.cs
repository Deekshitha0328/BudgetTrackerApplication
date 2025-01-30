using BudgetTracker.Database.Entities;
using BudgetTracker.Utitlities.Enums;

namespace BudgetTracker.Database.Interfaces;

public interface IExpenseRepository
{
    Task<List<Expense>> GetAllExpensesAsync(int userId);
    Task<Expense> GetExpenseByIdAsync(int expenseId);
    Task<Expense> CreateExpenseAsync(Expense expense);
    Task<Expense> UpdateExpenseAsync(int id, Expense expense);
    Task<bool> DeleteExpenseAsync(int expenseId);
    Task<Category> GetCategory(CategoryType categoryType);
    Task<RecurrenceIntervalEntity> GetRecurrenceInterval(RecurrenceInterval recurrenceInterval);

}
