namespace GymCoach.Shared.Utilities;

public static class PhoneNormalizer
{
    public static string Normalize(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return string.Empty;
        }

        var digits = new string(phone.Where(char.IsDigit).ToArray());
        if (digits.StartsWith("00", StringComparison.Ordinal))
        {
            digits = digits[2..];
        }

        return digits;
    }

    public static bool IsValid(string? phone)
    {
        var normalized = Normalize(phone);
        return normalized.Length is >= 10 and <= 15;
    }
}
