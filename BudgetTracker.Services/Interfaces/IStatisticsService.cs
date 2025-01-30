using BudgetTracker.Services.DTOs;

namespace BudgetTracker.Services.Interfaces;

public interface IStatisticsService
{
    // Task<int> GetActiveUserCountAsync();
    // Task<List<BudgetTrendDTO>> GetBudgetTrackingTrendsAsync(DateTime startDate, DateTime endDate);
    Task<List<UserSignUpTrendDTO>> GetUserSignUpTrendsAsync(DateTime startDate, DateTime endDate);
}
