namespace System
{
    public static class DateTimeExtension
    {
        public static Int64? ToUnixEpochDate(this DateTime? dateTime) => dateTime.HasValue ? ToUnixEpochDate(dateTime.Value) : (Int64?)null;

        public static Int64 ToUnixEpochDate(this DateTime dateTime) => (Int64)Math.Round((dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public static DateTime ToUnixEpochDate(this Int64 unixTime) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);


        public static DateTime? ToUnixEpochDate(this Int64? unixTime) => unixTime.HasValue ? ToUnixEpochDate(unixTime.Value) : (DateTime?)null;
    }
}
