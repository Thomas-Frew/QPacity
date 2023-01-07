using QPacity.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a boolean and returns either a green (active) or black (inactive) brush
    /// </summary>
    public class BooleanToBlackGreenBrushConverter : BaseValueConverter<BooleanToBlackGreenBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the passed value is true...
            if ((bool)value)
            {
                // Return a red brush
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#80DD50"));
            }
            else
            {
                // Return a black background
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#151515"));
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
