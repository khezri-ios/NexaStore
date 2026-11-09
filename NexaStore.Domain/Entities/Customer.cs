using NexaStore.Domain.ValueObjects;

namespace NexaStore.Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public EmailAddress Email { get; private set; } = null!;

    public string PhoneNumber { get; private set; } = string.Empty;

    // Constructor
    public Customer(
        string firstName,
        string lastName,
        EmailAddress email,
        string phoneNumber)
    {
        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));

        Email = email ?? throw new ArgumentNullException(nameof(email));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = phoneNumber?.Trim() ?? string.Empty;
    }

    // Private constructor for EF Core
    private Customer()
    {
    }

    // Update customer information
    public void UpdateProfile(
        string firstName,
        string lastName,
        EmailAddress email,
        string phoneNumber)
    {
        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));

        Email = email ?? throw new ArgumentNullException(nameof(email));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = phoneNumber?.Trim() ?? string.Empty;

        MarkAsUpdated();
    }

    private static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                $"{fieldName} is required.");
        }
    }
}