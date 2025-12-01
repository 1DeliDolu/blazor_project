using EventEase.Models;

namespace EventEase.Services;

public class EventService
{
    private List<Event> _events = new List<Event>();

    public EventService()
    {
        // Initialize with mock data
        _events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Name = "Tech Conference 2024",
                Date = new DateTime(2024, 12, 15),
                Location = "San Francisco, CA",
                Description = "Annual technology conference featuring the latest innovations in AI and cloud computing.",
                Capacity = 500,
                Category = "Technology"
            },
            new Event
            {
                Id = 2,
                Name = "Marketing Summit",
                Date = new DateTime(2024, 12, 20),
                Location = "New York, NY",
                Description = "Join marketing professionals to discuss digital strategies and trends.",
                Capacity = 300,
                Category = "Business"
            },
            new Event
            {
                Id = 3,
                Name = "Music Festival 2024",
                Date = new DateTime(2024, 12, 25),
                Location = "Los Angeles, CA",
                Description = "Three-day music festival featuring top artists from around the world.",
                Capacity = 10000,
                Category = "Entertainment"
            },
            new Event
            {
                Id = 4,
                Name = "Startup Networking Event",
                Date = new DateTime(2025, 1, 10),
                Location = "Austin, TX",
                Description = "Connect with entrepreneurs and investors in the startup ecosystem.",
                Capacity = 200,
                Category = "Business"
            },
            new Event
            {
                Id = 5,
                Name = "Coding Bootcamp",
                Date = new DateTime(2025, 1, 15),
                Location = "Seattle, WA",
                Description = "Intensive two-week coding bootcamp for aspiring developers.",
                Capacity = 50,
                Category = "Education"
            }
        };
    }

    public Task<List<Event>> GetAllEventsAsync()
    {
        return Task.FromResult(_events);
    }

    public Task<Event?> GetEventByIdAsync(int id)
    {
        var eventItem = _events.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(eventItem);
    }

    public Task<List<Event>> GetEventsByCategoryAsync(string category)
    {
        var filteredEvents = _events.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        return Task.FromResult(filteredEvents);
    }

    public Task AddEventAsync(Event newEvent)
    {
        newEvent.Id = _events.Any() ? _events.Max(e => e.Id) + 1 : 1;
        _events.Add(newEvent);
        return Task.CompletedTask;
    }

    public Task UpdateEventAsync(Event updatedEvent)
    {
        var existingEvent = _events.FirstOrDefault(e => e.Id == updatedEvent.Id);
        if (existingEvent != null)
        {
            existingEvent.Name = updatedEvent.Name;
            existingEvent.Date = updatedEvent.Date;
            existingEvent.Location = updatedEvent.Location;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Capacity = updatedEvent.Capacity;
            existingEvent.Category = updatedEvent.Category;
        }
        return Task.CompletedTask;
    }

    public Task DeleteEventAsync(int id)
    {
        var eventItem = _events.FirstOrDefault(e => e.Id == id);
        if (eventItem != null)
        {
            _events.Remove(eventItem);
        }
        return Task.CompletedTask;
    }
}
