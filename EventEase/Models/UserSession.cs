namespace EventEase.Models;

/// <summary>
/// Represents a user session in the EventEase application
/// </summary>
public class UserSession
{
    /// <summary>
    /// Unique session identifier
    /// </summary>
    public string SessionId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// User's full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// User's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// User's company/organization
    /// </summary>
    public string Company { get; set; } = string.Empty;

    /// <summary>
    /// List of event IDs the user has registered for
    /// </summary>
    public List<int> RegisteredEventIds { get; set; } = new();

    /// <summary>
    /// Session creation time
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Last activity time
    /// </summary>
    public DateTime LastActivityAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Indicates if the user is logged in
    /// </summary>
    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(Email);

    /// <summary>
    /// Number of events user has registered for
    /// </summary>
    public int TotalRegistrations => RegisteredEventIds.Count;
}
