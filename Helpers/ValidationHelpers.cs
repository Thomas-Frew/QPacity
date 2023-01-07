using QPacity.Properties;
using System;

namespace QPacity
{
    /// <summary>
    /// A series of commonly used logic in value converters.
    /// </summary>
    public class ValidationHelpers
    {
        /// <summary>
        /// Converts an input into a valid, positive double.
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The input string's double value, as a string </returns>
        public static string ValidatePositiveDouble(string input)
        {
            // Initialise a new double variable to store the resultant value.
            double value = new double();

            // If the input is a valid double...
            if (double.TryParse(input, out value))
            {
                if (input.Substring(input.Length - 1) == ".")
                {
                    // Clamp its value to positive double, with a decimal point and reutrn it.
                    return Math.Max(value, 0).ToString() + ".";
                }
                else
                {
                    // Clamp its value to positive double and reutrn it.
                    return Math.Max(value, 0).ToString();
                }

            }
            else
            {
                // Return an empty string, by default
                return "0";
            }
        }
    }
}
