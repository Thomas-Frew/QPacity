using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QPacity
{
    /// <summary>
    /// A set of useful functions to help with loading information from a database.
    /// </summary>
    public class DatabaseHelpers
    {
        /// <summary>
        /// Loads the South Australia's database of COVID restriction levels from local storage.
        /// </summary>
        /// <returns>The course database.</returns>
        public static List<List<string>> LoadRestrictionsDatabase()
        {
            // Load the database information from RestrictionsDatabase.csv as a list of lists of strings, by splitting each line by commas
            List<List<string>> database = File.ReadAllLines("RestrictionsDatabase.csv").Select(line => line.Split(',').ToList()).ToList();

            // Filter and return the only the restriction levels which have 9 properties (the correct amount)
            return database.Where(c => c.Count == 9).ToList();
        }

        /// <summary>
        /// Find the restriction data assocated with the current COVID restriction level.
        /// </summary>
        /// <param name="level">The current COVID restriction level</param>
        /// <returns>The restriction level's data</returns>
        public static List<string> FindRestrictionData(RestrictionLevel level)
        {
            // Load the resitrctions database
            List<List<string>> restrictionsDatabase = LoadRestrictionsDatabase();

            // Return the data for the selected COVID restriction level
            return restrictionsDatabase.Where(data => (RestrictionLevel)Convert.ToInt16(data[(int)RProp.Level]) == level).ToList()[0];
        }

        /// <summary>
        /// Returns the density of people that can attend the user's event, based on the current COVID restriction level and activity type
        /// </summary>
        /// <param name="level">The current COVID restriction level.</param>
        /// <param name="seatingType">The activity's seating plan.</param>
        /// <returns>The density of people who can attend this event.</returns>
        public static double FindDensity(RestrictionLevel level, SeatingPlan seatingType)
        {
            // Find the restriction data from the current COVID restriction level
            List<string> restrictionData = FindRestrictionData(level);

            // Return the density of this restriction level based on the seating type.
            switch (seatingType)
            {
                // If people are to be seated only...
                case SeatingPlan.Seated:
                    return Convert.ToDouble(restrictionData[(int)RProp.SeatedDensity]);

                // If people can be seated or standing...
                case SeatingPlan.Standing:
                    return Convert.ToDouble(restrictionData[(int)RProp.SeatedDensity]);

                // If people are playing indoor sports, or if some kind of error occurs where we must assume the worst...
                case SeatingPlan.Sports:
                default:
                    return Convert.ToDouble(restrictionData[(int)RProp.SportsDensity]);
            }
        }

        /// <summary>
        /// Find the current event's capacity cap based on additional COVID restrictions.
        /// </summary>
        /// <param name="level">The current COVID restriction level.</param>
        /// <param name="seatingType">The activity's seating plan.</param>
        /// <returns>The true capacity for this event.</returns>
        public static double FindCapacityCap(RestrictionLevel level, EventType eventType)
        {
            // Find the restriction data from the current COVID restriction level
            List<string> restrictionData = FindRestrictionData(level);

            // Set the default maximum number of people to one million.
            double capacityCap = 1000000;

            // Adjust this capacity cap based on the event type.
            switch (eventType)
            {
                // If the event is at home, set the capacity cap to the at-home limit.
                case EventType.At_Home:
                    capacityCap = Convert.ToDouble(restrictionData[(int)RProp.AtHomeLimit]);
                    break;

                // If the event is private, set the capacity cap to the private limit.
                case EventType.Private:
                    capacityCap = Convert.ToDouble(restrictionData[(int)RProp.PrivateLimit]);
                    break;
            }

            // Return the capacity cap
            return capacityCap;
        }

    }
}
