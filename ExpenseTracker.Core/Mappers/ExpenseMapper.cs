using AutoMapper;
using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Common.Models;

namespace ExpenseTracker.Core.Mappers
{
    public class ExpenseMapper : Profile
    {
        public ExpenseMapper() {
            CreateMap<Expense, ExpenseDto>()
                .ForMember(d => d.ExpenseId, m => m.MapFrom(s => s.Id));
        }
    }
}
