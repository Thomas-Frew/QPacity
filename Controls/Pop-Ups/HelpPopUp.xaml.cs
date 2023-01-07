using QPacity.ViewModel;
using System.Windows.Navigation;

namespace QPacity
{
    /// <summary>
    /// Interaction logic for RoomDimensions.xaml
    /// </summary>
    public partial class HelpPopUp : BasePopUp
    {
        public HelpPopUp()
        {
            InitializeComponent();

            // Connect the pop-up to a new instance of its ViewModel
            DataContext = new HelpPopUpViewModel();
        }

        /// <summary>
        /// Removes the history of a frame.
        /// </summary>
        /// <param name="sender">The sending frame</param>
        /// <param name="e">The frame's sending</param>
        private void RemoveFrameHistory(object sender, NavigationEventArgs e)
        {
            System.Windows.Controls.Frame frame = sender as System.Windows.Controls.Frame;
            frame.NavigationService.RemoveBackEntry();
        }
    }
}
