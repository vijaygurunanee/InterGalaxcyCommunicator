namespace InterGalaxcyCommunicator.Utility.CommunicationProcessor
{
    using System.Collections.Generic;

    /// <summary>
    /// Central Business class which contains all flows to solve the puzzle.
    /// </summary>
    public interface ICommunicationProcessor
    {
        /// <summary>
        /// Method to process the file input.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>Answers if possible.</returns>
        IList<string> ProcessInputs();
    }
}
