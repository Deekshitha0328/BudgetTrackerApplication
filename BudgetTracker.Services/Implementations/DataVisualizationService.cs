using BudgetTracker.Database.Interfaces;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
namespace BudgetTracker.Services.Implementations;

public class DataVisualizationService : IDataVisualizationService
{
    private readonly IUserRepository _userRepository;
    private readonly IIncomeRepository _incomeRepository;
    private readonly IExpenseRepository _expenseRepository;
    public DataVisualizationService(IUserRepository userRepository, IIncomeRepository incomeRepository, IExpenseRepository expenseRepository)
    {
        _userRepository = userRepository;
        _incomeRepository = incomeRepository;
        _expenseRepository = expenseRepository;
    }
    public async Task<List<CategoryExpenseDTO>> GetExpenseByCategoryAsync(string username)
    {
        var user = await _userRepository.GetUser(username);
        var expenses = await _expenseRepository.GetAllExpensesAsync(user.Id);
        return expenses.GroupBy(e => e.Category)
        .Select(g => new CategoryExpenseDTO
        {
            Category = g.Key.CategoryName,
            Total = g.Sum(e => e.Amount)

        }).ToList();
    }

    public async Task<List<IncomeVsExpenseDto>> GetIncomeVsExpensesAsync(string username)
    {
        var user = await _userRepository.GetUser(username);
        var income = await _incomeRepository.GetAllIncomesAsync(user.Id);
        var expenses = await _expenseRepository.GetAllExpensesAsync(user.Id);
        var incomeGrouping = income
            .GroupBy(i => new { i.Date.Year, i.Date.Month })
            .Select(g => new IncomeVsExpenseDto
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                TotalIncome = g.Sum(i => i.Amount)
            }).ToList();
        var expenseGrouping = expenses
            .GroupBy(e => new { e.Datespent.Year, e.Datespent.Month })
            .Select(g => new IncomeVsExpenseDto
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                TotalExpenses = g.Sum(e => e.Amount)
            }).ToList();
        var combined = incomeGrouping
       .GroupJoin(expenseGrouping,
           income => income.Month,
           expense => expense.Month,
           (income, expenseGroup) => new IncomeVsExpenseDto
           {
               Month = income.Month,
               TotalIncome = income.TotalIncome,
               TotalExpenses = expenseGroup.Any() ? expenseGroup.Sum(e => e.TotalExpenses) : 0
           }).ToList();

        return combined;

    }

}
