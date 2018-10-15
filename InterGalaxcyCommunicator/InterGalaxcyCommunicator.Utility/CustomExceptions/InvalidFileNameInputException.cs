namespace InterGalaxcyCommunicator.Utility.CustomExceptions
{
    using System;

    /// <summary>
    /// Custom exception class for invalid file path
    /// </summary>
    public class InvalidFileNameInputException : Exception
    {
        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public InvalidFileNameInputException()
        { }

        /// <summary>
        /// Constructor with message to base class.
        /// </summary>
        /// <param name="message">Exception Messge.</param>
        public InvalidFileNameInputException(string message) : base(message)
        { }

        /// <summary>
        /// Constructor with message to base class.
        /// </summary>
        /// <param name="message">Exception Messge.</param>
        /// <param name="innerException">Inner Exception.</param>
        public InvalidFileNameInputException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
