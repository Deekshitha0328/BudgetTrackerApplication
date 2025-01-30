using System.Text.Json.Serialization;

namespace BudgetTracker.Utitlities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]

public enum RecurrenceInterval
{
Daily=1,
Weekly=2,
Monthly=3,
Yearly=4
}
