using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using System;
using QPacity.Animation;
using QPacity.Properties;

namespace QPacity
{
    /// <summary>
    /// A base interface for all interfaces to gain base functionality.
    /// </summary>
    public class BasePage : Page
    {
        #region Public Properties

        /// <summary>
        /// The animation the play when the page is first loaded.
        /// </summary>
        public StoryboardAnimation PageLoadAnimation { get; set; } = StoryboardAnimation.SlideAndFadeInFromLeft;

        /// <summary>
        /// The animation the play when the page is unloaded.
        /// </summary>
        public StoryboardAnimation PageUnloadAnimation { get; set; } = StoryboardAnimation.SlideAndFadeOutToRight;

        /// <summary>
        /// The time any slide animation takes to complete.
        /// </summary>
        public float SlideSeconds { get; set; } = 1f;

        #endregion

        #region Public Events

        public static event EventHandler PageAnimatedOut;

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor.
        /// </summary>
        public BasePage()
        {
            // If we are animating in, hide to begin with
            if (PageLoadAnimation != StoryboardAnimation.None)
                Visibility = Visibility.Collapsed;

            // Listen out for the page loading
            Loaded += BaseInterface_Loaded;

            // If the selected keyboard changes, animate out and tell the window view model to change the page.
            Settings.Default.PropertyChanged += async (sender, e) =>
            {
                if (e.PropertyName == "CurrentPage" && Settings.Default.CurrentPage != Settings.Default.PreviousPage)
                {
                    await AnimateOut();
                    PageAnimatedOut(this, EventArgs.Empty);
                }
            };
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any required animation.
        /// </summary>
        /// <param name="sender">The sending control</param>
        /// <param name="e">The event arguments</param>
        private async void BaseInterface_Loaded(object sender, RoutedEventArgs e)
        {
            // Animate the page in
            await AnimateIn();
        }

        /// <summary>
        /// Animate the interface in.
        /// </summary>
        /// <returns></returns>
        public async Task AnimateIn()
        {
            // Make sure we have something to do
            if (PageLoadAnimation == StoryboardAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case Animation.StoryboardAnimation.SlideAndFadeInFromLeft:

                    // Start the animation
                    await this.SlideAndFadeInFromLeft(SlideSeconds);

                    break;
            }

        }

        /// <summary>
        /// Animate the interface out.
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOut()
        {
            // Make sure we have something to do
            if (PageUnloadAnimation == StoryboardAnimation.None)
                return;

            switch (PageUnloadAnimation)
            {
                case Animation.StoryboardAnimation.SlideAndFadeOutToRight:

                    // Start the animation
                    await this.SlideAndFadeOutToRight(SlideSeconds);
                    break;
            }
        }

        #endregion
    }
}