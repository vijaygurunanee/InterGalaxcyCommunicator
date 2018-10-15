namespace InterGalaxcyCommunicator.Utility.FileReader
{
    /// <summary>
    /// Lists all file related processing methods.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Used to fetch all the contents of a File at once.
        /// </summary>
        /// <returns>string of File content.</returns>
        string ReadAllContent();

        /// <summary>
        /// Used to fetch all the lines of a File at once.
        /// </summary>
        /// <returns>string array of all lines of file.</returns>
        string[] ReadAllLines();

        /// <summary>
        /// Used to read one line at a time. Reads only the part.
        /// </summary>
        /// <returns>One Line.</returns>
        string ReadLine();

        /// <summary>
        /// Used to close the file which is open in ReadLine method.
        /// </summary>
        void CloseFile();
    }
}
