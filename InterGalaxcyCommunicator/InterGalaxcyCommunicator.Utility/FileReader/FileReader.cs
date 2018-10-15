namespace InterGalaxcyCommunicator.Utility.FileReader
{
    using InterGalaxcyCommunicator.Contracts;
    using InterGalaxcyCommunicator.Utility.CustomExceptions;
    using System.IO;

    /// <summary>
    /// File related operations will be performed in this class.
    /// </summary>
    public class FileReader : IFileReader
    {
        #region Private Variables

        /// <summary>
        /// Contains File Path.
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// File Reader to read file one by one line.
        /// </summary>
        private StreamReader _fileReader;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with filePath as input.
        /// </summary>
        /// <param name="filePath">Input File Path.</param>
        public FileReader(string filePath)
        {
            ValidateFilePath(filePath);
            _filePath = filePath;
        }

        #endregion

        #region IFileReader Method

        /// <summary>
        /// Used to close the file which is open in ReadLine method.
        /// </summary>
        public void CloseFile()
        {
            if (_fileReader != null)
                _fileReader.Close();
        }

        /// <summary>
        /// Used to fetch all the contents of a File at once.
        /// </summary>
        /// <returns>string of File content.</returns>
        public string ReadAllContent()
        {
            return File.ReadAllText(_filePath);
        }

        /// <summary>
        /// Used to fetch all the lines of a File at once.
        /// </summary>
        /// <returns>string array of all lines of file.</returns>
        public string[] ReadAllLines()
        {
            return File.ReadAllLines(_filePath);
        }

        /// <summary>
        /// Used to read one line at a time. Reads only the part.
        /// </summary>
        /// <returns>One Line.</returns>
        public string ReadLine()
        {
            if (_fileReader == null)
                _fileReader = new StreamReader(_filePath);

            return _fileReader.ReadLine();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Methods to validate file Path.
        /// </summary>
        /// <param name="filePath">File Path.</param>
        private void ValidateFilePath(string filePath)
        {
            var errorMessage = string.Empty;

            if (string.IsNullOrEmpty(filePath))
                errorMessage = Constants.EmptyFilePath;
            else if (!filePath.EndsWith(Constants.TextFileExtention))
                errorMessage = Constants.NotTextFile;
            else if (!File.Exists(filePath))
                errorMessage = Constants.FileNotFound;
            else if (new FileInfo(filePath).Length == 0)
                errorMessage = Constants.EmptyFile;

            if (!string.IsNullOrEmpty(errorMessage))
                throw new InvalidFileNameInputException(errorMessage);
        }

        #endregion
    }
}
