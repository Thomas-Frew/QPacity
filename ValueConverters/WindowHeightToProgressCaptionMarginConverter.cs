using QPacity.Properties;
using System;
using System.Globalization;
using System.Windows;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in an RGB string such as FF00FF and converts it to a WPF brush
    /// </summary>
    public class WindowHeightToProgressCaptionMarginConverter : BaseValueConverter<WindowHeightToProgressCaptionMarginConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Returns the appropriate height
            return new Thickness(0, ConverterHelpers.WindowHeightToProgressBarHeight((double)value) + 10, 0, 0);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
