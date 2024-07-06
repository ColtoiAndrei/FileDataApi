namespace FileDataApi.Extensions
{
    public static class StringExtensions
    {
        public static List<string> SplitToLines(this string input)
        {
            return input
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .ToList();
        }
    }
}
