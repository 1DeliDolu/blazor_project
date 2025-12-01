using EventEase.Models;

namespace EventEase.Services;

public class TestDataService
{
    /// <summary>
    /// Generate a large dataset for performance testing
    /// </summary>
    public static List<Event> GenerateLargeDataset(int count = 1000)
    {
        var events = new List<Event>();
        var random = new Random();
        var categories = new[] { "Technology", "Business", "Entertainment", "Education", "Sports", "Health" };
        var cities = new[] { "San Francisco", "New York", "Los Angeles", "Chicago", "Austin", "Seattle", "Boston", "Miami" };

        for (int i = 1; i <= count; i++)
        {
            events.Add(new Event
            {
                Id = i,
                Name = $"Event {i} - {categories[random.Next(categories.Length)]} Conference",
                Date = DateTime.Now.AddDays(random.Next(1, 365)),
                Location = $"{cities[random.Next(cities.Length)]}, USA",
                Description = $"Description for event {i}. This is an exciting opportunity to learn and network.",
                Capacity = random.Next(50, 1000),
                Category = categories[random.Next(categories.Length)]
            });
        }

        return events;
    }

    /// <summary>
    /// Generate edge case test data
    /// </summary>
    public static List<Event> GenerateEdgeCaseData()
    {
        return new List<Event>
        {
            // Valid event
            new Event
            {
                Id = 1,
                Name = "Valid Event",
                Date = DateTime.Now.AddDays(10),
                Location = "San Francisco, CA",
                Description = "A valid event",
                Capacity = 100,
                Category = "Technology"
            },
            // Empty name
            new Event
            {
                Id = 2,
                Name = "",
                Date = DateTime.Now.AddDays(10),
                Location = "New York, NY",
                Description = "Event with empty name",
                Capacity = 100,
                Category = "Business"
            },
            // Default date
            new Event
            {
                Id = 3,
                Name = "Event with Default Date",
                Date = default(DateTime),
                Location = "Los Angeles, CA",
                Description = "Event with default date",
                Capacity = 100,
                Category = "Entertainment"
            },
            // Empty location
            new Event
            {
                Id = 4,
                Name = "Event with Empty Location",
                Date = DateTime.Now.AddDays(10),
                Location = "",
                Description = "Event with empty location",
                Capacity = 100,
                Category = "Education"
            },
            // All invalid fields
            new Event
            {
                Id = 5,
                Name = "",
                Date = default(DateTime),
                Location = "",
                Description = "",
                Capacity = 0,
                Category = ""
            },
            // Very long name
            new Event
            {
                Id = 6,
                Name = new string('A', 200),
                Date = DateTime.Now.AddDays(10),
                Location = "Seattle, WA",
                Description = "Event with very long name",
                Capacity = 100,
                Category = "Technology"
            },
            // Special characters
            new Event
            {
                Id = 7,
                Name = "Event <script>alert('test')</script>",
                Date = DateTime.Now.AddDays(10),
                Location = "Boston, MA",
                Description = "Event with special characters",
                Capacity = 100,
                Category = "Business"
            }
        };
    }

    /// <summary>
    /// Test email validation cases
    /// </summary>
    public static Dictionary<string, bool> GetEmailTestCases()
    {
        return new Dictionary<string, bool>
        {
            { "valid@example.com", true },
            { "user.name@example.com", true },
            { "user+tag@example.co.uk", true },
            { "invalid@", false },
            { "@example.com", false },
            { "invalid", false },
            { "", false },
            { "invalid @example.com", false },
            { "invalid@.com", false },
            { "invalid@example", false }
        };
    }

    /// <summary>
    /// Test phone number validation cases
    /// </summary>
    public static Dictionary<string, bool> GetPhoneTestCases()
    {
        return new Dictionary<string, bool>
        {
            { "1234567890", true },
            { "123-456-7890", true },
            { "(123) 456-7890", true },
            { "+1 123 456 7890", true },
            { "12345", false },  // Too short
            { "12345678901234567890", false },  // Too long
            { "abc-def-ghij", false },  // Not digits
            { "", false },
            { "123 456", false }  // Too short
        };
    }
}
