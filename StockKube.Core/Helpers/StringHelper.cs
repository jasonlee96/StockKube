namespace StockKube.Core.Helpers
{
    public static class StringHelper
    {
        public static TEnum ToEnum<TEnum>(this string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }
    }
}
