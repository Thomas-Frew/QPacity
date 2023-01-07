using QPacity.Properties;
using QRCoder;
using System;
using System.Globalization;
using System.Windows;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in an RGB string such as FF00FF and converts it to a WPF brush
    /// </summary>
    public class StringToQRCodeSourceConverter : BaseValueConverter<StringToQRCodeSourceConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != string.Empty)
            {
                // Initialise a new QR code generator
                QRCodeGenerator qrGenerator = new QRCodeGenerator();

                // Generate the QR Code's data
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(value.ToString(), QRCodeGenerator.ECCLevel.Q);

                // Create a new QR code from this data
                XamlQRCode qrCode = new XamlQRCode(qrCodeData);

                // Create a new Bitmap image from this QR code
                return qrCode.GetGraphic(20, "#151515", "#f5f5f5", false);
            }
            else
            {
                return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
