namespace System
{
    public static class DateTimeExtension
    {
        public static Int64? ToUnixEpochDate(this DateTime? dateTime)
        {
            var result = dateTime.HasValue ? ToUnixEpochDate(dateTime.Value) : (Int64?)null;

            return result;
        }

        public static Int64 ToUnixEpochDate(this DateTime dateTime)
        {
            var result = (Int64)Math.Round((dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

            return result;
        }

        public static DateTime ToUnixEpochDate(this Int64 unixTime)
        {
            var result = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);

            return result;
        }


        public static DateTime? ToUnixEpochDate(this Int64? unixTime)
        {
            var result = unixTime.HasValue ? ToUnixEpochDate(unixTime.Value) : (DateTime?)null;

            return result;
        }
    }
}
