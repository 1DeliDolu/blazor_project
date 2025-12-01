namespace EventEase.Models;

/// <summary>
/// Represents an event in the EventEase application
/// </summary>
public class Event
{
    /// <summary>
    /// Unique identifier for the event
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the event
    /// </summary>
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the event takes place
    /// </summary>
    public DateTime EventDate { get; set; }

    /// <summary>
    /// Location where the event will be held
    /// </summary>
    public string EventLocation { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the event
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Maximum number of attendees allowed
    /// </summary>
    public int MaxAttendees { get; set; }

    /// <summary>
    /// Current number of registered attendees
    /// </summary>
    public int CurrentAttendees { get; set; }

    /// <summary>
    /// Category of the event (e.g., Corporate, Social, Educational)
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// URL to the event image
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the event is still accepting registrations
    /// </summary>
    public bool IsRegistrationOpen
    {
        get
        {
            // Enhanced validation with null safety
            try
            {
                return CurrentAttendees < MaxAttendees && 
                       EventDate > DateTime.Now && 
                       MaxAttendees > 0;
            }
            catch
            {
                return false;
            }
        }
    }
    
    /// <summary>
    /// Gets the number of available seats
    /// </summary>
    public int AvailableSeats => Math.Max(0, MaxAttendees - CurrentAttendees);
    
    /// <summary>
    /// Gets the occupancy percentage
    /// </summary>
    public double OccupancyPercentage
    {
        get
        {
            if (MaxAttendees <= 0) return 0;
            var percentage = (double)CurrentAttendees / MaxAttendees * 100;
            return Math.Min(100, Math.Max(0, percentage));
        }
    }
}
