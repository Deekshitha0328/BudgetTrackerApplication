using System;
using BudgetTracker.Database.Entities;
using BudgetTracker.Utitlities.Enums;

namespace BudgetTracker.Database.Interfaces;

public interface IBudgetLimitRepository
{
    Task<List<BudgetLimit>> GetAllBudgetLimitsAsync(int userId);
    Task<BudgetLimit> GetBudgetLimitByIdAsync(int budgetLimitId);
    Task<BudgetLimit> CreateBudgetLimitAsync(BudgetLimit budgetLimit);
    Task<BudgetLimit> UpdateBudgetLimitAsync(int id,BudgetLimit budgetLimit);
    Task<bool> DeleteBudgetLimitAsync(int budgetLimitId);
    Task<Category> GetCategory(CategoryType CategoryType);
}
