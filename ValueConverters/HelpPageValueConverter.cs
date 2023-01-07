using System;
using System.Globalization;

namespace QPacity
{
    /// <summary>
    /// A converter that takes in a HelpPage index and returns the HelpPopUp's appropriate content
    /// </summary>
    public class HelpPageValueConverter : BaseValueConverter<HelpPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return different help page content, based on the passed HelpPage/
            switch ((HelpPage)value)
            {
                case HelpPage.Introduction:
                    return new IntroductionHelp();

                case HelpPage.RoomDimensions:
                    return new RoomDimensionsHelp();

                case HelpPage.Settings:
                    return new SettingsHelp();

                case HelpPage.Interface:
                    return new InterfaceHelp();

                case HelpPage.Scanner:
                    return new ScannerHelp();

                case HelpPage.CodeGenerator:
                    return new CodeGeneratorHelp();

                case HelpPage.CurrentGuests:
                    return new CurrentGuestsHelp();

                case HelpPage.Conclusion:
                    return new ConclusionHelp();

                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
