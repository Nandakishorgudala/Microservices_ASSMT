using InsightsService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InsightsService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InsightsController : ControllerBase
    {
        private readonly IInsightsService _insightsService;

        public InsightsController(IInsightsService insightsService)
        {
            _insightsService = insightsService;
        }

        [HttpGet("financial-health")]
        public async Task<IActionResult> GetFinancialHealth()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            // Extract the token from the header for the downstream call
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var result = await _insightsService.GetFinancialHealthAsync(userId, token);
            return Ok(result);
        }
    }
}
