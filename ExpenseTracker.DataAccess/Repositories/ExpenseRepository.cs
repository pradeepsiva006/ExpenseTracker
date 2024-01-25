using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Common.Models;
using ExpenseTracker.Core.Interfaces.IRepositories;
using ExpenseTracker.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess.Repositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllExpensesByUserIdAsync(int userId)
        {
            return await _context.Expenses.Where(x => x.UserId == userId).ToListAsync();
        }


    }
}
