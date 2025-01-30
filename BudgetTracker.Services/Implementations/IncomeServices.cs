using System.Security.Claims;
using AutoMapper;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BudgetTracker.Services.Implementations;

public class IncomeServices : IIncomeServices
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;


    public IncomeServices(IIncomeRepository incomeRepository, IMapper mapper, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _incomeRepository = incomeRepository;
        _mapper = mapper;

    }


    public async Task<List<IncomeDTO>> GetAllIncomesAsync(string userName)
    {
        var user = await _userRepository.GetUser(userName);
        var incomes = await _incomeRepository.GetAllIncomesAsync(user.Id);
        return _mapper.Map<List<IncomeDTO>>(incomes);
    }


    public async Task<IncomeDTO> CreateIncomeAsync(IncomeDTO incomeDto, string userName)
    {
        var income = _mapper.Map<Income>(incomeDto);
        var user = await _userRepository.GetUser(userName);
        income.UserId = user.Id;
        var createdIncome = await _incomeRepository.CreateIncomeAsync(income);
        return _mapper.Map<IncomeDTO>(createdIncome);
    }
    public async Task<IncomeDTO> UpdateIncomeAsync(int incomeId, IncomeDTO incomeDto, string userName)
    {
        var user = await _userRepository.GetUser(userName);
        var income = await _incomeRepository.GetIncomeByIdAsync(incomeId);
        income.UserId = user.Id;
        var updatedIncome = _mapper.Map<Income>(incomeDto);
        var newincome = await _incomeRepository.UpdateIncomeAsync(incomeId, updatedIncome);
        return _mapper.Map<IncomeDTO>(newincome);
    }

    public async Task<IncomeDTO> GetIncomeByIdAsync(int incomeId, string userName)
    {
        var user = await _userRepository.GetUser(userName);
        if (user == null)
        {
            throw new Exception("User  not found.");
        }

        var income = await _incomeRepository.GetIncomeByIdAsync(incomeId);
        if (income == null || income.UserId != user.Id)
        {
            throw new Exception("Income not found or does not belong to the user.");
        }

        return _mapper.Map<IncomeDTO>(income);
    }

    public async Task DeleteIncomeAsync(int incomeId, string userName)
    {
        var user = await _userRepository.GetUser(userName);
        var income = await _incomeRepository.GetIncomeByIdAsync(incomeId);
        income.UserId = user.Id;
        await _incomeRepository.DeleteIncomeAsync(incomeId);
    }

    public async Task<List<IncomeDTO>> FindIncomeBetween(DateTime startDate, DateTime endDate)
    {
        var result = await _incomeRepository.FindIncomeBetween(startDate, endDate);
        return _mapper.Map<List<IncomeDTO>>(result);
    }

    public async Task<decimal> FindTotalIncomeBetween(DateTime startdate, DateTime enddate)
    {
        return await _incomeRepository.FindTotalIncomeBetween(startdate, enddate);
    }

    
}
