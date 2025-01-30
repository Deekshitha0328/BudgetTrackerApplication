using System;
using BudgetTracker.Utitlities.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace BudgetTracker.Services.DTOs;

public class ExpenseDTO
{
    [SwaggerIgnore]
    public int ExpenseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Datespent { get; set; } = DateTime.Now;
    [SwaggerIgnore]
    public int CategoryId { get; set; }
    public bool IsRecurring { get; set; }
    [SwaggerIgnore]
    public int RecurrenceIntervalId { get; set; }
}
