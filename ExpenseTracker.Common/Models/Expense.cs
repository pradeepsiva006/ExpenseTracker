using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Common.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(200)]
        public string Category { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [MaxLength(1000)]
        public string ShortDescription { get; set; }

        public virtual User User { get; set; }
    }
}
