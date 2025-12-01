namespace EventEase.Models;

/// <summary>
/// Represents a registration for an event
/// </summary>
public class Registration
{
    /// <summary>
    /// Unique registration identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Reference number for the registration
    /// </summary>
    public string ReferenceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Event ID
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// Full name of registrant
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Company/Organization
    /// </summary>
    public string Company { get; set; } = string.Empty;

    /// <summary>
    /// Special requests or notes
    /// </summary>
    public string SpecialRequests { get; set; } = string.Empty;

    /// <summary>
    /// Newsletter subscription preference
    /// </summary>
    public bool Newsletter { get; set; }

    /// <summary>
    /// Registration date and time
    /// </summary>
    public DateTime RegisteredAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Check-in status
    /// </summary>
    public bool IsCheckedIn { get; set; }

    /// <summary>
    /// Check-in time
    /// </summary>
    public DateTime? CheckedInAt { get; set; }

    /// <summary>
    /// QR code for check-in
    /// </summary>
    public string QrCode { get; set; } = string.Empty;
}
