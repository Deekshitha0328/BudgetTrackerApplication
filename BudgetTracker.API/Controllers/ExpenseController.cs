using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Utitlities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("GetAllExpenses")]
        public async Task<ActionResult<List<ExpenseDTO>>> GetAllExpenses()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var expenses = await _expenseService.GetAllExpensesAsync(userName);
            return Ok(expenses);
        }
        [HttpGet("GetExpenseById/{expenseId}")]
        public async Task<ActionResult<ExpenseDTO>> GetExpenseById(int expenseId)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var expense = await _expenseService.GetExpenseByIdAsync(expenseId, userName);
                return Ok(expense);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPost("CreateExpense")]
        public async Task<ActionResult<ExpenseDTO>> CreateExpense([FromBody] ExpenseDTO expenseDto, [FromQuery] CategoryType categoryType, [FromQuery] RecurrenceInterval recurrenceInterval)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var createdExpense = await _expenseService.CreateExpenseAsync(userName, expenseDto, categoryType, recurrenceInterval);
            return Ok(createdExpense);
        }

        [HttpPut("UpdateExpense/{id}")]
        public async Task<ActionResult<ExpenseDTO>> UpdateExpense(int id, [FromBody] ExpenseDTO expenseDto)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var updatedExpense = await _expenseService.UpdateExpenseAsync(id, expenseDto, userName);
                return Ok(updatedExpense);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
        [HttpDelete("Delete/{expenseId}")]
        public async Task<ActionResult> DeleteExpense(int expenseId)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var success = await _expenseService.DeleteExpenseAsync(userName, expenseId);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }


        }
    }
}
