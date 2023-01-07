using System;
using System.Globalization;

namespace QPacity
{
    /// <summary>
    /// An integer to interface value converter
    /// </summary>
    public class PageValueConverter : BaseValueConverter<PageValueConverter>
    {
        /// <summary>
        /// Converts from integers to interfaces
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate interface [REPLACE WITH NEW VARIABLE]
            switch ((int)value)
            {
                // If no interface exists under the selected integer, return nothing
                default:
                    return null;
            }
        }

        /// <summary>
        /// Coverts from interfaces back to integers
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
