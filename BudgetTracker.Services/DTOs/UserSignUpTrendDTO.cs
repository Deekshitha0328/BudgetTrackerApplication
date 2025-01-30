using System;

namespace BudgetTracker.Services.DTOs;

public class UserSignUpTrendDTO
{
    public DateTime Month { get; set; }
    public int SignUpCount { get; set; }
}

