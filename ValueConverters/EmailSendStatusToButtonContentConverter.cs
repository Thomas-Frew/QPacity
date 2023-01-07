using System;
using System.Globalization;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in an EmailSendStatus and outputs the QPacity code issuing button's content
    /// </summary>
    public class EmailSendStatusToButtonContentConverter : BaseValueConverter<EmailSendStatusToButtonContentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return content of the QPacity code issuing button depending on its sending status
            switch ((EmailSendStatus)value)
            {
                // If a code is not being issued, or by default...
                case EmailSendStatus.Idle:
                default:
                    return "Issue QPacity Code";

                // If a code is sending...
                case EmailSendStatus.Sending:
                    return "Sending...";

                // If a code has successfully sent...
                case EmailSendStatus.Success:
                    return "Code Successfully Issued";

                // If a code failed to send...
                case EmailSendStatus.FaliureGeneric:
                    return "Code Failed To Send";

                // If a code failed to send...
                case EmailSendStatus.FaliurePhoneNumber:
                    return "Invalid Phone Number";

                // If a code failed to send...
                case EmailSendStatus.FaliureEmail:
                    return "Invalid Email";
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
