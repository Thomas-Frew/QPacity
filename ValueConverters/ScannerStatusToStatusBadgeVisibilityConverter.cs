using System;
using System.Globalization;
using System.Windows;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a RoomShape index and returns the visibility for any room shape's parameters.
    /// </summary>
    public class ScannerStatusToStatusBadgeVisibilityConverter : BaseValueConverter<ScannerStatusToStatusBadgeVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the parameter's is the same as the currently selected index, show the control.
            if ((ScannerStatus)value == (ScannerStatus)parameter)
            {
                return Visibility.Visible;
            }
            // Otherwise, hide it.
            else
            {
                return Visibility.Collapsed;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
