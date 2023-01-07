using QPacity.Properties;
using System;

namespace QPacity
{
    /// <summary>
    /// A series of commonly used logic in value converters.
    /// </summary>
    public class ConverterHelpers
    {
        /// <summary>
        /// Converts the window's height to the circular progress bar's height.
        /// </summary>
        /// <param name="windowHeight">The widnow's height</param>
        /// <returns>The circular progress bar's height</returns>
        public static double WindowHeightToProgressBarHeight(double windowHeight)
        {
            return Math.Max((windowHeight / 2) - 180, 0);
        }
    }
}
