using System.Security.Cryptography.X509Certificates;

namespace BudgetTracker.Database.Entities;

public class BudgetLimit
{
    
    /// <summary>
    /// Has one-one relationship with category
    /// Has many-one relationship with user
    /// Has one-many relationship with notification
    /// </summary>
    public int BudgetLimitId { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal SetLimit { get; set; }
    public decimal CurrentSpending { get; set; }
    public User User { get; set; }
    public Category Category { get; set; }
    public List<Notification> Notifications { get; set; }
}
