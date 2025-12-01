using EventEase.Models;

namespace EventEase.Services;

public class RegistrationService
{
    private readonly List<Registration> _registrations = new();
    private int _nextId = 1;

    public RegistrationService()
    {
        InitializeMockRegistrations();
    }

    private void InitializeMockRegistrations()
    {
        _registrations.AddRange(new[]
        {
            new Registration
            {
                Id = _nextId++,
                EventId = 1,
                UserId = 1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                NumberOfTickets = 2,
                SpecialRequests = "Wheelchair accessible seating",
                RegistrationDate = DateTime.Now.AddDays(-5),
                Status = RegistrationStatus.Confirmed,
                ConfirmationCode = GenerateConfirmationCode()
            },
            new Registration
            {
                Id = _nextId++,
                EventId = 2,
                UserId = 2,
                FullName = "Jane Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "9876543210",
                NumberOfTickets = 1,
                SpecialRequests = "",
                RegistrationDate = DateTime.Now.AddDays(-3),
                Status = RegistrationStatus.Confirmed,
                ConfirmationCode = GenerateConfirmationCode()
            }
        });
    }

    public async Task<Registration?> CreateRegistrationAsync(Registration registration)
    {
        await Task.Delay(100); // Simulate async operation

        // Check if user already registered for this event
        if (_registrations.Any(r => r.UserId == registration.UserId && r.EventId == registration.EventId))
        {
            return null; // User already registered
        }

        registration.Id = _nextId++;
        registration.RegistrationDate = DateTime.Now;
        registration.Status = RegistrationStatus.Pending;
        registration.ConfirmationCode = GenerateConfirmationCode();

        _registrations.Add(registration);

        return registration;
    }

    public async Task<List<Registration>> GetUserRegistrationsAsync(int userId)
    {
        await Task.Delay(50); // Simulate async operation
        return _registrations
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.RegistrationDate)
            .ToList();
    }

    public async Task<List<Registration>> GetEventRegistrationsAsync(int eventId)
    {
        await Task.Delay(50); // Simulate async operation
        return _registrations
            .Where(r => r.EventId == eventId)
            .OrderByDescending(r => r.RegistrationDate)
            .ToList();
    }

    public async Task<Registration?> GetRegistrationByIdAsync(int id)
    {
        await Task.Delay(50); // Simulate async operation
        return _registrations.FirstOrDefault(r => r.Id == id);
    }

    public async Task<Registration?> GetRegistrationByConfirmationCodeAsync(string confirmationCode)
    {
        await Task.Delay(50); // Simulate async operation
        return _registrations.FirstOrDefault(r => 
            r.ConfirmationCode.Equals(confirmationCode, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<bool> UpdateRegistrationStatusAsync(int id, RegistrationStatus status)
    {
        await Task.Delay(50); // Simulate async operation

        var registration = _registrations.FirstOrDefault(r => r.Id == id);
        if (registration != null)
        {
            registration.Status = status;
            return true;
        }

        return false;
    }

    public async Task<bool> CancelRegistrationAsync(int id)
    {
        await Task.Delay(50); // Simulate async operation

        var registration = _registrations.FirstOrDefault(r => r.Id == id);
        if (registration != null)
        {
            registration.Status = RegistrationStatus.Cancelled;
            return true;
        }

        return false;
    }

    public async Task<int> GetEventRegistrationCountAsync(int eventId)
    {
        await Task.Delay(50); // Simulate async operation
        return _registrations
            .Where(r => r.EventId == eventId && r.Status != RegistrationStatus.Cancelled)
            .Sum(r => r.NumberOfTickets);
    }

    public async Task<bool> IsUserRegisteredAsync(int userId, int eventId)
    {
        await Task.Delay(50); // Simulate async operation
        return _registrations.Any(r => 
            r.UserId == userId && 
            r.EventId == eventId && 
            r.Status != RegistrationStatus.Cancelled);
    }

    private string GenerateConfirmationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
