using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Common.DTOs
{
    public class UserResponseDto : BaseResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
