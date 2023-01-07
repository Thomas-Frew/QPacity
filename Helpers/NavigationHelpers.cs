using QPacity.Properties;
using System;

namespace QPacity
{
    /// <summary>
    /// A series of aggregators for pop ups.
    /// </summary>
    public class PopUpAggregator
    {
        // Initialise public events to be received when a pop-up is created or deleted.
        public static Action OnRoomDimensionsPopUpCreation;
        public static Action OnSettingsPopUpCreation;
        public static Action OnQRCodeGeneratorPopUpCreation;
        public static Action OnCurrentGuestsPopUpCreation;
        public static Action OnHelpPopUpCreation;
        public static Action OnDeletion;

        /// <summary>
        /// Broadcasts that an "Room Dimensions" pop-up has been created.
        /// </summary>
        public static void BroadcastRoomDimensionsPopUpCreation()
        {
            // Close any open pop-ups
            BroadcastDeletion();

            // Save that a pop-up has been opened
            Settings.Default.PopUpOpen = true;
            Settings.Default.Save();

            // Call the event to open this pop-up
            OnRoomDimensionsPopUpCreation?.Invoke();
        }

        /// <summary>
        /// Broadcasts that an "Settings" pop-up has been created.
        /// </summary>
        public static void BroadcastSettingsPopUpCreation()
        {
            // Close any open pop-ups
            BroadcastDeletion();

            // Save that a pop-up has been opened
            Settings.Default.PopUpOpen = true;
            Settings.Default.Save();

            // Call the event to open this pop-up
            OnSettingsPopUpCreation?.Invoke();
        }

        /// <summary>
        /// Broadcasts that an "QR Code" pop-up has been created.
        /// </summary>
        public static void BroadcastQRCodeGeneratorPopUpCreation()
        {
            // Close any open pop-ups
            BroadcastDeletion();

            // Save that a pop-up has been opened
            Settings.Default.PopUpOpen = true;
            Settings.Default.Save();

            // Call the event to open this pop-up
            OnQRCodeGeneratorPopUpCreation?.Invoke();
        }

        /// <summary>
        /// Broadcasts that an "Current Guests" pop-up has been created.
        /// </summary>
        public static void BroadcastCurrentGuestsPopUpCreation()
        {
            // Close any open pop-ups
            BroadcastDeletion();

            // Save that a pop-up has been opened
            Settings.Default.PopUpOpen = true;
            Settings.Default.Save();

            // Call the event to open this pop-up
            OnCurrentGuestsPopUpCreation?.Invoke();
        }

        /// <summary>
        /// Broadcasts that an "Help" pop-up has been created.
        /// </summary>
        public static void BroadcastHelpPopUpCreation()
        {
            // Close any open pop-ups
            BroadcastDeletion();

            // Save that a pop-up has been opened
            Settings.Default.PopUpOpen = true;
            Settings.Default.Save();

            // Call the event to open this pop-up
            OnHelpPopUpCreation?.Invoke();
        }  

        /// <summary>
        /// Broadcasts that a pop-up has been deleted.
        /// </summary>
        public static void BroadcastDeletion()
        {
            // Save that a pop-up has been closed
            Settings.Default.PopUpOpen = false;
            Settings.Default.Save();

            // Call the event to close ANY active pop-up
            OnDeletion?.Invoke();
        }
    }

    /// <summary>
    /// A series of aggregators for GuestItem management
    /// </summary>
    public class GuestItemAggregator
    {
        // Initialise a public event to be received when a GuestItem is deleted
        public static Action<Guest> OnGuestRemoved;
        public static Action<Guest> OnGuestBanned;

        /// <summary>
        /// Broadcasts that an "Room Dimensions" pop-up has been created.
        /// </summary>
        public static void BroadcastGuestRemoved(Guest guest)
        {
            // Call the event to open this pop-up
            OnGuestRemoved?.Invoke(guest);
        }

        /// <summary>
        /// Broadcasts that an "Room Dimensions" pop-up has been created.
        /// </summary>
        public static void BroadcastGuestBanned(Guest guest)
        {
            // Call the event to open this pop-up
            OnGuestBanned?.Invoke(guest);
        }
    }
}
