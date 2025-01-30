using System;

namespace BudgetTracker.Services.DTOs;

public class LoginDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
