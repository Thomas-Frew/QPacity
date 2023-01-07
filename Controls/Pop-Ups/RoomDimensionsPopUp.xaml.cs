using QPacity.ViewModel;

namespace QPacity
{
    /// <summary>
    /// Interaction logic for RoomDimensions.xaml
    /// </summary>
    public partial class RoomDimensionsPopUp : BasePopUp
    {
        public RoomDimensionsPopUp()
        {
            InitializeComponent();

            // Connect the pop-up to a new instance of its ViewModel
            DataContext = new RoomDimensionsPopUpViewModel();
        }
    }
}
