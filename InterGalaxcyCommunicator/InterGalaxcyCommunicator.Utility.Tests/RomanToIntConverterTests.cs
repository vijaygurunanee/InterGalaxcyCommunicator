namespace InterGalaxcyCommunicator.Utility.Tests
{
    using InterGalaxcyCommunicator.Utility.CustomExceptions;
    using InterGalaxcyCommunicator.Utility.RomanToIntConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class RomanToIntConverterTests
    {
        #region Private Variables

        /// <summary>
        /// To invoke target methods
        /// </summary>
        private readonly IRomanToIntConverter _romanToIntConverter;

        /// <summary>
        /// Few Valid string List to test
        /// </summary>
        private readonly IList<string> _validStringsList;

        /// <summary>
        /// Few in-valid string List to test
        /// </summary>
        private readonly IList<string> _inValidStringsList;

        /// <summary>
        /// A dictionary of Roman strings with expected int value.
        /// </summary>
        private readonly IDictionary<string, int> _romanExpectedInt;

        #endregion

        #region Constructor

        /// <summary>
        /// To assign private readonly property
        /// </summary>
        public RomanToIntConverterTests()
        {
            _romanToIntConverter = new RomanToIntConverter();
            _validStringsList = new List<string>
            {
                "I",    "V",    "X",    "L",    "C",   "D",    "M",
                "II",   "VV",   "XX",   "LL",   "CC",  "DD",   "MM",
                "III",  "VVV",  "XXX",  "LLL",  "CCC", "DDD",  "MMM",
                "IV",   "IVI",  "IX",   "IXIII","VII", "XIII", "IXVIV",
                "XL",   "XLX",  "XC",   "XCXXX","CXX", "LXXX", "CCCXCXL",
                "CD",   "CDC",  "CM",   "CMCCC","DCC", "MCCC", "MDCLXVI",
                "MMMCMDDDCDCCCXCLLLXLXXXIXVVVIVIII"
            };

            _inValidStringsList = new List<string>
            {
                "", "A", "Vijay", "VX", "IVXLCDM",
                "MMMCMDDDCDCCCXCLLLXLXXXIXVVVIVIIII"
            };

            _romanExpectedInt = new Dictionary<string, int>
            {
                { "MMMCMDDDCDCCCXCLLLXLXXXIXVVVIVIII", 6441},
                { "CCCXCXL", 430},
                { "MDCLXVI", 1666},
                { "IXVIV", 18}
            };
        }

        #endregion

        #region IsValidRomanString Tests

        /// <summary>
        /// Empty Input
        /// </summary>
        [TestMethod]
        public void IsValidRomanStringEmptyTest()
        {
            var actual = _romanToIntConverter.IsValidRomanString(string.Empty);
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Valid strings test
        /// </summary>
        [TestMethod]
        public void IsValidRomanValidTest()
        {
            foreach (var input in _validStringsList)
            {
                Assert.IsTrue(_romanToIntConverter.IsValidRomanString(input));
            }
        }

        /// <summary>
        /// In-Valid strings test
        /// </summary>
        [TestMethod]
        public void IsValidRomanInValidTest()
        {
            foreach (var input in _inValidStringsList)
            {
                Assert.IsFalse(_romanToIntConverter.IsValidRomanString(input));
            }
        }

        #endregion

        #region ConvertToInt test

        /// <summary>
        /// Test of few valid strings.
        /// </summary>
        [TestMethod]
        public void ConvertToIntTest()
        {
            var expected = 0;
            var actual = 0;
            foreach (var item in _romanExpectedInt)
            {
                expected = item.Value;

                actual = _romanToIntConverter.ConvertToInt(item.Key);

                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        /// Test of an invalid input.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidRomanInputSequanceException))]
        public void ConvertToIntInValidInputTest()
        {
            _romanToIntConverter.ConvertToInt("ABC");
        }

        #endregion
    }
}