using System;
using Swashbuckle.AspNetCore.Annotations;

namespace BudgetTracker.Services.DTOs;

public class UserDTO
{
    [SwaggerIgnore]
    public int id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;

}
