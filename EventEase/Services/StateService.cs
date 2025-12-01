using EventEase.Models;

namespace EventEase.Services;

/// <summary>
/// Service for managing application state and user sessions
/// </summary>
public class StateService
{
    private UserSession? _currentSession;
    private readonly List<Registration> _registrations = new();

    /// <summary>
    /// Event raised when session state changes
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Gets the current user session
    /// </summary>
    public UserSession? CurrentSession => _currentSession;

    /// <summary>
    /// Gets all registrations
    /// </summary>
    public IReadOnlyList<Registration> Registrations => _registrations.AsReadOnly();

    /// <summary>
    /// Creates or updates the user session
    /// </summary>
    public void SetUserSession(string fullName, string email, string phone, string company = "")
    {
        try
        {
            if (_currentSession == null)
            {
                _currentSession = new UserSession
                {
                    FullName = fullName,
                    Email = email,
                    Phone = phone,
                    Company = company
                };
            }
            else
            {
                _currentSession.FullName = fullName;
                _currentSession.Email = email;
                _currentSession.Phone = phone;
                _currentSession.Company = company;
                _currentSession.LastActivityAt = DateTime.Now;
            }

            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting user session: {ex.Message}");
        }
    }

    /// <summary>
    /// Adds an event registration
    /// </summary>
    public Registration? AddRegistration(int eventId, string fullName, string email, 
        string phone, string company, string specialRequests, bool newsletter)
    {
        try
        {
            var registration = new Registration
            {
                Id = _registrations.Count + 1,
                ReferenceNumber = $"EE-{DateTime.Now:yyyyMMdd}-{new Random().Next(10000, 99999)}",
                EventId = eventId,
                FullName = fullName,
                Email = email,
                Phone = phone,
                Company = company,
                SpecialRequests = specialRequests,
                Newsletter = newsletter,
                QrCode = GenerateQrCode(eventId, email)
            };

            _registrations.Add(registration);

            // Update user session
            if (_currentSession != null && !_currentSession.RegisteredEventIds.Contains(eventId))
            {
                _currentSession.RegisteredEventIds.Add(eventId);
                _currentSession.LastActivityAt = DateTime.Now;
            }
            else if (_currentSession == null)
            {
                SetUserSession(fullName, email, phone, company);
                _currentSession?.RegisteredEventIds.Add(eventId);
            }

            NotifyStateChanged();
            return registration;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding registration: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets registrations for a specific event
    /// </summary>
    public List<Registration> GetEventRegistrations(int eventId)
    {
        try
        {
            return _registrations.Where(r => r.EventId == eventId).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting event registrations: {ex.Message}");
            return new List<Registration>();
        }
    }

    /// <summary>
    /// Gets registrations for the current user
    /// </summary>
    public List<Registration> GetUserRegistrations()
    {
        try
        {
            if (_currentSession == null || string.IsNullOrWhiteSpace(_currentSession.Email))
                return new List<Registration>();

            return _registrations
                .Where(r => r.Email.Equals(_currentSession.Email, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(r => r.RegisteredAt)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting user registrations: {ex.Message}");
            return new List<Registration>();
        }
    }

    /// <summary>
    /// Checks if user is registered for an event
    /// </summary>
    public bool IsUserRegistered(int eventId)
    {
        try
        {
            if (_currentSession == null)
                return false;

            return _currentSession.RegisteredEventIds.Contains(eventId);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Checks in a registration
    /// </summary>
    public bool CheckInRegistration(string referenceNumber)
    {
        try
        {
            var registration = _registrations.FirstOrDefault(r => 
                r.ReferenceNumber.Equals(referenceNumber, StringComparison.OrdinalIgnoreCase));

            if (registration != null && !registration.IsCheckedIn)
            {
                registration.IsCheckedIn = true;
                registration.CheckedInAt = DateTime.Now;
                NotifyStateChanged();
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking in registration: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Gets attendance statistics for an event
    /// </summary>
    public (int Total, int CheckedIn, int Pending) GetAttendanceStats(int eventId)
    {
        try
        {
            var eventRegistrations = GetEventRegistrations(eventId);
            var total = eventRegistrations.Count;
            var checkedIn = eventRegistrations.Count(r => r.IsCheckedIn);
            var pending = total - checkedIn;

            return (total, checkedIn, pending);
        }
        catch
        {
            return (0, 0, 0);
        }
    }

    /// <summary>
    /// Clears the current session
    /// </summary>
    public void ClearSession()
    {
        _currentSession = null;
        NotifyStateChanged();
    }

    /// <summary>
    /// Updates last activity time
    /// </summary>
    public void UpdateActivity()
    {
        if (_currentSession != null)
        {
            _currentSession.LastActivityAt = DateTime.Now;
        }
    }

    /// <summary>
    /// Generates a QR code identifier
    /// </summary>
    private string GenerateQrCode(int eventId, string email)
    {
        var data = $"{eventId}:{email}:{DateTime.Now.Ticks}";
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));
    }

    /// <summary>
    /// Notifies subscribers that state has changed
    /// </summary>
    private void NotifyStateChanged() => OnChange?.Invoke();
}
