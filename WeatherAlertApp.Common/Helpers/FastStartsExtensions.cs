namespace WeatherAlertApp.Common.Helpers;

public static class FastStartsExtensions
{
    public static bool FastStartsWith(this string input, string substring)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(substring) || input.Length < substring.Length)
        {
            return false;
        }

        ReadOnlySpan<char> span = input.AsSpan();
        return span.Slice(0, substring.Length).SequenceEqual(substring.AsSpan());
    }
}