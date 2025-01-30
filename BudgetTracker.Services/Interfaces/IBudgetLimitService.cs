using System;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Utitlities.Enums;

namespace BudgetTracker.Services.Interfaces;

public interface IBudgetLimitService
{
    Task<List<BudgetLimitDTO>> GetAllBudgetLimitsAsync(string username);
    Task<BudgetLimitDTO> GetBudgetLimitByIdAsync(int budgetLimitId,string username);
   Task<BudgetLimitDTO> CreateBudgetLimitAsync(BudgetLimitDTO budgetLimitDto, string username, CategoryType CategoryName);
    Task<BudgetLimitDTO> UpdateBudgetLimitAsync(int limitId,BudgetLimitDTO budgetLimitDto,string username);
    Task<bool> DeleteBudgetLimitAsync(int budgetLimitId,string userName);
}
