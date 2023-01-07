using QPacity.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a RoomShape index and returns the visibility for any room shape's parameters.
    /// </summary>
    public class GuestsToPopUpElementVisbilityConverter : BaseValueConverter<GuestsToPopUpElementVisbilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the fact that are more than zero guests matches whether we should show the element when there are more than zero guests, show it.
            if (((double)value) > 0 == (bool)parameter)
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
