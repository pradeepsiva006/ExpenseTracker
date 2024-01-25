using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IExpenseService _expenseService;


        public ExpenseController(ILogger<ExpenseController> logger, IExpenseService expenseService)
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        [HttpPost("addExpense")]
        [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddExpenseForUser(ExpenseRequestDto expense)
        {
            //TODO: the below fetch can be moved to a helper class method.
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out var userId))
            {
                await _expenseService.AddExpenseForUser(expense, userId);
                return Ok(new BaseResponseDto { Message = "Expense Added!" });
            }
            else
            {
                _logger.LogError("ExpenseController : AddExpenseForUser() failed when parsing userId in the token");
                return BadRequest("Invalid UserId in the token!");
            }

        }

        [HttpPut("updateExpense")]
        [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateExpenseForUser(ExpenseRequestDto updatedExpense)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out var userId))
            {
                if (userId != updatedExpense.UserId)
                {
                    _logger.LogError("ExpenseController : UpdateExpenseForUser() aborted as the userId in the request body does not match with the token!");
                    return BadRequest("UserId is not matching with the Token!");
                }
                await _expenseService.UpdateExpenseWithId(updatedExpense);
                return Ok(new BaseResponseDto { Message = "Expense Updated!" });
            }
            else
            {
                _logger.LogError("ExpenseController : UpdateExpenseForUser() failed when parsing userId in the token"); 
                return BadRequest("Invalid UserId in the token!");
            }
        }


        [HttpGet("GetAllExpenses")]
        [ProducesResponseType(typeof(ExpenseResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllExpensesForUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out var userId))
            {
                var res = await _expenseService.GetAllExpensesByUserId(userId);
                return Ok(res);
            }
            else
            {
                _logger.LogError("ExpenseController : GetAllExpenses() failed when parsing userId in the token"); 
                return BadRequest("Invalid UserId in the token!");
            }
        }




    }
}
