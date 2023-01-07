using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace QPacity
{
    /// <summary>
    /// Interaction logic for MaskMandationBadge.xaml
    /// </summary>
    public partial class StatusBadge : UserControl
    {
        /// <summary>
        /// The initial text content of the scanning badge.
        /// </summary>
        public string InitialScanningText = "Scanning for QPacity codes";

        /// <summary>
        /// The default constructor.
        /// </summary>
        public StatusBadge()
        {
            InitializeComponent();

            // Begin all scanning animations
            BeginScanningBadgeAnimation();
            ((Storyboard)FindResource("SpinnerAnimation")).Begin();
            ((Storyboard)FindResource("SignInAnimation")).Begin();
            ((Storyboard)FindResource("SignOutAnimation")).Begin();
        }

        /// <summary>
        /// Begins the scanning badge's animation.
        /// </summary>
        private void BeginScanningBadgeAnimation()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task Clock = TimerHelpers.RunPeriodically(AnimateScanningBadgeFrame, TimeSpan.FromMilliseconds(1000), tokenSource.Token);
        }

        /// <summary>
        /// Sets the scanning badge to the next animation frame.
        /// </summary>
        private void AnimateScanningBadgeFrame()
        {
            // If the scanning badge's text has 2 periods or less...
            if (ScanningText.Text.Length < InitialScanningText.Length + 3)
            {
                // Add a period to the scanning badge's text
                ScanningText.Text += ".";
            }
            else
            {
                // Otherwise, reset the scanning badge's text
                ScanningText.Text = InitialScanningText;
            }

        }
    }
}
