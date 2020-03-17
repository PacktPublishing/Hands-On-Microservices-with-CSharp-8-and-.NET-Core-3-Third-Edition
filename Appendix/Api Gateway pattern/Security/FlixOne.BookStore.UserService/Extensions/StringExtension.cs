namespace System
{
    public static class StringExtension
    {
        public static int ToInt32(this string Expr, int DefaultValue = 0) => ToInt32Nullable(Expr) ?? DefaultValue;
        public static int? ToInt32Nullable(this string Expr)
        {
            int? result = null;

            if (Expr.IsNullOrEmpty())
                return null;

            Expr = Expr.Trim();

            int newInt32;

            if (int.TryParse(Expr, out newInt32))
                result = newInt32;

            return result;
        }
        public static bool IsNullOrEmpty(this string Expr) => string.IsNullOrEmpty(Expr);
    }
}
