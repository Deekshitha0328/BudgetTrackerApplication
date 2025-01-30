using BudgetTracker.Utitlities.Enums;
namespace BudgetTracker.Services.DTOs;
using Swashbuckle.AspNetCore.Annotations;

public class BudgetLimitDTO
{
    [SwaggerIgnore]
    public int BudgetLimitId { get; set; }
    [SwaggerIgnore]
    public int CategoryId { get; set; }
    public decimal SetLimit { get; set; }
    public decimal CurrentSpending { get; set; }
}
