namespace InterGalaxcyCommunicator.Contracts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the complete current set-up.
    /// It will hold the metals, symbols and questions.
    /// </summary>
    public class DataContract
    {

        /// <summary>
        /// The value of symbols in Roman Sequence.
        /// </summary>
        public IDictionary<string, string> RomanSymbolsValues { get; private set; }

        /// <summary>
        /// Metal values in Integer.
        /// </summary>
        public IDictionary<string, float> MetalValues { get; private set; }

        /// <summary>
        /// List of questions.
        /// </summary>
        public IList<Question> Questions { get; private set; }

        #region Add methods

        /// <summary>
        /// Method to add Roman Symbols.
        /// </summary>
        /// <param name="key">Key = Symbol.</param>
        /// <param name="value">Value = Roman Value.</param>
        public void AddRomanSymbolsValues(string key, string value)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            if (RomanSymbolsValues == null)
                RomanSymbolsValues = new Dictionary<string, string>();

            RomanSymbolsValues.Add(key, value);
        }

        /// <summary>
        /// Method to add Metal Values.
        /// </summary>
        /// <param name="key">Key = Metal.</param>
        /// <param name="value">Value.</param>
        public void AddMetalValues(string key, float value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (MetalValues == null)
                MetalValues = new Dictionary<string, float>();

            MetalValues.Add(key.Trim().ToLower(), value);
        }

        /// <summary>
        /// Method to add questions.
        /// </summary>
        /// <param name="question">Question.</param>
        public void AddQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException("question");

            if (Questions == null)
                Questions = new List<Question>();

            Questions.Add(question);
        }

        #endregion
    }
}