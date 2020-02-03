using System.Text;

namespace ConsoleApp2
{
    /// <summary>
    /// Class created for displaying messages regarding progress
    /// </summary>
    class StatusChecker
    {
        /// <summary>
        /// Check whether palindrome was already found
        /// </summary>
        public bool WasPrimeNumberFound { get; set; }

        /// <summary>
        /// Contains message displaying to user regarding computing progress
        /// </summary>
        public StringBuilder StatusMessage { get; set; }
    }
}
