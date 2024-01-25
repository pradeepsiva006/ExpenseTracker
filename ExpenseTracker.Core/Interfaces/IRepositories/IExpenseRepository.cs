using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces.IRepositories
{
    public interface IExpenseRepository : IBaseRepository<Expense>
    {
        Task<List<Expense>> GetAllExpensesByUserIdAsync(int userId);
    }
}
