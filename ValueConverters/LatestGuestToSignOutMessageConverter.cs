using System;
using System.Globalization;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in the latest guest's name and outputs a goodbye message customised to them.
    /// </summary>
    public class LatestGuestToSignOutMessageConverter : BaseValueConverter<LatestGuestToSignOutMessageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Integrates the latest guest's name into the appropriate goodbye message
            return "Goodbye, " + (string)value + ".";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
