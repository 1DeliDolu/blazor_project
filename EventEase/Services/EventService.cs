using EventEase.Models;

namespace EventEase.Services;

/// <summary>
/// Service for managing events in the EventEase application
/// </summary>
public class EventService
{
    private List<Event> _events;

    public EventService()
    {
        // Initialize with mock data
        _events = GetMockEvents();
    }

    /// <summary>
    /// Gets all available events
    /// </summary>
    public List<Event> GetAllEvents()
    {
        return _events ?? new List<Event>();
    }

    /// <summary>
    /// Gets a specific event by ID with validation
    /// </summary>
    public Event? GetEventById(int id)
    {
        try
        {
            // Validate input
            if (id <= 0)
                return null;
                
            return _events?.FirstOrDefault(e => e.Id == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetEventById: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Registers a user for an event with validation and error handling
    /// </summary>
    public bool RegisterForEvent(int eventId)
    {
        try
        {
            var eventItem = GetEventById(eventId);
            
            // Comprehensive validation
            if (eventItem == null)
            {
                Console.WriteLine($"Event {eventId} not found");
                return false;
            }
            
            if (!eventItem.IsRegistrationOpen)
            {
                Console.WriteLine($"Registration closed for event {eventId}");
                return false;
            }
            
            // Prevent overbooking
            if (eventItem.CurrentAttendees >= eventItem.MaxAttendees)
            {
                Console.WriteLine($"Event {eventId} is full");
                return false;
            }
            
            eventItem.CurrentAttendees++;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in RegisterForEvent: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Creates mock event data for demonstration
    /// </summary>
    private List<Event> GetMockEvents()
    {
        return new List<Event>
        {
            new Event
            {
                Id = 1,
                EventName = "Tech Summit 2025",
                EventDate = new DateTime(2025, 12, 15, 9, 0, 0),
                EventLocation = "Istanbul Congress Center",
                Description = "The largest technology conference of the year. Industry leaders and innovative solutions come together.",
                MaxAttendees = 500,
                CurrentAttendees = 234,
                Category = "Corporate",
                ImageUrl = "/images/tech-summit.png"
            },
            new Event
            {
                Id = 2,
                EventName = "New Year's Gala",
                EventDate = new DateTime(2025, 12, 31, 20, 0, 0),
                EventLocation = "Ciragan Palace, Istanbul",
                Description = "You are invited to a magnificent New Year's Eve. Live music, cocktails, and entertainment.",
                MaxAttendees = 200,
                CurrentAttendees = 156,
                Category = "Social",
                ImageUrl = "/images/new-year-gala.png"
            },
            new Event
            {
                Id = 3,
                EventName = "Entrepreneurship Workshop",
                EventDate = new DateTime(2025, 12, 10, 14, 0, 0),
                EventLocation = "ITU Technopolis, Istanbul",
                Description = "A comprehensive training program for those who want to start a startup. Mentorship and networking opportunities.",
                MaxAttendees = 50,
                CurrentAttendees = 42,
                Category = "Educational",
                ImageUrl = "/images/entrepreneurship.png"
            },
            new Event
            {
                Id = 4,
                EventName = "Art and Culture Festival",
                EventDate = new DateTime(2025, 12, 20, 10, 0, 0),
                EventLocation = "Zorlu Performing Arts Center, Istanbul",
                Description = "A meeting of modern art, music, and culture. Exhibitions, concerts, and workshops.",
                MaxAttendees = 300,
                CurrentAttendees = 178,
                Category = "Social",
                ImageUrl = "/images/art-festival.png"
            },
            new Event
            {
                Id = 5,
                EventName = "Blazor and .NET Workshop",
                EventDate = new DateTime(2025, 12, 8, 13, 0, 0),
                EventLocation = "Microsoft Turkey, Istanbul",
                Description = "A hands-on workshop on building modern web applications with Blazor.",
                MaxAttendees = 30,
                CurrentAttendees = 28,
                Category = "Educational",
                ImageUrl = "/images/blazor-workshop.png"
            },
            new Event
            {
                Id = 6,
                EventName = "Corporate Networking Evening",
                EventDate = new DateTime(2025, 12, 18, 18, 30, 0),
                EventLocation = "Raffles Hotel, Istanbul",
                Description = "A networking event for business professionals. Cocktails and business talks.",
                MaxAttendees = 150,
                CurrentAttendees = 89,
                Category = "Corporate",
                ImageUrl = "/images/networking.png"
            }
        };
    }
}
