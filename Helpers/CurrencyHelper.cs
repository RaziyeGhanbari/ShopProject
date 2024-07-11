using System.Globalization;

namespace ShopProject.Helpers
{
    public static class CurrencyHelper
    {
        public static string FormatPrice(decimal? price)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:N0} ریال", price);
        }
    }
    
}
