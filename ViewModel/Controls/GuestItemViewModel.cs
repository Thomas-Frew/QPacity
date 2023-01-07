using QPacity.Properties;
using QPacity.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows.Input;
using System.ComponentModel;

namespace QPacity.ViewModel
{
    class GuestItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The guest's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The guest's phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Whether the guest has been reported
        /// </summary>
        public bool Reported { get; set; }

        #endregion

        #region Public Command

        /// <summary>
        /// The command for when the "-" button is pressed, to remvoe the user from the room
        /// </summary>
        public ICommand RemoveGuestCommand { get; set; }

        /// <summary>
        /// The command for when the flag button is pressed, to remvoe the user from the room
        /// </summary>
        public ICommand ReportGuestCommand { get; set; }

        /// <summary>
        /// The command for when the flag button is pressed, to remvoe the user from the room
        /// </summary>
        public ICommand BanGuestCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="window"></param>
        public GuestItemViewModel()
        {
            // Connect button commands to their functions
            RemoveGuestCommand = new RelayCommand(() => RemoveGuest());
            ReportGuestCommand = new RelayCommand(() => ReportGuest());
            BanGuestCommand    = new RelayCommand(() => BanGuest());
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Increased the ID of COVID breach emails, such that each has a unqiue identifier.
        /// </summary>
        /// <param name="sender">The sending control</param>
        /// <param name="e">The event arguments</param>
        private void IncrementBreachID(object sender, AsyncCompletedEventArgs e)
        {
            // Save that the guest has been successfully reported
            Reported = true;

            // Increment and save the breach ID
            Settings.Default.BreachID++;
            Settings.Default.Save();
        }

        /// <summary>
        /// Remove this guest from the room, disposing of the control.
        /// </summary>
        private void RemoveGuest()
        {
            // Initialise a varaible storing the guest that to be removed, this guest
            Guest guest = new Guest
            {
                Name = Name,
                PhoneNumber = PhoneNumber
            };

            // Broadcast that this guest is to be deleted
            GuestItemAggregator.BroadcastGuestRemoved(guest);
        }

        /// <summary>
        /// Report this guest for a COVID breach, altering.
        /// </summary>
        private void ReportGuest()
        {
            // Create an instance of the guest this QPacity code will be issued to
            Guest guest = new Guest
            {
                Name = Name,
                PhoneNumber = PhoneNumber
            };

            // Create a properly enabled SMTP client
            SmtpClient client = EmailHelpers.CompileSmtpClient();

            // Declare a new email the correct subject and formatting with no recipients
            MailMessage message = new MailMessage("qpacity@outlook.com", "qpacityserver@gmail.com");
            message.IsBodyHtml = true;
            message.Subject = "Reported COVID Breach #" + Settings.Default.BreachID;

            // Attach the email's header and content
            message = EmailHelpers.AttachEmailHeader(message);
            message = EmailHelpers.AttachBreachEmailContent(message, guest);

            // Hook a function upon the email successfully sending
            client.SendCompleted += new SendCompletedEventHandler(IncrementBreachID);

            try
            {
                // Attempt to send the email
                client.SendAsync(message, null);
            }
            catch
            {
                // Do nothing. Whether something should be implented here is debatable
            }
        }

        /// <summary>
        /// Bans this guest from this QPacity session.
        /// </summary>
        private void BanGuest()
        {
            // Initialise a varaible storing the guest that to be removed, this guest
            Guest guest = new Guest
            {
                Name = Name,
                PhoneNumber = PhoneNumber
            };

            GuestItemAggregator.BroadcastGuestBanned(guest);
        }

        #endregion
    }
}
