using System;
using BudgetTracker.Database.DbContexts;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Database.Implementations;

public class IncomeRepository : IIncomeRepository
{
    private readonly BudgetTrackerDbContext _context;
    public IncomeRepository(BudgetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<List<Income>> GetAllIncomesAsync(int userId)
    {
        return await _context.UserIncomes.Where(i => i.UserId == userId).ToListAsync();
    }

    public async Task<Income> CreateIncomeAsync(Income income)
    {
        _context.UserIncomes.AddAsync(income);
        await _context.SaveChangesAsync();
        return income;
    }

    public async Task<Income> UpdateIncomeAsync(int id, Income income)
    {
        var existing = await _context.UserIncomes.FindAsync(id);
        if (existing == null)
        {
            throw new Exception("Income not found");
        }
        existing.IncomeName = income.IncomeName;
        existing.Amount = income.Amount;
        existing.Date = income.Date;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<Income> GetIncomeByIdAsync(int incomeId)
    {
        return await _context.UserIncomes
            .FirstOrDefaultAsync(i => i.IncomeId == incomeId);
    }

    public async Task DeleteIncomeAsync(int incomeId)
    {

        var income = await _context.UserIncomes.FindAsync(incomeId);
        _context.UserIncomes.Remove(income);
        await _context.SaveChangesAsync();

    }

    public async Task<List<Income>> FindIncomeBetween(DateTime startdate, DateTime enddate)
    {
        return await _context.UserIncomes.Where(income=>income.Date>=startdate&& income.Date<=enddate).ToListAsync();
    }

    public  async Task<decimal> FindTotalIncomeBetween(DateTime startdate,DateTime enddate){
         return await Task.FromResult(_context.UserIncomes
            .Where(income => income.Date >= startdate && income.Date <= enddate)
            .Sum(income => income.Amount));
    } 

}
