using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeServices _incomeServices;

        public IncomeController(IIncomeServices incomeServices)
        {
            _incomeServices = incomeServices;
        }

        [HttpPost("CreateIncome")]
        public async Task<ActionResult<IncomeDTO>> CreateIncome([FromBody] IncomeDTO incomeDto)
        {
            if (incomeDto == null)
            {
                return BadRequest("Income data is required.");
            }
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var createdIncome = await _incomeServices.CreateIncomeAsync(incomeDto, userName);
            return Ok(createdIncome);
        }

        [HttpGet("GetAllIncomes")]
        public async Task<ActionResult<List<IncomeDTO>>> GetAllIncomes()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var incomes = await _incomeServices.GetAllIncomesAsync(userName);
            return Ok(incomes);
        }


        [HttpPut("UpdateIncome/{id}")]
        public async Task<ActionResult<IncomeDTO>> UpdateIncome(int id, [FromBody] IncomeDTO incomeDto)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var updatedIncome = await _incomeServices.UpdateIncomeAsync(id, incomeDto, userName);
            if (updatedIncome == null)
            {
                return NotFound();
            }

            return Ok(updatedIncome);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await _incomeServices.DeleteIncomeAsync(id, userName);
            return NoContent();
        }

        [HttpGet("GetIncomeById/{incomeId}")]
        public async Task<ActionResult<IncomeDTO>> GetIncomeById(int incomeId)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User  is not authenticated.");
            }
            try
            {
                var incomeDto = await _incomeServices.GetIncomeByIdAsync(incomeId, userName);
                return Ok(incomeDto);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}

