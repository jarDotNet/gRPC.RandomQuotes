namespace Client.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNumber(this string input)
        {
            return int.TryParse(input, out _);
        }
    }
}
