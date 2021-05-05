using System;

namespace BlockBase.BBLinq.ExtensionMethods
{
    internal static class ObjectExtensionMethods
    {
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal;
        }

        public static int ToUnixTimestamp(this DateTime date)
        {
            return (int) (date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static void FromUnixTimestamp(this ref DateTime date, int timestamp)
        {
            date = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            date=date.AddSeconds(timestamp);
        }

    }

}
