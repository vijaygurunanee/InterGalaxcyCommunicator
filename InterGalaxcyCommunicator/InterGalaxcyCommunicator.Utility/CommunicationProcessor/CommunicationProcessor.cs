namespace InterGalaxcyCommunicator.Utility.CommunicationProcessor
{
    using InterGalaxcyCommunicator.Utility.AnswerGenerator;
    using InterGalaxcyCommunicator.Utility.FileReader;
    using InterGalaxcyCommunicator.Utility.InputParser;
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Central Business class which contains all flows to solve the puzzle.
    /// </summary>
    public class CommunicationProcessor : ICommunicationProcessor
    {
        #region Private Read Only Variables

        /// <summary>
        /// File Reader.
        /// </summary>
        private readonly IFileReader _fileReader;

        /// <summary>
        /// Input Parser.
        /// </summary>
        private readonly IInputParser _inputParser;

        /// <summary>
        /// Answer Generator.
        /// </summary>
        private readonly IAnswerGenerator _answerGenerator;

        #endregion

        #region Contructor

        /// <summary>
        /// Constructor to initialize the private variables.
        /// </summary>
        /// <param name="fileReader">File Reader.</param>
        /// <param name="inputParser">Input Parser.</param>
        /// <param name="answerGenerator">Answer Generator.</param>
        public CommunicationProcessor(IFileReader fileReader, IInputParser inputParser, IAnswerGenerator answerGenerator)
        {
            if (fileReader == null)
                throw new ArgumentNullException("fileReader");

            if (inputParser == null)
                throw new ArgumentNullException("inputParser");

            if (answerGenerator == null)
                throw new ArgumentNullException("answerGenerator");

            _fileReader = fileReader;
            _inputParser = inputParser;
            _answerGenerator = answerGenerator;
        }

        #endregion

        #region ICommunicationProcessor implementation

        /// <summary>
        /// Method to process the file input.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>Answers if possible.</returns>
        public IList<string> ProcessInputs()
        {
            var inputs = _fileReader.ReadAllLines();

            var parsedData = _inputParser.ParseInputs(inputs);

            var answers = _answerGenerator.GenerateAnswers(parsedData);

            return answers;
        }

        #endregion
    }
}
