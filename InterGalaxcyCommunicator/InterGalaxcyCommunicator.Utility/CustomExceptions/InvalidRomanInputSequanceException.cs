namespace InterGalaxcyCommunicator.Utility.CustomExceptions
{
    using System;

    /// <summary>
    /// Custom exception class for invalid file path
    /// </summary>
    public class InvalidRomanInputSequanceException : Exception
    {
        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public InvalidRomanInputSequanceException()
        { }

        /// <summary>
        /// Constructor with message to base class.
        /// </summary>
        /// <param name="message">Exception Messge.</param>
        public InvalidRomanInputSequanceException(string message) : base(message)
        { }

        /// <summary>
        /// Constructor with message to base class.
        /// </summary>
        /// <param name="message">Exception Messge.</param>
        /// <param name="innerException">Inner Exception.</param>
        public InvalidRomanInputSequanceException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
