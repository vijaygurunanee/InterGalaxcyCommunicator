namespace InterGalaxcyCommunicator.Contracts
{
    /// <summary>
    /// Class with list of all constants.
    /// </summary>
    public class Constants
    {
        public const string WelComeMessage = "Hi!! Wel-come to InterGallactic Communicator...";

        public const string RequestFilePath = "You haven't started program execution with proper input File Path...\nPlease Enter Full Path Below...";

        public const string TextFileExtention = ".txt";

        public const string EmptyFilePath = "File Path is empty.";

        public const string NotTextFile = "Not a text file. File name doesn't end with .txt";

        public const string FileNotFound = "File Not Found.";

        public const string EmptyFile = "File is Empty.";

        public const string InvalidFilePath = "File Path is not proper. Program found below mentioned issue.\n{0}";

        public const string ArgumentNullException = "Null Argument : {0}";

        public const string ImproperFile = "Proper File is not provided... Good Bye...";

        public const string RomanValidation = @"^[M]{0,3}([C]{1}[M]{1}){0,1}[D]{0,3}([C]{1}[D]{1}){0,1}[C]{0,3}([X]{1}[C]{1}){0,1}[L]{0,3}([X]{1}[L]{1}){0,1}[X]{0,3}([I]{1}[X]{1}){0,1}[V]{0,3}([I]{1}[V]{1}){0,1}[I]{0,3}$";

        public const string InvalidRomanSequenceMessge = "Roman Sequence in not according to defined rules...";

        public const string EmptyInputLines = "Either File is empty or error in reading file...";

        public const string CollectedFileContentMessage = "------- Collected File Content --------";

        public const char Space = ' ';

        public const string Is = "is";

        public const string QuestionMark = "?";

        public const string Credits = "Credits";

        public const string GeneratedAnswers = "------- Answers --------";

        public const string NoIdea = "I have no idea what you are talking about";

        public const string RomanSymbolAnswer = "{0} is {1}";

        public const string MetalSymbolAnswer = "{0} is {1} Credits";

        public const string FloatFormatter = "0.0";

        public const string Error = "Something went wrong... \nError Message: {0}";

        public const string NoQuestions = "Input contains no questions...";

        public const string NoMetals = "Input contains no metals...";

        public const string NoSymbolsForRoman = "Input contains no symbols for Roman...";

        public const string InvalidArgument = "Invalid Argument : {0}";
    }
}
