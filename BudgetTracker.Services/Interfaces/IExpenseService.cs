using System;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Utitlities.Enums;

namespace BudgetTracker.Services.Interfaces;

public interface IExpenseService
{
    Task<List<ExpenseDTO>> GetAllExpensesAsync(string username);
    Task<ExpenseDTO> GetExpenseByIdAsync(int expenseId,string username);
    Task<ExpenseDTO> CreateExpenseAsync(string username, ExpenseDTO expenseDto,CategoryType categoryType, RecurrenceInterval recurrenceInterval);
    Task<ExpenseDTO> UpdateExpenseAsync(int expenseId, ExpenseDTO expenseDto,string userName);
    Task<bool> DeleteExpenseAsync(string username, int expenseId);

}
