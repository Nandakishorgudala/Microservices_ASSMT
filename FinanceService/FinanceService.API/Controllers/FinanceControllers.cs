using FinanceService.Application.Dtos;
using FinanceService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public TransactionsController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _financeService.AddTransactionAsync(userId, dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _financeService.GetTransactionsAsync(userId);
            return Ok(result);
        }
    }

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public BudgetsController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpPost]
        public async Task<IActionResult> UpsertBudget([FromBody] CreateBudgetDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _financeService.UpsertBudgetAsync(userId, dto);
            return Ok(result);
        }

        [HttpGet("{month}")]
        public async Task<IActionResult> GetBudget(string month)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _financeService.GetBudgetAsync(userId, month);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
