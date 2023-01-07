using System;
using System.Globalization;
using System.Windows.Media;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in an RGB string such as FF00FF and converts it to a WPF brush
    /// </summary>
    public class ProgressPercentageToBrushConverter : BaseValueConverter<ProgressPercentageToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Count the number of days since the message was sent, capped at 10 days
            double percentage = System.Convert.ToDouble(value);

            // If the percentage is infinity, set it to zero
            if (double.IsInfinity (percentage)) { percentage = 0; }
            if (double.IsNaN(percentage)) { percentage = 0; }

            // Increase red as the message gets older, green as it gets younger (see Desmos configuration)
            int R = System.Convert.ToInt32(Math.Round(220 - 0.018 * (percentage - 100) * (percentage - 100)));
            int G = System.Convert.ToInt32(Math.Round(220 - 0.018 * percentage * percentage));

            Color myColor = Color.FromRgb((byte)R, (byte)G, 40);

            return "#FF" + myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
