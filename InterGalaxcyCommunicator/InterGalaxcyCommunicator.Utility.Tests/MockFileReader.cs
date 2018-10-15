namespace InterGalaxcyCommunicator.Utility.Tests
{
    using InterGalaxcyCommunicator.Utility.FileReader;

    /// <summary>
    /// Mock file reader class is designed to pass the mock data for test cases.
    /// </summary>
    public class MockFileReader : IFileReader
    {
        #region Interface implementation

        public string ReadAllContent()
        {
            throw new System.NotImplementedException();
        }

        public string[] ReadAllLines()
        {
            return new string[] {
                "itt is I",
                "vtt is V",
                "xtt is X",
                "ltt is L",
                "ctt is C",
                "dtt is D",
                "mtt is M",
                "xtt Metal is 100 Credits",
                "mtt itt TestMetal is 10010 Credits",
                "AnotherTestMetal is 20 Credits",
                "how much is mtt mtt mtt ctt mtt dtt dtt dtt ctt dtt ctt ctt ctt xtt ctt ltt ltt ltt xtt ltt xtt xtt xtt itt xtt vtt vtt vtt itt vtt itt itt itt ?",
                "how many Credits is xtt ltt vtt itt Metal ?",
                "how many Credits is ctt ctt ctt xtt ctt xtt ltt TestMetal ?",
                "how many Credits is AnotherTestMetal ?"
            };
        }

        public string ReadLine()
        {
            throw new System.NotImplementedException();
        }

        public void CloseFile()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
