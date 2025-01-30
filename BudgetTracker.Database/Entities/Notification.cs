using BudgetTracker.Database.Entities;

public class Notification
{
    /// <summary>
    /// Has many-one relationship with user
    /// Has many-one relationship with BudgetLimit
    /// </summary>
    public int NotificationId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public int UserId { get; set; }
    public int BudgetId { get; set; }
    public BudgetLimit BudgetLimit { get; set; }
    public User User { get; set; }
}


