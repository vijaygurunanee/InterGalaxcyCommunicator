namespace InterGalaxcyCommunicator.Utility.InputParser
{
    using InterGalaxcyCommunicator.Contracts;
    using InterGalaxcyCommunicator.Utility.CustomExceptions;
    using InterGalaxcyCommunicator.Utility.RomanToIntConverter;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class to parse the inputs and generate the required meta-data for question processing.
    /// </summary>
    public class InputParser : IInputParser
    {
        #region Private Readonly Variables

        /// <summary>
        /// Roman to Int Converter.
        /// </summary>
        private readonly IRomanToIntConverter _romanToIntConverter;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize private readonly properties.
        /// </summary>
        /// <param name="romanToIntConverter">Roman to Int Conversion.</param>
        public InputParser(IRomanToIntConverter romanToIntConverter)
        {
            if (romanToIntConverter == null)
                throw new ArgumentNullException("romanToIntConverter");

            _romanToIntConverter = romanToIntConverter;
        }

        #endregion

        #region IInputParser implementation

        /// <summary>
        /// This method will parse the inputs and validates it.
        /// </summary>
        /// <param name="inputs">Inputs from Caller.</param>
        /// <returns>DataContract.</returns>
        public DataContract ParseInputs(string[] inputs)
        {
            if (inputs == null || inputs.Length == 0 || inputs.All(input => string.IsNullOrEmpty(input)))
                throw new EmptyInputException(Constants.EmptyInputLines);

            var parsedData = new DataContract();

            foreach(var input in inputs.Where(i => !string.IsNullOrEmpty(i)))
            {
                // First of all separate the line with space
                var words = input.Split(Constants.Space);

                // if there are 3 words, then it is Symbol for Roman chars.
                // Just store it in romanSymbolic
                if (words.Length == 3)
                {
                    var symbolForRoman = FetchSymbolForRoman(words);

                    if (symbolForRoman != null)
                        parsedData.AddRomanSymbolsValues(symbolForRoman.Symbol, symbolForRoman.RomanSequence);

                    continue;
                }
                
                // if sentence ends with question mark then it is a question.
                // Just add it to question list.
                if (input.EndsWith(Constants.QuestionMark, StringComparison.InvariantCultureIgnoreCase))
                {
                    var question = FetchQuestion(input);

                    if (question != null)
                        parsedData.AddQuestion(question);

                    continue;
                }

                // if sentence ends with "Credits" then it presents the value of metal.
                // Just add it to metal values.
                if (input.EndsWith(Constants.Credits, StringComparison.InvariantCultureIgnoreCase))
                {
                    Metal metal = FetchMetal(input, parsedData.RomanSymbolsValues);

                    if (metal != null)
                        parsedData.AddMetalValues(metal.MetalName, metal.Value);
                }
            }

            return parsedData;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// A method to fetch the roman for symbol.
        /// </summary>
        /// <param name="inputWords">Inputs.</param>
        /// <returns>Symbol For Roman.</returns>
        private SymbolForRoman FetchSymbolForRoman(string[] inputWords)
        {
            //Middle word should be is only.
            if (inputWords.Any(value => string.IsNullOrEmpty(value)) ||
                inputWords[1].Trim().ToLower() != Constants.Is)
                return null;

            var symbol = inputWords[0].Trim();
            var romanSq = inputWords[2].Trim().ToUpper();

            //Check if value is valid roman char or not.
            if (!_romanToIntConverter.IsValidRomanString(romanSq))
                return null;

            return new SymbolForRoman
            {
                Symbol = symbol,
                RomanSequence = romanSq
            };
        }

        /// <summary>
        /// A method to fetch question.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>Question.</returns>
        private Question FetchQuestion(string input)
        {
            // Remove question mark from end and add it to questions list.
            input = input.Remove(input.Length - 1);

            var question = new Question();

            // Split question with Is
            var splittedQuestion = input.Split(new string[] { Constants.Is }, 2, StringSplitOptions.RemoveEmptyEntries);
            if(splittedQuestion.Length != 2)
            {
                question.PuzzlePart = input;
                question.Type = QuestionType.Invalid;
                return question;
            }

            // Actual Question Part
            question.PuzzlePart = splittedQuestion[1];

            // Now type can be defined using first part
            if (splittedQuestion[0].Contains(Constants.Credits))
                question.Type = QuestionType.RomanMetalToCredit;
            else
                question.Type = QuestionType.RomanToCredit;

            return question;
        }

        /// <summary>
        /// A method to fetch metal value.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>Metal Object.</returns>
        private Metal FetchMetal(string input, IDictionary<string, string> romanSymbolsValues)
        {
            string[] inputWords = input.Split(Constants.Space).ToArray();

            //input word list should contain Is word.
            if (inputWords.Any(value => string.IsNullOrEmpty(value)) ||
                !inputWords.Contains(Constants.Is))
                return null;

            // Remove credits from last.
            inputWords = inputWords.Take(inputWords.Length - 1).ToArray();

            float metalVal = 0;
            if (!float.TryParse(inputWords[inputWords.Length - 1], out metalVal))
                return null;

            var romanSequenceBeforeMetal = string.Empty;
            var metalName = string.Empty;

            // Extract Name of metal and Roman sequance before name of metal
            foreach (var word in inputWords)
            {
                if (word == Constants.Is)
                    break;

                if (romanSymbolsValues.ContainsKey(word))
                {
                    romanSequenceBeforeMetal = string.Concat(romanSequenceBeforeMetal, romanSymbolsValues[word]);
                    continue;
                }
                else
                    metalName = string.Concat(metalName, Constants.Space, word);
            }

            // If Roman Sequence is found then divide metal value with divisor
            if (!string.IsNullOrEmpty(romanSequenceBeforeMetal))
            {
                if (!_romanToIntConverter.IsValidRomanString(romanSequenceBeforeMetal))
                    return null;

                float divisor = _romanToIntConverter.ConvertToInt(romanSequenceBeforeMetal);

                metalVal = metalVal / divisor;
            }

            return new Metal { MetalName = metalName, Value = metalVal };
        }

        #endregion
    }
}
