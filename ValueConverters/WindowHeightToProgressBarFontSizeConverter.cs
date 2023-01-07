using System;
using System.Globalization;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in an RGB string such as FF00FF and converts it to a WPF brush
    /// </summary>
    public class WindowHeightToProgressBarFontSizeConverter : BaseValueConverter<WindowHeightToProgressBarFontSizeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Returns the appropriate height
            return ConverterHelpers.WindowHeightToProgressBarHeight((double)value) - 20;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
