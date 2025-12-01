namespace EventEase.Models;

public class UserPreferences
{
    public List<string> PreferredCategories { get; set; } = new();
    public bool EmailNotifications { get; set; } = true;
    public bool SmsNotifications { get; set; } = false;
    public string Theme { get; set; } = "light";
    public string Language { get; set; } = "en";
    public int EventsPerPage { get; set; } = 12;
}
