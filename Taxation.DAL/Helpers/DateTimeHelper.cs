namespace Taxation.DAL.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTimeOffset ConvertToDateTimeOffset(string dateString)
        {
            DateTime date = DateTime.Parse(dateString);
            DateTimeOffset dateTimeOffset = new(date, TimeZoneInfo.Local.GetUtcOffset(date));
            return dateTimeOffset;
        }

        public static bool IsValidDate(string dateString)
        {
            return DateTime.TryParse(dateString, out _);
        }
    }
}
