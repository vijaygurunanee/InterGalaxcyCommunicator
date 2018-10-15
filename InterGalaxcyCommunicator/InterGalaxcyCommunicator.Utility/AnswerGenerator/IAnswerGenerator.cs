namespace InterGalaxcyCommunicator.Utility.AnswerGenerator
{
    using InterGalaxcyCommunicator.Contracts;
    using System.Collections.Generic;

    /// <summary>
    /// Contains business to generate the answers from question
    /// </summary>
    public interface IAnswerGenerator
    {
        /// <summary>
        /// Method process the question data to generate answers.
        /// </summary>
        /// <param name="parsedData">Parsed Communication Inputs.</param>
        /// <returns>List of answers.</returns>
        IList<string> GenerateAnswers(DataContract parsedData);
    }
}
