using BudgetTracker.Database.Interfaces;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;

namespace BudgetTracker.Services.Implementations;

public class StatisticsService : IStatisticsService
{
    private readonly IUserRepository _userRepository;
    private readonly IBudgetLimitRepository _budgetLimitRepository;
    public StatisticsService(IBudgetLimitRepository budgetLimitRepository, IUserRepository userRepository)
    {
        _budgetLimitRepository = budgetLimitRepository;
        _userRepository = userRepository;

    }

    public async Task<List<UserSignUpTrendDTO>> GetUserSignUpTrendsAsync(DateTime startDate, DateTime endDate)
    {
        var userSignUps = await _userRepository.GetUserSignUpsAsync(startDate, endDate);
        if (userSignUps == null || !userSignUps.Any())
        {
            return new List<UserSignUpTrendDTO>();
        }
        var signUpTrends = userSignUps
            .GroupBy(u => new { u.RegistrationDate.Year, u.RegistrationDate.Month })
            .Select(g => new UserSignUpTrendDTO
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                SignUpCount = g.Count()
            })
            .OrderBy(t => t.Month)
            .ToList();

        return signUpTrends;
    }
}
