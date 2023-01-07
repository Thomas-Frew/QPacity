/// <summary>
/// A custom variable type for the possible statuses during the QPacity code issuing process.
/// </summary>
public enum EmailSendStatus
{
    /// <summary>
    /// When an code is not being issued.
    /// </summary>
    Idle = 0,

    /// <summary>
    /// When a code is sending.
    /// </summary>
    Sending = 1,

    /// <summary>
    /// When a code has successfully sent.
    /// </summary>
    Success = 2,

    /// <summary>
    /// When a code has failed to send (generic).
    /// </summary>
    FaliureGeneric = 3,

    /// <summary>
    /// When a code has failed to send because the phone number is not valid.
    /// </summary>
    FaliurePhoneNumber = 4,

    /// <summary>
    /// When a code has failed to send because the email is not valid.
    /// </summary>
    FaliureEmail = 5
}