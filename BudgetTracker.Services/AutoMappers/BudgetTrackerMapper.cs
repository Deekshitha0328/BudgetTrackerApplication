using AutoMapper;
using BudgetTracker.Database.Entities;
using BudgetTracker.Services.DTOs;

namespace BudgetTracker.Services.AutoMappers;

public class BudgetTrackerMapper : Profile
{
    public BudgetTrackerMapper()
    {
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<LoginDTO, User>();
        CreateMap<UpdateDTO, User>();
        CreateMap<IncomeDTO, Income>().ReverseMap();
        CreateMap<BudgetLimitDTO, BudgetLimit>().ReverseMap();
        CreateMap<ExpenseDTO,Expense>().ReverseMap();
    }
}