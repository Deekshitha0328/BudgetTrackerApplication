using System;

namespace BudgetTracker.Database.Entities;

public class Income
{
    /// <summary>
    /// Has many-one relationship with User
    /// </summary>
    public int IncomeId { get; set; }
    public int UserId { get; set; }
    public string IncomeName { get; set; }
    public decimal Amount { get; set; }
    public DateTime IncomeCreditDate { get; set; } = DateTime.Now;
    public User User { get; set; }
}
