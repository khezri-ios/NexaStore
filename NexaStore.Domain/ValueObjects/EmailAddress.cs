using System.Text.RegularExpressions;

namespace NexaStore.Domain.ValueObjects;

public sealed class EmailAddress : IEquatable<EmailAddress>
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.");

        value = value.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(
            value,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new ArgumentException("Invalid email address.");
        }

        Value = value;
    }

    public bool Equals(EmailAddress? other)
    {
        return other is not null &&
               Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is EmailAddress other &&
               Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}