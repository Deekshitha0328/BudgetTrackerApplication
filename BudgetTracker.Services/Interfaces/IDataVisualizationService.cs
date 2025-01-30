using BudgetTracker.Services.DTOs;

namespace BudgetTracker.Services.Interfaces;

public interface IDataVisualizationService
{
    Task<List<CategoryExpenseDTO>> GetExpenseByCategoryAsync(string username);
    Task<List<IncomeVsExpenseDto>> GetIncomeVsExpensesAsync(string username);

}
