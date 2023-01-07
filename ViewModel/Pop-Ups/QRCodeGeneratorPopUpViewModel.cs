using QPacity.ViewModel.Base;
using System.Windows.Input;
using System.Drawing;
using System;
using QRCoder;
using System.Windows.Media;
using System.Net.Mail;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace QPacity.ViewModel
{
    class QRCodeGeneratorPopUpViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A private variable storing the QPacity code owner's name.
        /// </summary>
        public string _name { get; set; } = string.Empty;

        /// <summary>
        /// The QPacity code owner's name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                // Regenerate the QPacity code.
                UpdateQPacityCode();
            }
        }

        /// <summary>
        /// A private variable storing the QPacity code owner's phone number.
        /// </summary>
        public string _phoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The QPacity code owner's phone number.
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = Regex.Replace(value, @"[^0-9]", "");

                // Regenerate the QPacity code.
                UpdateQPacityCode();
            }
        }

        /// <summary>
        /// The QPacity code owner's email.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The QPacity code's display data.
        /// </summary>
        public DrawingImage DisplayCode { get; set; } = new DrawingImage();

        /// <summary>
        /// The QPacity code's Bitmap data (for email uploading).
        /// </summary>
        public Bitmap CodeData { get; set; }

        /// <summary>
        /// The status of the QPacity code issuing thread.
        /// </summary>
        public EmailSendStatus EmailSendStatus { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command for when the multi-content button ("Issue QPacity Code") is pressed
        /// </summary>
        public ICommand IssueQPacityCodeCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="window"></param>
        public QRCodeGeneratorPopUpViewModel()
        {
            // Create the default QR code
            UpdateQPacityCode();

            // Connect the button commands with their appropriate functions
            IssueQPacityCodeCommand = new RelayCommand(() => IssueQPacityCode());

            // Core Pop-up functionality connection
            ClosePopUpCommand = new RelayCommand(() => ClosePopUp());
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Generates a QPacity user's code and stores it as a Bitmap
        /// </summary>
        private void UpdateQPacityCode()
        {
            // Default the QPacity code owner's name to Anonymous if none has been provided
            string name = !string.IsNullOrWhiteSpace(Name) ? Name : "Anonymous";

            // Default the QPacity code owner's phone number to a space if none has been provided
            string phoneNumber = !string.IsNullOrWhiteSpace(PhoneNumber) ? PhoneNumber : " ";

            // Find the first and last digits of this QPacity code's phone number
            char firstChar = phoneNumber[0];
            char lastChar = phoneNumber[phoneNumber.Length - 1];

            // Calculate the check-number for this QPacity code, using the formula F^L + L^F - 1
            int checkNumber = firstChar * lastChar - 1;

            // Create the string for the QR code's contents
            string qrString = checkNumber.ToString() + "#" + name + "#" + phoneNumber;

            // Initialise a new QR code generator
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            // Generate the QR Code's data
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);

            // Create a newdisplay and data QR codes from this data
            XamlQRCode xamlQRCode = new XamlQRCode(qrCodeData);
            QRCode qrCode = new QRCode(qrCodeData);

            // Create a new Bitmap image from this QR code
            DisplayCode = xamlQRCode.GetGraphic(20, "#151515", "#f5f5f5", false);
            CodeData = qrCode.GetGraphic(20, "#000000", "#ffffff", true);
        }

        /// <summary>
        /// Issue a QPacity code to a guest's entered email
        /// </summary>
        private void IssueQPacityCode()
        {
            // Default the QPacity code owner's name to Anonymous if none has been provided
            string name = !string.IsNullOrWhiteSpace(Name) ? Name : "Anonymous";

            // If the phone number is empty, set it to one space so regex can pass
            string phoneNumber = PhoneNumber.Length > 0 ? PhoneNumber : " ";

            // If there are no characters of the phone remaining, then no phone number has been input...
            if (string.IsNullOrEmpty(phoneNumber))
            {
                // Hence, display that the code cannot be issued
                EmailSendStatus = EmailSendStatus.FaliurePhoneNumber;
            }
            // If there entered email does not contain an @, it is not a valid email...
            else if (!Email.Contains("@"))
            {
                // Hence, display that the code cannot be issued
                EmailSendStatus = EmailSendStatus.FaliureEmail;
            }
            else
            {
                // Create an instance of the guest this QPacity code will be issued to
                Guest guest = new Guest
                {
                    Name = name,
                    PhoneNumber = phoneNumber
                };

                // Create a properly enabled SMTP client
                SmtpClient client = EmailHelpers.CompileSmtpClient();

                // Declare a new email the correct subject and formatting with no recipients
                MailMessage message = new MailMessage("qpacity@outlook.com", Email);
                message.IsBodyHtml = true;
                message.Subject = "Your unique QPacity code";

                // Attach the email's header and content
                message = EmailHelpers.AttachEmailHeader(message);
                message = EmailHelpers.AttachIssueEmailContent(message, guest, CodeData);

                // Hook a function upon the email successfully sending
                client.SendCompleted += new SendCompletedEventHandler(ShowIssuingSuccess);

                try
                {
                    // Attempt to send the email
                    client.SendAsync(message, null);

                    // Indicate that the email is sending
                    EmailSendStatus = EmailSendStatus.Sending;
                }
                catch
                {
                    // Indicate that the email failed to send, for whatever reason
                    EmailSendStatus = EmailSendStatus.FaliureGeneric;
                }
            }
        }

        /// <summary>
        /// Show that a QPacity code has been successful issued, when its email has been sent.
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The event arguments</param>
        private void ShowIssuingSuccess(object sender, AsyncCompletedEventArgs e)
        {
            // Indicate that the email has successfully sent
            EmailSendStatus = EmailSendStatus.Success;
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
