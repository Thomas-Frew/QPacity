using System;
using System.Globalization;
using System.Windows;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a RoomShape index and returns the visibility for any room shape's parameters.
    /// </summary>
    public class RoomShapeIndexToRoomParameterVisibilityConverter : BaseValueConverter<RoomShapeIndexToRoomParameterVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the parameter's index is the same as the currently selected index, show it.
            if ((RoomShape)value == (RoomShape)parameter)
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
