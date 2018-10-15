namespace InterGalaxcyCommunicator.Utility.AnswerGenerator
{
    using InterGalaxcyCommunicator.Contracts;
    using InterGalaxcyCommunicator.Utility.RomanToIntConverter;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Contains business to generate the answers from question
    /// </summary>
    public class AnswerGenerator : IAnswerGenerator
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
        public AnswerGenerator(IRomanToIntConverter romanToIntConverter)
        {
            if (romanToIntConverter == null)
                throw new ArgumentNullException("romanToIntConverter");

            _romanToIntConverter = romanToIntConverter;
        }

        #endregion

        #region IAnswerGenerator implementation

        /// <summary>
        /// Method process the question data to generate answers.
        /// </summary>
        /// <param name="parsedData">Parsed Communication Inputs.</param>
        /// <returns>List of answers.</returns>
        public IList<string> GenerateAnswers(DataContract parsedData)
        {
            if (parsedData == null ||
                parsedData.Questions == null || parsedData.Questions.Count == 0 ||
                parsedData.Questions.All(qstn => qstn == null || string.IsNullOrEmpty(qstn.PuzzlePart)))
                return null;

            var answers = new List<string>();
            var ans = string.Empty;
            
            foreach(var question in parsedData.Questions)
            {
                switch (question.Type)
                {
                    case QuestionType.RomanToCredit:
                        ans = ConvertRomanSequenceToAnswer(question.PuzzlePart, parsedData);
                        break;
                    case QuestionType.RomanMetalToCredit:
                        ans = ConvertRomanMetalSequenceToAnswer(question.PuzzlePart, parsedData);
                        break;
                    default:
                        ans = Constants.NoIdea;
                        break;
                }

                answers.Add(ans);
            }

            return answers;
        }

        #endregion

        #region Private

        /// <summary>
        /// Method to generate answer from symbols of roman.
        /// </summary>
        /// <param name="inputSeq">Sequence of symbols.</param>
        /// <returns>String Answer.</returns>
        private string ConvertRomanSequenceToAnswer(string inputSeq, DataContract parsedData)
        {
            if (parsedData.RomanSymbolsValues == null || parsedData.RomanSymbolsValues.Count == 0)
                return Constants.NoSymbolsForRoman;

            var inputs = inputSeq.Trim().Split(Constants.Space);

            var romanSeq = GenerateRomanSequenceFromSymbols(inputs, parsedData);

            if (!_romanToIntConverter.IsValidRomanString(romanSeq))
                return Constants.NoIdea;

            var ans = _romanToIntConverter.ConvertToInt(romanSeq);

            return string.Format(Constants.RomanSymbolAnswer, inputSeq, ans);
        }

        /// <summary>
        /// Method to generate answer from symbols of roman and metal.
        /// </summary>
        /// <param name="inputSeq">Sequence of symbols and metal.</param>
        /// <returns>String Answer.</returns>
        private string ConvertRomanMetalSequenceToAnswer(string inputSeq, DataContract parsedData)
        {
            if (parsedData.MetalValues == null || parsedData.MetalValues.Count == 0)
                return Constants.NoMetals;

            var inputs = inputSeq.Trim().Split(Constants.Space);

            // Fetch the metal value
            var metalName = inputs.Last().Trim().ToLower();
            if (!parsedData.MetalValues.ContainsKey(metalName))
                return Constants.NoIdea;

            var metalVal = parsedData.MetalValues[metalName];

            // Fetch Roman String
            var romanSeq = GenerateRomanSequenceFromSymbols(inputs.Take(inputs.Length - 1).ToArray(), parsedData);

            if(string.IsNullOrEmpty(romanSeq))
                return string.Format(Constants.MetalSymbolAnswer, inputSeq, metalVal);

            // Fetch value of roman sequence
            if (!_romanToIntConverter.IsValidRomanString(romanSeq))
                return Constants.NoIdea;

            var romanVal = _romanToIntConverter.ConvertToInt(romanSeq);

            // Multiply and answer
            return string.Format(Constants.MetalSymbolAnswer, inputSeq, metalVal * romanVal);
        }

        /// <summary>
        /// Generates the roman sequence from symbols.
        /// </summary>
        /// <param name="inputs">Sequence of Symbols.</param>
        /// <returns>Roman Sequence String.</returns>
        private string GenerateRomanSequenceFromSymbols(string[] inputs, DataContract parsedData)
        {
            var romanSeq = string.Empty;

            foreach(var input in inputs)
            {
                if(!parsedData.RomanSymbolsValues.ContainsKey(input))
                {
                    romanSeq = Constants.NoIdea;
                    break;
                }

                romanSeq = string.Concat(romanSeq, parsedData.RomanSymbolsValues[input]);
            }

            return romanSeq;
        }

        #endregion
    }
}
