namespace InterGalaxcyCommunicator.Utility.RomanToIntConverter
{
    /// <summary>
    /// Interface to convert roman strings to decimal
    /// </summary>
    public interface IRomanToIntConverter
    {

        /// <summary>
        /// Validate the given input string.
        /// </summary>
        /// <param name="input">Input String.</param>
        /// <returns>true if input is valid Roman string.</returns>
        bool IsValidRomanString(string input);

        /// <summary>
        /// Convert the given Roman input into equal integer value.
        /// </summary>
        /// <param name="input">Roman Input.</param>
        /// <returns>Int.</returns>
        int ConvertToInt(string input);
    }
}
