using System;

namespace BudgetTracker.Services.DTOs;

public class IncomeVsExpenseDto
{
    public DateTime Month { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
}
