
namespace BudgetTracker.Database.Entities;

public class Expense
{
    /// <summary>
    /// Has one-one relationship with category
    /// Has many-one relationship with User
    /// </summary>

    public int ExpenseId { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Datespent { get; set; } = DateTime.Now;
    public bool IsRecurring { get; set; }
    public int RecurrenceIntervalId { get; set; }
    public RecurrenceIntervalEntity recurrenceInterval { get; set; }
    public DateTime NextOccurence { get; set; }
    public User User { get; set; }
    public Category Category { get; set; }

}
