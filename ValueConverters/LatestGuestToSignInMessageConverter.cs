using System;
using System.Globalization;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in the latest guest's name and outputs a welcome message customised to them.
    /// </summary>
    public class LatestGuestToSignInMessageConverter : BaseValueConverter<LatestGuestToSignInMessageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Integrates the latest guest's name into the appropriate welcome message
            return "Welcome, " + (string)value + ".";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
