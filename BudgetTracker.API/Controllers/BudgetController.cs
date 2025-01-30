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
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetLimitService _budgetLimitService;

        public BudgetController(IBudgetLimitService budgetLimitService)
        {
            _budgetLimitService = budgetLimitService;
        }


        [HttpGet("GetAllBudgetLimits")]
        public async Task<ActionResult<List<BudgetLimitDTO>>> GetAllBudgetLimitsAsync()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var budgetLimits = await _budgetLimitService.GetAllBudgetLimitsAsync(userName);
            return Ok(budgetLimits);
        }
        [HttpGet("GetById/{budgetLimitId}")]
        public async Task<ActionResult<BudgetLimitDTO>> GetBudgetLimitByIdAsync(int budgetLimitId)
        {
             var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            var budgetLimit = await _budgetLimitService.GetBudgetLimitByIdAsync(budgetLimitId,userName);
            if (budgetLimit == null)
            {
                return NotFound();
            }
            return Ok(budgetLimit);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BudgetLimitDTO>> CreateBudgetLimitAsync([FromBody]BudgetLimitDTO budgetLimitDto,[FromQuery] CategoryType categoryType )
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var createdBudgetLimit = await _budgetLimitService.CreateBudgetLimitAsync(budgetLimitDto, userName,categoryType);
            return Ok(createdBudgetLimit);
        }
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<BudgetLimitDTO>> UpdateBudgetLimitAsync(int id,[FromBody]BudgetLimitDTO budgetLimitDto)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var updatedBudgetLimit = await _budgetLimitService.UpdateBudgetLimitAsync(id,budgetLimitDto,userName);
            return Ok(updatedBudgetLimit);
        }

        [HttpDelete("Delete/{budgetLimitId}")]
        public async Task<IActionResult> DeleteBudgetLimitAsync(int budgetLimitId)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await _budgetLimitService.DeleteBudgetLimitAsync(budgetLimitId,userName);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
