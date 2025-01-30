using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataVisualizationController : ControllerBase
    {
        private readonly IDataVisualizationService _dataVisualizationService;

        public DataVisualizationController(IDataVisualizationService dataVisualizationService)
        {
            _dataVisualizationService = dataVisualizationService;
        }

        [HttpGet("expenses-by-category")]
        public async Task<IActionResult> GetExpensesByCategory()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var expensesByCategory = await _dataVisualizationService.GetExpenseByCategoryAsync(userName);
            return Ok(expensesByCategory);
        }

        [HttpGet("income-vs-expenses")]
        public async Task<IActionResult> GetIncomeVsExpenses()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var incomeVsExpenses = await _dataVisualizationService.GetIncomeVsExpensesAsync(userName);
            return Ok(incomeVsExpenses);
        }

    }

}
