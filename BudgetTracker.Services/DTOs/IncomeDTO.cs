using Swashbuckle.AspNetCore.Annotations;
namespace BudgetTracker.Services.DTOs;

public class IncomeDTO
{
    [SwaggerIgnore]
    public int IncomeId{get;set;}
    public string IncomeName { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}
