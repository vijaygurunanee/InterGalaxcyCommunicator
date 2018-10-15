namespace InterGalaxcyCommunicator.Utility.InputParser
{
    using InterGalaxcyCommunicator.Contracts;

    /// <summary>
    /// Interface to parse the inputs.
    /// </summary>
    public interface IInputParser
    {
        /// <summary>
        /// This method will parse the inputs and validates it.
        /// </summary>
        /// <param name="inputs">Inputs from Caller.</param>
        /// <returns>DataContract.</returns>
        DataContract ParseInputs(string[] inputs);
    }
}
