namespace InterGalaxcyCommunicator
{
    using Contracts;
    using System;
    using Utility.AnswerGenerator;
    using Utility.CommunicationProcessor;
    using Utility.CustomExceptions;
    using Utility.FileReader;
    using Utility.InputParser;
    using Utility.RomanToIntConverter;

    /// <summary>
    /// Execution Beginning Point.
    /// </summary>
    public class Program
    {
        #region private static variables

        /// <summary>
        /// File Reader;
        /// </summary>
        private static IFileReader _fileReader;

        /// <summary>
        /// Roman To Integer Converter.
        /// </summary>
        private static IRomanToIntConverter _romanToIntConverter;

        /// <summary>
        /// Input Parser.
        /// </summary>
        private static IInputParser _inputParser;

        /// <summary>
        /// Answer Generator.
        /// </summary>
        private static IAnswerGenerator _answerGenerator;

        /// <summary>
        /// Communication Processor.
        /// </summary>
        private static ICommunicationProcessor _communicationProcessor;

        #endregion

        #region Main Method

        /// <summary>
        /// Program Starting Point.
        /// </summary>
        /// <param name="args">System starting Inputs.</param>
        public static void Main(string[] args)
        {
            var filePath = string.Empty;

            Console.WriteLine(Constants.WelComeMessage);

            if (args.Length == 0)
            {
                Console.WriteLine(Constants.RequestFilePath);
                filePath = Console.ReadLine();
            }
            else
                filePath = args[0];

            try
            {
                GeneratePreRecs(filePath);

                var answers = _communicationProcessor.ProcessInputs();

                foreach(var answer in answers)
                {
                    Console.WriteLine(answer);
                }
            }
            catch (InvalidFileNameInputException ex)
            {
                Console.WriteLine(Constants.InvalidFilePath, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(Constants.ArgumentNullException, ex.ParamName);
            }
            catch (EmptyInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Error, ex.Message);
            }

            Console.ReadLine();

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to initialize the private variables.
        /// </summary>
        /// <param name="filePath">Source File Path.</param>
        private static void GeneratePreRecs(string filePath)
        {
            _fileReader = new FileReader(filePath);
            _romanToIntConverter = new RomanToIntConverter();
            _inputParser = new InputParser(_romanToIntConverter);
            _answerGenerator = new AnswerGenerator(_romanToIntConverter);
            _communicationProcessor = new CommunicationProcessor(_fileReader, _inputParser, _answerGenerator);
        }

        #endregion
    }
}
