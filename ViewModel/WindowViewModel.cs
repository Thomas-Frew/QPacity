using QPacity.ViewModel.Base;
using System.Windows.Input;
using System;
using QPacity.Properties;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZXing;
using AForge.Video.DirectShow;
using System.Drawing;
using AForge.Video;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace QPacity.ViewModel
{
    class WindowViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The window's minimum width.
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 900;

        /// <summary>
        /// The window's minimum height.
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 650;

        /// <summary>
        /// The window's current height.
        /// </summary>
        public double WindowHeight { get; set; }

        /// <summary>
        /// A private variable room's current count of people.
        /// </summary>
        private double _currentCapacity { get; set; } = 0;

        /// <summary>
        /// The room's current count of people.
        /// </summary>
        public double CurrentCapacity
        {
            get
            {
                return _currentCapacity;
            }
            set
            {
                _currentCapacity = value;

                // Refresh the interface as neccessary
                RefreshAlertBadges();
                ResetStatusBadgeState();

                UpdateCurrentCapacityPercentage();

                UpdateCurrentGuests();
            }
        }

        /// <summary>
        /// A private variable storing the room's capacity (in people).
        /// </summary>
        private double _maximumCapacity = 0;

        /// <summary>
        /// The room's capacity (in people).
        /// </summary>
        public double MaximumCapacity
        {
            get
            {
                return _maximumCapacity;
            }
            set
            {
                _maximumCapacity = value;

                // Refresh the interface as neccessary
                RefreshAlertBadges();
                ResetStatusBadgeState();

                UpdateCurrentCapacityPercentage();
            }
        }

        /// <summary>
        /// The current percentage of people in the room, as a percentage relative to the room's total capacity.
        /// </summary>
        public double CurrentCapacityPercentage { get; set; }

        /// <summary>
        /// Whether users should be alterted to wear a mask, through a MaskMandationBage.
        /// </summary>
        public bool ShowMaskMandationBadge { get; set; } = false;

        /// <summary>
        /// Whether users should be alterted that this is a COVID management plan-protected event, through a CovidManagementPlanBadge.
        /// </summary>
        public bool ShowCOVIDManagementPlanBadge { get; set; } = false;

        /// <summary>
        /// The scanner's current status, which defaults to active scanning
        /// </summary>
        public ScannerStatus ScannerStatus { get; set; } = ScannerStatus.Scanning;

        /// <summary>
        /// The name of the most recent guest to sign in or out.
        /// </summary>
        public string LatestGuest { get; set; }

        /// <summary>
        /// An ovservable collection of the guests currently in the room.
        /// </summary>
        public ObservableCollection<Guest> Guests { get; set; } = new ObservableCollection<Guest>();

        /// <summary>
        /// An ovservable collection of currently blacklisted guests.
        /// </summary>
        public ObservableCollection<Guest> BlacklistedGuests { get; set; } = new ObservableCollection<Guest>();

        #endregion Public Commands

        #region Public Commands

        /// <summary>
        /// The command for when the "Sign In User" button is pressed, to add a user to the counter
        /// </summary>
        public ICommand SignInUserCommand { get; set; }

        /// <summary>
        /// The command for when the "X" button is pressed, to remove a user to the counter
        /// </summary>
        public ICommand SignOutUserCommand { get; set; }

        /// <summary>
        /// The command for when the arrows button is pressed, to remove a user to the counter
        /// </summary>
        public ICommand OpenRoomDimensionsPopUpCommand { get; set; }

        /// <summary>
        /// The command for when the cog button is pressed, to remove a user to the counter
        /// </summary>
        public ICommand OpenSettingsPopUpCommand { get; set; }

        /// <summary>
        /// The command for when the QR code button is pressed, to remove a user to the counter
        /// </summary>
        public ICommand OpenQRCodeGeneratorPopUpCommand { get; set; }

        /// <summary>
        /// The command for when the people button is pressed, to view the list of users in the room
        /// </summary>
        public ICommand OpenCurrentGuestsPopUpCommand { get; set; }

        /// <summary>
        /// The command for when the "?" button is pressed, to view the official COVID restrictions in Chrome
        /// </summary>
        public ICommand OpenHelpPopUpCommand { get; set; }

        /// <summary>
        /// The command for when the book button is pressed, to view the official COVID restrictions in Chrome
        /// </summary>
        public ICommand OpenOfficialCOVIDRestrictionsCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel()
        {
            // Setup the QR code scanner
            SetUpCaptureDevice();

            // Setup the maximum capacity and current capacity percentage
            UpdateMaximumCapacity();
            UpdateCurrentCapacityPercentage();

            // Connect the pop up buttons to their commands
            OpenRoomDimensionsPopUpCommand = new RelayCommand(() => OpenRoomDimensionsPopUp());
            OpenSettingsPopUpCommand = new RelayCommand(() => OpenSettingsPopUp());
            OpenQRCodeGeneratorPopUpCommand = new RelayCommand(() => OpenQRCodeGeneratorPopUp());
            OpenCurrentGuestsPopUpCommand = new RelayCommand(() => OpenCurrentGuestsPopUp());
            OpenHelpPopUpCommand = new RelayCommand(() => OpenHelpPopUp());

            // Connect the COVID restrictions button to its command
            OpenOfficialCOVIDRestrictionsCommand = new RelayCommand(() => OpenOfficialCOVIDRestrictions());

            // Since the Current Guests pop-up is hosted on-site, close pop-up functionity must also be implmeneted
            ClosePopUpCommand = new RelayCommand(() => ClosePopUp());

            // If the room's area is changed...
            Settings.Default.PropertyChanged += (sender, e) => 
            { if (e.PropertyName == "RoomArea" || e.PropertyName == "Density" || e.PropertyName == "CapacityCap" || e.PropertyName == "DefaultScannerStatus")
                {
                    ResetStatusBadgeState();
                    RefreshAlertBadges();
                    UpdateMaximumCapacity();
                }
            };

            // Hook into guest removal to manage this on their pop-up
            GuestItemAggregator.OnGuestRemoved += PassGuestSignOut;
            GuestItemAggregator.OnGuestBanned += BanGuest;
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Adds a user to the user counter.
        /// </summary>
        private void IncrementCapacityCounter()
        {
            // If the current capacity is not exceed...
            if (CurrentCapacity < MaximumCapacity)
            {
                // Increment the person counter
                CurrentCapacity++;
            }
        }

        /// <summary>
        /// Removes a user to the user counter.
        /// </summary>
        private void DecrementCapacityCounter()
        {
            // If the current capacity is not exceed...
            if (CurrentCapacity > 0)
            {
                // Decrement the person counter
                CurrentCapacity--;
            }
        }

        /// <summary>
        /// Updates the percentage the room is occupied.
        /// </summary>
        private async void UpdateCurrentCapacityPercentage()
        {
            int frames = 50;

            // Save the old capacity percentage to a new variable
            double oldCapacityPercentage = CurrentCapacityPercentage;

            // Calaculate the new capacity percentage and write it to a new variable
            double newCapacityPercentage = (CurrentCapacity / MaximumCapacity) * 100;

            double percentageDifference = newCapacityPercentage - oldCapacityPercentage;

            for (int i = frames; i > 0; i--)
            {
                CurrentCapacityPercentage += percentageDifference / frames;
                await Task.Delay(1);
            }

            // Find the current capacity of the room as a fraction of the total, then mulitply by 100 to get the percetnage
            CurrentCapacityPercentage = (CurrentCapacity / MaximumCapacity) * 100;
        }

        /// <summary>
        /// Reassessed whether alert badges should be displayed, and displays them, if needed.
        /// </summary>
        private void RefreshAlertBadges()
        {
            // Load the current restriction level, as its appropriate variable type
            RestrictionLevel level = (RestrictionLevel)Settings.Default.RestrictionLevelIndex;

            // Initialise a new list of strings to store the current COVID level's restriction data
            List<string> restrictionData = DatabaseHelpers.FindRestrictionData(level);

            // If this COVID restriction level requires mask, or if the room's capacity cap has been reached, display a badge to wear masks.
            ShowMaskMandationBadge = (restrictionData[(int)RProp.RequiresMasks] == "1" || CurrentCapacity >= MaximumCapacity);

            // If this COVID restriction level can host CMP events, and if the threshold for this event is exceeded, display a badge to alert users that this is a CMP event.
            ShowCOVIDManagementPlanBadge = (restrictionData[(int)RProp.HasCMPEvents] == "1" && CurrentCapacity >= Convert.ToInt16(restrictionData[(int)RProp.CMPThreshold]));
        }

        /// <summary>
        /// Reculates the room's maximum capacity based on its area, density and capacity caps
        /// </summary>
        private void UpdateMaximumCapacity()
        {
            // Initialise a double variable to store the room's theoretical capacity: its area mulitplied by its density
            double theoreticalCapacity = Settings.Default.RoomArea * Settings.Default.Density;

            // Initialise a double variable to store the room's actual capacity, which takes into account capacity caps and rounds down to the nearest person (floor)
            double actualCapacity = Math.Floor(Math.Min(theoreticalCapacity, Settings.Default.CapacityCap));

            // Set MaximumCapacity to the room's actual capacity
            MaximumCapacity = actualCapacity;
        }

        /// <summary>
        /// Opens the SA's official COVID restrictions in Chrome.
        /// </summary>
        private void OpenOfficialCOVIDRestrictions()
        {
            // Initialise the link to SA's current activity restrictions.
            string link = "https://www.covid-19.sa.gov.au/restrictions-and-responsibilities/activities-and-gatherings/current-activity-restrictions";

            // Open a new chrome tab with this page's link.
            Process.Start("chrome.exe", link);
        }

        #endregion

        #region Scanner Core

        /// <summary>
        /// A collection of devices which can capture camera input (we will be using the first)
        /// </summary>
        public FilterInfoCollection CaptureDevices;

        /// <summary>
        /// The most recent frame from the current capture device
        /// </summary>
        public VideoCaptureDevice CurrentFrame;

        /// <summary>
        /// The most recent frame from the current capture device, as a bitmap
        /// </summary>
        public Bitmap CurrentFrameBitmap = new Bitmap(100, 100);

        #region Scanner Setup

        /// <summary>
        /// Sets up the video capturing device and QR code scanning loop.
        /// </summary>
        private void SetUpCaptureDevice()
        {
            // Find all capture devices on this computer
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // Use the first capture device we have available
            CurrentFrame = new VideoCaptureDevice(CaptureDevices[0].MonikerString);

            // Attach the HandleFrameData function to whenever this device captures a new frame
            CurrentFrame.NewFrame += new NewFrameEventHandler(HandleFrameData);

            // Begin the capturing device
            CurrentFrame.Start();

            // Begin the QR code scanning loop, which uses the capturing device's content
            BeginScannerLoop();
        }

        /// <summary>
        /// Initialises the QR code scanning loop, which runs every 0.1 seconds as long as the app is open.
        /// </summary>
        private void BeginScannerLoop()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task Clock = TimerHelpers.RunPeriodically(ScanQPacityCode, TimeSpan.FromMilliseconds(1000), tokenSource.Token);
        }

        /// <summary>
        /// Saves the current frame's data, as read by the capturing device, to CurrentFrameBitmap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void HandleFrameData(object sender, NewFrameEventArgs eventArgs)
        {
            CurrentFrameBitmap = (Bitmap)eventArgs.Frame.Clone();
        }

        #endregion

        /// <summary>
        /// Checks the current video frame for a QPacity code, and handles a user signing in if it sees one
        /// </summary>
        private async void ScanQPacityCode()
        {
            // Create a new reader to look for the QR code
            BarcodeReader qrReader = new BarcodeReader();

            /// [DISABLED AS THEY SLOW THE CODE] Setup the QR reader's options to make it look as best as possible 
            /// qrReader.AutoRotate = qrReader.TryInverted = true;
            /// qrReader.Options = new DecodingOptions { TryHarder = true };

            // Attempt to decode the QR code in the current frame, returning null if one cannot be found
            var result = qrReader.Decode(CurrentFrameBitmap);

            // If a QR code was found, and the application is scanning for more quests...
            if (result != null && (ScannerStatus == ScannerStatus.Scanning || ScannerStatus == ScannerStatus.SignIn))
            {
                // Get the QR code's raw content as a string
                string rawQRData = result.ToString().Trim();

                // Derive the QR code data
                List<string> qrData = rawQRData.Split('#').ToList();

                // If the QR code is a QPacity code (header = QPacityCode) andf there are three items...
                if (IsCodeValid(qrData) && qrData.Count == 3)
                {
                    // Initialise a new variable to store this guest's data
                    Guest guest = new Guest();

                    // Setup the guest data by loading from the QR code's data
                    guest.Name = qrData[(int)QProp.Name];
                    guest.PhoneNumber = qrData[(int)QProp.PhoneNumber];

                    // If the user is not on the blacklist...
                    if (!BlacklistedGuests.Any(data => data.Equals(guest)))
                    {
                        // If the user is not currently in the room...
                        if (!Guests.Any(data => data.Equals(guest)))
                        {
                            // Handle the user being signed in
                            await SignInGuest(guest);
                        }
                        else
                        {
                            // Handle the user being signed out
                            await SignOutGuest(guest);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a QPacity is valid by checking its header has been appropriatley calculated
        /// </summary>
        private bool IsCodeValid(List<string> qrData)
        {
            // Find the first and last digits of this QPacity code's phone number
            string phoneNumber = qrData[(int)QProp.PhoneNumber];

            // Find the first and last digits of this QPacity code's phone number
            char firstChar = phoneNumber[0];
            char lastChar = phoneNumber[phoneNumber.Length - 1];

            // Calculate the check-number for this QPacity code, using the formula F^L + L^F - 1
            int checkNumber = firstChar * lastChar - 1;

            // If the header and check number match, the code is valid, and return true
            return Convert.ToInt32(qrData[(int)QProp.Header]) == checkNumber;
        }

        /// <summary>
        /// Handles a guest being signed into the room.
        /// </summary>
        /// <param name="guest">The guest being signed in</param>
        /// <returns>A notification that the sign-in has completed</returns>
        private async Task SignInGuest(Guest guest)
        {
            // Add this guest's data to the list
            Guests.Add(guest);

            // The latest guest in the one which just signed in
            LatestGuest = guest.Name;

            // Increment the capacity counter
            IncrementCapacityCounter();

            // Display the user has signed in by changing the StatusBadge's apperance
            await SuspendStatusBadgeState(ScannerStatus.SignIn);
        }

        /// <summary>
        /// Asynchronously signs a guest out.
        /// </summary>
        /// <param name="guest">The guest to be signed out.</param>
        private async void PassGuestSignOut(Guest guest)
        {
            // Asynchronously sign a guest out
            await SignOutGuest(guest);
        }

        /// <summary>
        /// Rejects a guest that has already signed into the room.
        /// </summary>
        /// <param name="guest">The guest being signed in</param>
        /// <returns>A notification that the sign-in has completed</returns>
        private async Task SignOutGuest(Guest guest)
        {
            // Add this guest's data to the list
            Guests.Remove(Guests.Where(data => data.Equals(guest)).First());

            // The latest guest in the one which just signed out
            LatestGuest = guest.Name;

            // Increment the capacity counter
            DecrementCapacityCounter();

            // Display the user has signed in by changing the StatusBadge's apperance
            await SuspendStatusBadgeState(ScannerStatus.SignOut);
        }

        #region Status Badge Management

        /// <summary>
        /// Sets the status badge's state to the passed state for 2000 milliseconds.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private async Task SuspendStatusBadgeState(ScannerStatus state)
        {
            // Set the StatusBadge to its Confirm state
            ScannerStatus = state;

            // Wait 2 seconds
            await Task.Delay(2000);

            // Reset the StatusBadge to its appropaiate status
            ResetStatusBadgeState();
        }

        /// <summary>
        /// Resets the status badge to its appropriate state, either accepting (blue) or rejecting (black) users
        /// </summary>
        private void ResetStatusBadgeState()
        {
            // If current capacity has not reached maximum, let the app continue scanning, otherwise, stop it from scanning.
            ScannerStatus = CurrentCapacity != MaximumCapacity ? (ScannerStatus)Settings.Default.DefaultScannerStatus : ScannerStatus.DisabledCapacity;
        }

        #endregion

        #endregion

        #region Current Guests Pop-Up

        /// <summary>
        /// An observable collection storing the GuestItems displayed in the "Current Guests".
        /// </summary>
        public ObservableCollection<GuestItemViewModel> GuestItems { get; set; } = new ObservableCollection<GuestItemViewModel>();

        /// <summary>
        /// Loads all guests, in this current room, into the "Current Guests" pop-up
        /// </summary>
        private void UpdateCurrentGuests()
        {
            // Clear the list of guest item
            GuestItems.Clear();

            // For every guest in the list of guests who have signed in...
            foreach (Guest guest in Guests)
            {
                DisplayGuest(guest);
            }
        }

        /// <summary>
        /// Adds a guest to the "Current Guests" pop-up as as GuestItemViewModel.
        /// </summary>
        /// <param name="guest">The guest to be added</param>
        private void DisplayGuest(Guest guest)
        {
            // Create a new GuestItemViewModel for the current guest
            GuestItemViewModel addedGuestItem = new GuestItemViewModel
            {
                Name = guest.Name,
                PhoneNumber = guest.PhoneNumber
            };

            // Adds the current guest's view model to the list of GuestItemViewModel
            GuestItems.Add(addedGuestItem);
        }

        /// <summary>
        /// Handles a guest being banned, kicking them out and adding them to the blacklist
        /// </summary>
        /// <param name="guest">The guest to be banned</param>
        private async void BanGuest(Guest guest)
        {
            BlacklistedGuests.Add(guest);
            await SignOutGuest(guest);
        }

        #endregion

        #region Pop-Up Core

        /// <summary>
        /// Opens a new room dimensions pop-up, if one is not already open.
        /// </summary>
        private void OpenRoomDimensionsPopUp()
        {
            // If a pop-up is not already open...
            if (!Settings.Default.PopUpOpen)
            {
                PopUpAggregator.BroadcastRoomDimensionsPopUpCreation();
            }
        }

        /// <summary>
        /// Opens a new settings pop-up, if one is not already open.
        /// </summary>
        private void OpenSettingsPopUp()
        {
            // If a pop-up is not already open...
            if (!Settings.Default.PopUpOpen)
            {
                PopUpAggregator.BroadcastSettingsPopUpCreation();
            }
        }

        /// <summary>
        /// Opens a new QR code generator pop-up, if one is not already open.
        /// </summary>
        private void OpenQRCodeGeneratorPopUp()
        {
            // If a pop-up is not already open...
            if (!Settings.Default.PopUpOpen)
            {
                PopUpAggregator.BroadcastQRCodeGeneratorPopUpCreation();
            }
        }

        /// <summary>
        /// Opens a new current guests pop-up, if one is not already open.
        /// </summary>
        private void OpenCurrentGuestsPopUp()
        {
            // If a pop-up is not already open...
            if (!Settings.Default.PopUpOpen)
            {
                PopUpAggregator.BroadcastCurrentGuestsPopUpCreation();
            }
        }

        /// <summary>
        /// Opens a new help pop-up, if one is not already open
        /// </summary>
        private void OpenHelpPopUp()
        {
            // If a pop-up is not already open...
            if (!Settings.Default.PopUpOpen)
            {
                PopUpAggregator.BroadcastHelpPopUpCreation();
            }
        }

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
