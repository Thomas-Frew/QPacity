using QPacity.ViewModel.Base;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System;

namespace QPacity.ViewModel
{
    class HelpPopUpViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The current help page to be displayed.
        /// </summary>
        public HelpPage CurrentPage { get; set; } = HelpPage.Introduction;

        #endregion

        #region Public Commands

        /// <summary>
        /// The command for when the right arrow button is pressed, to switch to the next help page.
        /// </summary>
        public ICommand IncrementPageCommand { get; set; }
        /// <summary>
        /// The command for when the left arrow button is pressed, to switch to the previous help page.
        /// </summary>
        public ICommand DecrementPageCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="window"></param>
        public HelpPopUpViewModel()
        {
            // Connect the help page navigation buttons to their commands
            IncrementPageCommand = new RelayCommand(() => IncrementPage());
            DecrementPageCommand = new RelayCommand(() => DecrementPage());

            // Core pop-up functionality connection
            ClosePopUpCommand = new RelayCommand(() => ClosePopUp());
        }

        /// <summary>
        /// Switches the to next help page.
        /// </summary>
        private void IncrementPage()
        {
            // Increment the current page, unless it is the conclusion, where the page should loop to the introduction
            CurrentPage = CurrentPage != HelpPage.Conclusion ? CurrentPage + 1 : HelpPage.Introduction;
        }

        /// <summary>
        /// Switches the to next help page.
        /// </summary>
        private void DecrementPage()
        {
            // Decrement the current page, unless it is the introduction, where the page should loop to the conclusion
            CurrentPage = CurrentPage != HelpPage.Introduction ? CurrentPage - 1 : HelpPage.Conclusion;
        }


        #endregion

        #region Private Helpers


        #endregion

        #region Pop-Up Core

        /// <summary>
        /// The command for when a pop-up is closed
        /// </summary>
        public ICommand ClosePopUpCommand { get; set; }

        /// <summary>
        /// Closes the current pop-up.
        /// </summary>
        private void ClosePopUp()
        {
            PopUpAggregator.BroadcastDeletion();
        }

        #endregion
    }
}
