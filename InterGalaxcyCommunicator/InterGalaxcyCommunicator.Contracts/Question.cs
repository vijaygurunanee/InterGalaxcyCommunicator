namespace InterGalaxcyCommunicator.Contracts
{
    /// <summary>
    /// The question class repersents the question detials and it's type.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// String lateral will contains origional question.
        /// </summary>
        public string PuzzlePart { get; set; }

        /// <summary>
        /// Represents which type of question is this.
        /// </summary>
        public QuestionType Type { get; set; }
    }
}
