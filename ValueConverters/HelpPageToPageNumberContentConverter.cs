using System;
using System.Globalization;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a HelpPage index and returns the content of the page number display on the help page.
    /// </summary>
    public class HelpPageToPageNumberContentConverter : BaseValueConverter<HelpPageToPageNumberContentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Use the enumerable index of the current page to return the content of the page number display
            return "Page " + (((int)((HelpPage)value)) + 1).ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
