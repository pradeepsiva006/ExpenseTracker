using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Common.DTOs
{
    public class ExpenseResponseDto : BaseResponseDto
    {
        public ExpenseResponseDto() {
            Expenses = new List<ExpenseDto>();
        }
        public List<ExpenseDto> Expenses { get; set; }
    }

    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string ShortDescription { get; set; }
    }
}
