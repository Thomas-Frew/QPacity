using QPacity.Properties;
using QPacity.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace QPacity.ViewModel
{
    class SettingsPopUpViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A list containing all possible South Australian COVID Restriction Levels, loaded from the custom variable type <see cref="RestrictionLevel"/> 
        /// </summary>
        public List<string> RestrictionLevels { get; set; } = Enum.GetValues(typeof(RestrictionLevel)).Cast<RestrictionLevel>().Select(s => s.ToString().Replace('_', ' ')).ToList();

        /// <summary>
        /// A private integer storing the currently selected restriction level's index, which defaults to the last selected restriction level index.
        /// </summary>
        private int _restrictionLevelIndex { get; set; } = Settings.Default.RestrictionLevelIndex;

        /// <summary>
        /// The currently selected restriction level's index.
        /// </summary>
        public int RestrictionLevelIndex
        {
            get
            {
                return _restrictionLevelIndex;
            }
            set
            {
                _restrictionLevelIndex = value;

                // Update the room's density and capacity cap
                UpdateRestrictionParameters();

                // Save all event settings
                SaveRestrictionSettings();
            }
        }

        /// <summary>
        /// A list containing all possible event types, loaded from the custom variable type <see cref="EventTypes"/> 
        /// </summary>
        public List<string> EventTypes { get; set; } = Enum.GetValues(typeof(EventType)).Cast<EventType>().Select(s => s.ToString().Replace('_', ' ')).ToList();

        /// <summary>
        /// A private integer storing the currently selected event type's index, which defaults to the last selected event type index.
        /// </summary>
        private int _eventTypeIndex { get; set; } = Settings.Default.EventTypeIndex;

        /// <summary>
        /// The currently selected event type's index.
        /// </summary>
        public int EventTypeIndex
        {
            get
            {
                return _eventTypeIndex;
            }
            set
            {
                _eventTypeIndex = value;

                // Update the room's density and capacity cap
                UpdateRestrictionParameters();

                // Save all event settings
                SaveRestrictionSettings();
            }
        }

        /// <summary>
        /// A list containing all possible seating plans, loaded from the custom variable type <see cref="SeatingPlan"/> 
        /// </summary>
        public List<string> SeatingPlans { get; set; } = Enum.GetValues(typeof(SeatingPlan)).Cast<SeatingPlan>().Select(s => s.ToString().Replace('_', ' ')).ToList();

        /// <summary>
        /// A private integer storing the currently selected seating plan's index, which defaults to the last seating plan's index.
        /// </summary>
        private int _seatingPlanIndex { get; set; } = Settings.Default.SeatingPlanIndex;

        /// <summary>
        /// The currently selected seating plan's index.
        /// </summary>
        public int SeatingPlanIndex
        {
            get
            {
                return _seatingPlanIndex;
            }
            set
            {
                _seatingPlanIndex = value;

                // Update the room's density and capacity cap
                UpdateRestrictionParameters();

                // Save all event settings
                SaveRestrictionSettings();
            }
        }

        /// <summary>
        /// The room's maximum person density (in people per meters squared).
        /// </summary>
        public double Density { get; set; } = Settings.Default.Density;

        /// <summary>
        /// The room's absolute density cap (in people).
        /// </summary>
        public double CapacityCap { get; set; } = Settings.Default.CapacityCap;

        /// <summary>
        /// The scanner's default status, (whether it is enabled or disabled), as set by the button.
        /// </summary>
        public ScannerStatus DefaultScannerStatus { get; set; } = (ScannerStatus)Settings.Default.DefaultScannerStatus;

        #endregion

        #region Public Commands

        // The command for when the multi-content, scanner status button is pressed, to invert the scanner's default status.
        public ICommand InvertDefaultScannerStatusCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="window"></param>
        public SettingsPopUpViewModel()
        {
            // Connect button commands to their methods
            InvertDefaultScannerStatusCommand = new RelayCommand(() => InvertDefaultScannerStatus());

            // Core Pop-up functionality connection
            ClosePopUpCommand = new RelayCommand(() => ClosePopUp());
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Recalcualte the COVID restriction parameters (density and capacity cap) from the currently selected event parameters.
        /// </summary>
        private void UpdateRestrictionParameters()
        {
            // Convert the COVID restriction level, event type and seating plan from their indices into their custom variable types
            RestrictionLevel level = (RestrictionLevel)RestrictionLevelIndex;
            EventType eventType = (EventType)EventTypeIndex;
            SeatingPlan seatingPlan = (SeatingPlan)SeatingPlanIndex;

            // Update the event's density and capacity cap, based on these variables, using helper functions
            Density = DatabaseHelpers.FindDensity(level, seatingPlan);
            CapacityCap = DatabaseHelpers.FindCapacityCap(level, eventType);
        }

        /// <summary>
        /// Saves all COVID restriction settings.
        /// </summary>
        private void SaveRestrictionSettings()
        {
            // Write the events's properties to the application's settings
            Settings.Default.RestrictionLevelIndex = RestrictionLevelIndex;
            Settings.Default.EventTypeIndex = EventTypeIndex;
            Settings.Default.SeatingPlanIndex = SeatingPlanIndex;

            Settings.Default.Density = Density;
            Settings.Default.CapacityCap = CapacityCap;

            // Save the settings
            Settings.Default.Save();
        }

        /// <summary>
        /// Inverts the scanner's default status and saves it to settings.
        /// </summary>
        private void InvertDefaultScannerStatus()
        {
            // If the scanner status is scanning, set it to manually disabled. Otherwise, set it to scanning.
            DefaultScannerStatus = DefaultScannerStatus == ScannerStatus.Scanning ? ScannerStatus.DisabledManual : ScannerStatus.Scanning;

            // Save this status to settings
            Settings.Default.DefaultScannerStatus = (int)DefaultScannerStatus;
            Settings.Default.Save();
        }

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
