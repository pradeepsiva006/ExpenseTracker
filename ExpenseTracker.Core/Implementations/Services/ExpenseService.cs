using AutoMapper;
using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Common.Models;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Interfaces.IRepositories;
using ExpenseTracker.Core.Interfaces.IServices;

namespace ExpenseTracker.Core.Implementations.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddExpenseForUser(ExpenseRequestDto expense, int userId)
        {
            if (!ValidateExpenseRequest(expense))
            {
                throw new CustomAppException("Please provide valid expense data!");
            }
            var expenseAdded = await _expenseRepository.InsertAsync(new Common.Models.Expense() { 
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date,
                ShortDescription = expense.ShortDescription,
                UserId = userId
            });

            if(expenseAdded == null) { return false; }
            return true;
            
        }

        public async Task<bool> UpdateExpenseWithId(ExpenseRequestDto expense)
        {
            if (!ValidateExpenseRequest(expense))
            {
                throw new CustomAppException("Please provide valid expense data!");
            }
            return await _expenseRepository.UpdateAsync(new Common.Models.Expense()
            {
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date,
                ShortDescription = expense.ShortDescription,
                UserId = expense.UserId,
                Id = expense.ExpenseId
            });
        }

        private bool ValidateExpenseRequest(ExpenseRequestDto expense)
        {
            return expense.Amount > 0 && !string.IsNullOrEmpty(expense.Category) && expense.Date != default;
        }

        public async Task<ExpenseResponseDto> GetAllExpensesByUserId(int userId)
        {
            ExpenseResponseDto response = new ExpenseResponseDto();
            var expensesFromDb = await _expenseRepository.GetAllExpensesByUserIdAsync(userId);
            response.Expenses = _mapper.Map<List<ExpenseDto>>(expensesFromDb);
            return response;
        }
    }
}
