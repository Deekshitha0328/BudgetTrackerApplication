using System;
using AutoMapper;
using BudgetTracker.Database.Entities;
using BudgetTracker.Database.Interfaces;
using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
using BudgetTracker.Utitlities.Enums;

namespace BudgetTracker.Services.Implementations;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IUserRepository userRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<ExpenseDTO>> GetAllExpensesAsync(string username)
    {
        var user = await _userRepository.GetUser(username);
        var expenses = await _expenseRepository.GetAllExpensesAsync(user.Id);
        return _mapper.Map<List<ExpenseDTO>>(expenses);
    }
    public async Task<ExpenseDTO> GetExpenseByIdAsync(int expenseId, string username)
    {
        var user = await _userRepository.GetUser(username);
        var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);
        return _mapper.Map<ExpenseDTO>(expense);
    }

    public async Task<ExpenseDTO> CreateExpenseAsync(string username, ExpenseDTO expenseDto, CategoryType categoryType, RecurrenceInterval recurrenceInterval)
    {
        var user = await _userRepository.GetUser(username);
        var expense = _mapper.Map<Expense>(expenseDto);
        expense.UserId = user.Id;
        var category = await _expenseRepository.GetCategory(categoryType);
        expense.CategoryId = category.CategoryId;
        var recurrence = await _expenseRepository.GetRecurrenceInterval(recurrenceInterval);
        expense.RecurrenceIntervalId = recurrence.Id;
        var createdExpense = await _expenseRepository.CreateExpenseAsync(expense);
        return _mapper.Map<ExpenseDTO>(createdExpense);
    }

    public async Task<ExpenseDTO> UpdateExpenseAsync(int expenseId, ExpenseDTO expenseDto, string username)
    {
        var user = await _userRepository.GetUser(username);
        var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);
        expense = _mapper.Map<Expense>(expenseDto);
        expense.UserId = user.Id;
        var updatedExpense = await _expenseRepository.UpdateExpenseAsync(expenseId, expense);
        return _mapper.Map<ExpenseDTO>(updatedExpense);
    }

    public async Task<bool> DeleteExpenseAsync(string username, int expenseId)
    {
        var user = await _userRepository.GetUser(username);
        var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);
        expense.UserId = user.Id;
        return await _expenseRepository.DeleteExpenseAsync(expenseId);
    }


}
