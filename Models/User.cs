namespace EventEase.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // In production, this should be hashed
    public List<int> FavoriteEventIds { get; set; } = new();
    public List<int> RegisteredEventIds { get; set; } = new();
    public UserPreferences Preferences { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastLoginAt { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
}
