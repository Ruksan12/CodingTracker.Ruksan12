using System.Globalization;

namespace CodingTracker.Ruksan12.Menu
{
    internal class Validation
    {
        public bool ValidateDate(string date, string format)
        {
            return DateTime.TryParseExact(date, format, new CultureInfo("en-US"), DateTimeStyles.None, out _);
        }

        public bool ValidateDateRange(string startDate, string endDate, string format)
        {
            DateTime start = DateTime.ParseExact(startDate, format, new CultureInfo("en-US"));
            DateTime end = DateTime.ParseExact(endDate, format, new CultureInfo("en-US"));

            return end > start;
        }
    }
}
