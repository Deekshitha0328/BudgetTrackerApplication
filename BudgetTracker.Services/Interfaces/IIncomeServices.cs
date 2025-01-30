using BudgetTracker.Services.DTOs;

namespace BudgetTracker.Services.Interfaces;

public interface IIncomeServices
{
   Task<List<IncomeDTO>> GetAllIncomesAsync(string username);
   Task<IncomeDTO> GetIncomeByIdAsync(int incomeId, string userName);
   Task<IncomeDTO> CreateIncomeAsync(IncomeDTO incomeDto, string username);
   Task<IncomeDTO> UpdateIncomeAsync(int incomeId, IncomeDTO incomeDto, string userName);
   Task DeleteIncomeAsync(int incomeId, string userName);
   Task<List<IncomeDTO>> FindIncomeBetween(DateTime startDate, DateTime endDate);
   Task<decimal> FindTotalIncomeBetween(DateTime startdate,DateTime enddate); 

}
