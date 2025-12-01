using EventEase.Models;

namespace EventEase.Services;

public class UserSessionService
{
    private User? _currentUser;
    private readonly List<User> _users = new();
    
    public event Action? OnChange;

    public UserSessionService()
    {
        // Initialize with mock users for demonstration
        InitializeMockUsers();
    }

    private void InitializeMockUsers()
    {
        _users.Add(new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Password = "password123", // In production, use proper hashing
            FavoriteEventIds = new List<int> { 1, 3 },
            RegisteredEventIds = new List<int> { 1 },
            Preferences = new UserPreferences
            {
                PreferredCategories = new List<string> { "Technology", "Business" },
                EmailNotifications = true,
                Theme = "light"
            }
        });

        _users.Add(new User
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            PhoneNumber = "9876543210",
            Password = "password123",
            FavoriteEventIds = new List<int> { 2, 4 },
            RegisteredEventIds = new List<int> { 2, 3 },
            Preferences = new UserPreferences
            {
                PreferredCategories = new List<string> { "Entertainment", "Education" },
                EmailNotifications = false,
                SmsNotifications = true,
                Theme = "dark"
            }
        });
    }

    public User? GetCurrentUser()
    {
        return _currentUser;
    }

    public bool IsUserLoggedIn()
    {
        return _currentUser != null;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        await Task.Delay(100); // Simulate async operation
        
        var user = _users.FirstOrDefault(u => 
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && 
            u.Password == password);

        if (user != null)
        {
            user.LastLoginAt = DateTime.Now;
            _currentUser = user;
            NotifyStateChanged();
            return true;
        }

        return false;
    }

    public async Task<bool> RegisterAsync(User newUser)
    {
        await Task.Delay(100); // Simulate async operation

        // Check if email already exists
        if (_users.Any(u => u.Email.Equals(newUser.Email, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        newUser.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
        newUser.CreatedAt = DateTime.Now;
        newUser.LastLoginAt = DateTime.Now;
        _users.Add(newUser);
        
        _currentUser = newUser;
        NotifyStateChanged();
        return true;
    }

    public void Logout()
    {
        _currentUser = null;
        NotifyStateChanged();
    }

    public async Task<bool> UpdateUserAsync(User updatedUser)
    {
        await Task.Delay(100); // Simulate async operation

        var existingUser = _users.FirstOrDefault(u => u.Id == updatedUser.Id);
        if (existingUser != null)
        {
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
            existingUser.Preferences = updatedUser.Preferences;

            if (_currentUser?.Id == updatedUser.Id)
            {
                _currentUser = existingUser;
            }

            NotifyStateChanged();
            return true;
        }

        return false;
    }

    public bool AddToFavorites(int eventId)
    {
        if (_currentUser != null && !_currentUser.FavoriteEventIds.Contains(eventId))
        {
            _currentUser.FavoriteEventIds.Add(eventId);
            NotifyStateChanged();
            return true;
        }
        return false;
    }

    public bool RemoveFromFavorites(int eventId)
    {
        if (_currentUser != null && _currentUser.FavoriteEventIds.Contains(eventId))
        {
            _currentUser.FavoriteEventIds.Remove(eventId);
            NotifyStateChanged();
            return true;
        }
        return false;
    }

    public bool IsFavorite(int eventId)
    {
        return _currentUser?.FavoriteEventIds.Contains(eventId) ?? false;
    }

    public bool AddRegistration(int eventId)
    {
        if (_currentUser != null && !_currentUser.RegisteredEventIds.Contains(eventId))
        {
            _currentUser.RegisteredEventIds.Add(eventId);
            NotifyStateChanged();
            return true;
        }
        return false;
    }

    public bool IsRegistered(int eventId)
    {
        return _currentUser?.RegisteredEventIds.Contains(eventId) ?? false;
    }

    public List<int> GetUserFavorites()
    {
        return _currentUser?.FavoriteEventIds ?? new List<int>();
    }

    public List<int> GetUserRegistrations()
    {
        return _currentUser?.RegisteredEventIds ?? new List<int>();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
