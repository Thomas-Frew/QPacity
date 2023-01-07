using System;
using System.Globalization;
using System.Windows.Media;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a RoomShape index and returns the visibility for any room shape's parameters.
    /// </summary>
    public class ScannerStatusToRedGreenBrushConverter : BaseValueConverter<ScannerStatusToRedGreenBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the passed ScannerStatus is Scanning...
            if ((ScannerStatus)value == ScannerStatus.Scanning)
            {
                // Return a green brush
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#60CC40"));
            }
            else
            {
                // Return a red brush
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FB3640"));
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
