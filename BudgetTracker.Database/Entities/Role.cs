using System;

namespace BudgetTracker.Database.Entities;

public class Role
{
    /// <summary>
    /// Has One-many relationship with users
    /// </summary>
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public List<User> Users { get; set; }
}
