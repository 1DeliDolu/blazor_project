namespace EventEase.Models;

public class Registration
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int NumberOfTickets { get; set; }
    public string SpecialRequests { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;
    public string ConfirmationCode { get; set; } = string.Empty;
}

public enum RegistrationStatus
{
    Pending,
    Confirmed,
    Cancelled,
    Attended,
    NoShow
}
