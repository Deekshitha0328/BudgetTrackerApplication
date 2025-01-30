using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService){
            _statisticsService=statisticsService;
        }
         [HttpGet("trends")]  
    public async Task<IActionResult> GetUserSignUpTrends([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)  
    {    
        if (endDate < startDate)  
        {  
            return BadRequest("End date must be greater than or equal to start date.");  
        }  
        var trends = await _statisticsService.GetUserSignUpTrendsAsync(startDate, endDate);   
        return Ok(trends);  
    }  
    }
}
