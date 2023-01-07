/// <summary>
/// A custom variable type to organsie the properties of a restriction level.
/// </summary>
public enum RProp
{
    /// <summary>
    /// The restriction level.
    /// </summary>
    Level = 0,

    /// <summary>
    /// The maximum density of seated people within this restriction level.
    /// </summary>
    SeatedDensity = 1,

    /// <summary>
    /// The maximum density of standing people within this restriction level.
    /// </summary>
    StandingDensity = 2,

    /// <summary>
    /// The maximum density of people playing sports within this restriction level.
    /// </summary>
    SportsDensity = 3,

    /// <summary>
    /// The limit of people in at-home gatherings within this restriction level.
    /// </summary>
    AtHomeLimit = 4,

    /// <summary>
    /// The limit of people in private gatherings within this restriction level.
    /// </summary>
    PrivateLimit = 5,

    /// <summary>
    /// If restriction level can have COVID Management Plan events.
    /// </summary>
    HasCMPEvents = 6,

    /// <summary>
    /// The threshold of people at which a COVID Management Plan is required.
    /// </summary>
    CMPThreshold = 7,

    /// <summary>
    /// If this restriction level requires people to wear masks.
    /// </summary>
    RequiresMasks = 8
}