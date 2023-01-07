using System;
using System.Threading;
using System.Threading.Tasks;

namespace QPacity
{
    /// <summary>
    /// Commonly used logic to keep asynchonous time in the app.
    /// </summary>
    public class TimerHelpers
    {
        /// <summary>
        /// Runs a action every time a passed interval occurs.
        /// </summary>
        /// <param name="action">The action to run</param>
        /// <param name="interval">The interval between actions</param>
        /// <param name="token">The cancellation tokens</param>
        /// <returns>The completed task</returns>
        public async static Task RunPeriodically(Action action, TimeSpan interval, CancellationToken token)
        {
            // While the app is still running...
            while (true)
            {
                // Await the specified time
                await Task.Delay(interval, token);

                // Run the specified action
                action();
            }
        }
    }
}
