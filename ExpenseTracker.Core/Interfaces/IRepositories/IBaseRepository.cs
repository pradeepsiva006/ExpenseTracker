using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> InsertAsync(T entity);
        Task<bool> UpdateAsync(T model);
    }
}
