using ExpenseTracker.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces.IServices
{
    public interface IExpenseService
    {
        Task<bool> AddExpenseForUser(ExpenseRequestDto expense, int userId);
        Task<bool> UpdateExpenseWithId(ExpenseRequestDto expense);
        Task<ExpenseResponseDto> GetAllExpensesByUserId(int userId);
    }
}
