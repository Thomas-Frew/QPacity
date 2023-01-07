using System;
using System.Globalization;
using System.Windows.Media;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a RoomShape index and returns the visibility for any room shape's parameters.
    /// </summary>
    public class ScannerStatusToEnabledButtonContentConverter : BaseValueConverter<ScannerStatusToEnabledButtonContentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the passed ScannerStatus is Scanning...
            if ((ScannerStatus)value == ScannerStatus.Scanning)
            {
                // Return that the scanner is enabled
                return "Scanner Enabled";
            }
            else
            {
                // Return that the scanner is disabled
                return "Scanner Disabled";
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
