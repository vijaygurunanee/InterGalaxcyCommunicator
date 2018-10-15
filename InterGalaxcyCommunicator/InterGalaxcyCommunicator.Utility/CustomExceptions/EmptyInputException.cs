namespace InterGalaxcyCommunicator.Utility.CustomExceptions
{
    using System;

    /// <summary>
    /// Custom exception class for Empty inputs.
    /// </summary>
    public class EmptyInputException : Exception
    {
        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public EmptyInputException()
        { }

        /// <summary>
        /// Constructor with message to base class.
        /// </summary>
        /// <param name="message">Exception Messge.</param>
        public EmptyInputException(string message)
            : base(message)
        { }

        /// <summary>
        /// Constructor with message to base class.
        /// </summary>
        /// <param name="message">Exception Messge.</param>
        /// <param name="innerException">Inner Exception.</param>
        public EmptyInputException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
