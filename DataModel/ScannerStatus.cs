/// <summary>
/// A custom variable type for the application scanner's various statuses.
/// </summary>
public enum ScannerStatus
{
    /// <summary>
    /// Active QPacity code scanning (blue)
    /// </summary>
    Scanning = 0,

    /// <summary>
    /// User has been signed In (green)
    /// </summary>
    SignIn = 1,

    /// <summary>
    /// User has been signed Out (red)
    /// </summary>
    SignOut = 2,

    /// <summary>
    /// Closed QPacity code scanning, when capacity is reached (black)
    /// </summary>
    DisabledCapacity = 3,

    /// <summary>
    /// Closed QPacity code scanning, disabled manually (black)
    /// </summary>
    DisabledManual = 4
}