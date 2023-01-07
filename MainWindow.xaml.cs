using QPacity.Properties;
using QPacity.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QPacity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Initalise the window and its view model
            InitializeComponent();
            DataContext = new WindowViewModel();

            PopUpAggregator.OnRoomDimensionsPopUpCreation += ShowRoomDimensionsPopUp;
            PopUpAggregator.OnSettingsPopUpCreation += ShowSettingsPopUp;
            PopUpAggregator.OnQRCodeGeneratorPopUpCreation += ShowQRCodeGeneratorPopUp;
            PopUpAggregator.OnCurrentGuestsPopUpCreation += ShowCurrentGuestsPopUp;
            PopUpAggregator.OnHelpPopUpCreation += ShowHelpPopUp;

            // Begin the application with pop-ups in a closed state
            Settings.Default.PopUpOpen = false;
            Settings.Default.Save();
        }

        /// <summary>
        /// Displays a room dimensions pop-up.
        /// </summary>
        private void ShowRoomDimensionsPopUp()
        {
            RoomDimensionsPopUp popUp = new RoomDimensionsPopUp();

            MainGrid.Children.Add(popUp);
            Grid.SetRowSpan(popUp, 10);
            popUp.VerticalAlignment = VerticalAlignment.Stretch;
            popUp.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        /// <summary>
        /// Displays a settings pop-up.
        /// </summary>
        private void ShowSettingsPopUp()
        {
            SettingsPopUp popUp = new SettingsPopUp();

            MainGrid.Children.Add(popUp);
            Grid.SetRowSpan(popUp, 10);
            popUp.VerticalAlignment = VerticalAlignment.Stretch;
            popUp.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        /// <summary>
        /// Displays a QR code generator pop-up.
        /// </summary>
        private void ShowQRCodeGeneratorPopUp()
        {
            QRCodeGeneratorPopUp popUp = new QRCodeGeneratorPopUp();

            MainGrid.Children.Add(popUp);
            Grid.SetRowSpan(popUp, 10);
            popUp.VerticalAlignment = VerticalAlignment.Stretch;
            popUp.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        /// <summary>
        /// Displays a current guests pop-up.
        /// </summary>
        private void ShowCurrentGuestsPopUp()
        {
            CurrentGuestsPopUp popUp = new CurrentGuestsPopUp();

            MainGrid.Children.Add(popUp);
            Grid.SetRowSpan(popUp, 10);
            popUp.VerticalAlignment = VerticalAlignment.Stretch;
            popUp.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        /// <summary>
        /// Displays a help pop-up.
        /// </summary>
        private void ShowHelpPopUp()
        {
            HelpPopUp popUp = new HelpPopUp();

            MainGrid.Children.Add(popUp);
            Grid.SetRowSpan(popUp, 10);
            popUp.VerticalAlignment = VerticalAlignment.Stretch;
            popUp.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        /// <summary>
        /// Removes the history of a frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveFrameHistory(object sender, NavigationEventArgs e)
        {
            Frame frame = sender as Frame;
            frame.NavigationService.RemoveBackEntry();
        }

        /// <summary>
        /// If the window size is changed, set the height and width to their actual dimensions to be used in value converters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDimensions(object sender, SizeChangedEventArgs e)
        {
            Height = ActualHeight;
            Width = ActualWidth;
        }
    }
}
