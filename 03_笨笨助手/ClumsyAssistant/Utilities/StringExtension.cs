namespace ClumsyAssistant.Utilities
{
    public static class StringExtension
    {
        public static double ToNumber(this string str)
        {
            double d = 0;
            if (!string.IsNullOrEmpty(str))
            {
                double.TryParse(str, out d);
            }
            return d;
        }
    }
}
