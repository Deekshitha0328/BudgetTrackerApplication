using BudgetTracker.Database.Entities;

namespace BudgetTracker.Database.Interfaces;

public interface IIncomeRepository
{
Task<List<Income>> GetAllIncomesAsync(int userId);
Task<Income> GetIncomeByIdAsync(int incomeId);
Task<Income> CreateIncomeAsync(Income income);
Task<Income> UpdateIncomeAsync(int id,Income income);
Task DeleteIncomeAsync(int incomeId);
Task<List<Income>> FindIncomeBetween(DateTime startDate,DateTime endDate);
Task<decimal> FindTotalIncomeBetween(DateTime startdate,DateTime enddate);
}
