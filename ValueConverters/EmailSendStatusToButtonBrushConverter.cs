using System;
using System.Globalization;
using System.Windows.Media;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in an EmailSendStatus and outputs the QPacity code issuing button's content
    /// </summary>
    public class EmailSendStatusToButtonBrushConverter : BaseValueConverter<EmailSendStatusToButtonBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return content of the QPacity code issuing button depending on its sending status
            switch ((EmailSendStatus)value)
            {
                // If a code is not being issued, or by default...
                case EmailSendStatus.Idle:
                default:
                    // Return a black background
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#151515"));

                // If a code is sending...
                case EmailSendStatus.Sending:
                    // Return a blue background
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#3296DD"));

                // If a code has successfully sent...
                case EmailSendStatus.Success:
                    // Return a green background
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#60CC40"));

                // If a code failed to send, in any way...
                case EmailSendStatus.FaliureGeneric:
                case EmailSendStatus.FaliurePhoneNumber:
                case EmailSendStatus.FaliureEmail:
                    // Return a red background
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FB3640"));
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
