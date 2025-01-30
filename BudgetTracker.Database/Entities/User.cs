namespace BudgetTracker.Database.Entities;

public class User
{
    /// <summary>
    /// Has one-many relationship with Incomes
    /// Has one-many relationship with Expenses
    /// Has one-many relationship with BudgetLimits
    /// </summary>
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public List<Income> Incomes { get; set; }
    public List<Expense> Expenses { get; set; }
    public List<BudgetLimit> BudgetLimits { get; set; }
    public List<Notification> Notifications { get; set; }

}
