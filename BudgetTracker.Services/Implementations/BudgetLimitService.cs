using AutoMapper;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Utitlities.Enums;

namespace BudgetTracker.Services.Implementations;

public class BudgetLimitService : IBudgetLimitService
{
    private readonly IBudgetLimitRepository _budgetLimitRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public BudgetLimitService(IBudgetLimitRepository budgetLimitRepository, IUserRepository userRepository, IMapper mapper)
    {
        _budgetLimitRepository = budgetLimitRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<BudgetLimitDTO>> GetAllBudgetLimitsAsync(string username)
    {
        var user=await _userRepository.GetUser(username);
        var budgetLimits = await _budgetLimitRepository.GetAllBudgetLimitsAsync(user.Id);
        return _mapper.Map<List<BudgetLimitDTO>>(budgetLimits);
    }
    public async Task<BudgetLimitDTO> GetBudgetLimitByIdAsync(int budgetLimitId,string username)
    {
        var user=await _userRepository.GetUser(username);
        var budgetLimit = await _budgetLimitRepository.GetBudgetLimitByIdAsync(budgetLimitId);
        return _mapper.Map<BudgetLimitDTO>(budgetLimit);
    }

    public async Task<BudgetLimitDTO> CreateBudgetLimitAsync(BudgetLimitDTO budgetLimitDto, string username, CategoryType categoryType)
    {
        var user = await _userRepository.GetUser(username);
        var budgetLimit = _mapper.Map<BudgetLimit>(budgetLimitDto);
        budgetLimit.UserId = user.Id;
        var category = await _budgetLimitRepository.GetCategory(categoryType);
        budgetLimit.CategoryId = category.CategoryId;
        var createdBudgetLimit = await _budgetLimitRepository.CreateBudgetLimitAsync(budgetLimit);
        return _mapper.Map<BudgetLimitDTO>(createdBudgetLimit);
    }

    public async Task<BudgetLimitDTO> UpdateBudgetLimitAsync(int limitId,BudgetLimitDTO budgetLimitDto,string username)
    {
        var user = await _userRepository.GetUser(username);
        var budget=await _budgetLimitRepository.GetBudgetLimitByIdAsync(limitId);
        budget.UserId=user.Id;
        var budgetLimit = _mapper.Map<BudgetLimit>(budgetLimitDto);
        var updatedBudgetLimit = await _budgetLimitRepository.UpdateBudgetLimitAsync(limitId,budgetLimit);
        return _mapper.Map<BudgetLimitDTO>(updatedBudgetLimit);
    }
    public async Task<bool> DeleteBudgetLimitAsync(int budgetLimitId,string userName)
    {
        var user = await _userRepository.GetUser(userName);
        var budgetLimit=await _budgetLimitRepository.GetBudgetLimitByIdAsync(budgetLimitId);
        budgetLimit.UserId=user.Id;
        return await _budgetLimitRepository.DeleteBudgetLimitAsync(budgetLimitId);
    }

}
