using System.Text.Json.Serialization;

namespace BudgetTracker.Utitlities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CategoryType
{
    Food = 1,
    Rent = 2,
    Utilities = 3,
    Entertainment = 4,
    Transportation = 5,
    HealthCare = 6

}
