using QPacity.Properties;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Media;

namespace QPacity
{
    /// <summary>
    /// A set of useful functions to help with sending emails.
    /// </summary>
    public class EmailHelpers
    {
        /// <summary>
        /// Creates an approved SmtpClient for sending emails.
        /// </summary>
        /// <returns>The fully completed SMTP client</returns>
        public static SmtpClient CompileSmtpClient()
        {
            // Creates an SMTP client which connects to outlook by default.
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            // Ensures the email is sent over a network with a minute-long timeout direction
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 600000;

            // Create credentials for QPacity's mail server
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("qpacityserver@gmail.com", "UltimateEmailServer");

            // Setup the client for QPacity's mail server
            client.Credentials = credentials;

            // Enables encrypton for the email client
            client.EnableSsl = true;

            // Return the setup, SMTP client
            return client;
        }

        /// <summary>
        /// Attaches the header to an email.
        /// </summary>
        /// <param name="message">The unheadered email</param>
        /// <returns>The email, with the appropariate header attached</returns>
        public static MailMessage AttachEmailHeader(MailMessage message)
        {
            message.Body += "<body style=\"font-family:Courier New;\">";
            return message;
        }

        /// <summary>
        /// Attaches the standard FAQ content to a QPacity code Issuing email.
        /// </summary>
        /// <param name="message">The email, without its content</param>
        /// <returns>The email with its appropraite content</returns>
        public static MailMessage AttachIssueEmailContent(MailMessage message, Guest guest, Bitmap imageData)
        {

            // Determine the QPacity code's unique file directory
            string directory = "C:/QPacity/QPacityCode" + Settings.Default.CodeID.ToString() + ".jpg";

            // Archive the passed QR code to files and pull it out to send through an email
            imageData.Save(directory);
            message.Attachments.Add(new Attachment(directory));

            // Increment the QPacity code's ID to prevent overlap
            Settings.Default.CodeID++;
            Settings.Default.Save();

            // Opening regards
            message.Body += "<h1>Welcome to QPacity, " + guest.Name + "!</h1>";
            message.Body += "<p>Getting started with QPacity is simple. Just present your QPacity code (attached to this email) to the scanner at any QPacity-protected room to sign in and out.</p>";

            // Download Reccomendations
            message.Body += "<h3>Should I download my QPacity code?</h3>";
            message.Body += "<p>If you cannot conveniently access your emails on your phone, it is a good idea to save a downloaded copy there. Printed QPacity codes should also validate, although you may experience some troubles if the paper is contorted.</p>";

            // Scanning Tricks
            message.Body += "<h3>How long does it take to scan my QPacity code?</h3>";
            message.Body += "<p>To ensure your QPacity code successfully scans, please present it to a scanner for at least one second. If you experience further difficulties, try removing obstructions from your code or holding it close to the scanner.</p>";

            // Security Concerns
            message.Body += "<h3>What does my QPacity code contain?</h3>";
            message.Body += "<p>Your QPacity code contains your name and phone number. These give you a unique identifier to enter protected rooms and help staff protect their guests.</p>";

            // Concluding regards
            message.Body += "<h3>Have any other questions?</h3>";   
            message.Body += "<p>Please contact room managers for QPacity assistance when attending events. For other enquiries, feel free to contact the QPacity Developers <a href=mailto:qpacityserver@gmail.com>here</a>.</p>";
            message.Body += "</body>";

            return message;
        }

        /// <summary>
        /// Attaches the an email's content.
        /// </summary>
        /// <param name="message">The email, withou its content</param>
        /// <returns>The email with its appropraite content</returns>
        public static MailMessage AttachBreachEmailContent(MailMessage message, Guest guest)
        {
            // Opening regards
            message.Body += "<h1>COVID Breach on " + DateTime.Now.ToString("dd/MM") + ", " + DateTime.Now.ToString("hh:mm") + ".</h1>";
            message.Body += "<p>There has been a reported breach of COVID restrictions by " + guest.Name + " (" + guest.PhoneNumber + ").</p>";
            message.Body += "<p>Please forward this email to SA Health's <a href=mailto:health.COVIDCompliance@sa.gov.au>COVID compliance department</a>.</p>";
            message.Body += "</body>";

            return message;
        }
    }
}
