namespace InterGalaxcyCommunicator.Utility.Tests
{
    using InterGalaxcyCommunicator.Contracts;
    using InterGalaxcyCommunicator.Utility.CustomExceptions;
    using InterGalaxcyCommunicator.Utility.InputParser;
    using InterGalaxcyCommunicator.Utility.RomanToIntConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    
    [TestClass]
    public class InputParserTests
    {
        #region Private Readonly Variables

        /// <summary>
        /// Roman to Int Converter.
        /// </summary>
        private readonly IRomanToIntConverter _romanToIntConverter;

        /// <summary>
        /// Input Parser to invoke target methods
        /// </summary>
        private readonly IInputParser _inputParser;

        #endregion

        #region Constructor

        /// <summary>
        /// To assign private readonly property
        /// </summary>
        public InputParserTests()
        {
            _romanToIntConverter = new RomanToIntConverter();
            _inputParser = new InputParser(_romanToIntConverter);
        }

        #endregion

        #region Constructor Tests

        /// <summary>
        /// Empty Argument Constructor Test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentEmptyConstructorTest()
        {
            var parser = new InputParser(null);
            Assert.IsNotNull(parser);
            Assert.Fail();
        }

        /// <summary>
        /// Normal Constructor Test.
        /// </summary>
        public void NormalConstructorTest()
        {
            var parser = new InputParser(_romanToIntConverter);
            Assert.IsNotNull(parser);
        }

        #endregion

        #region Empty Input Tests

        /// <summary>
        /// Null input Test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyInputException))]
        public void ParseInputsNullTest()
        {
            _inputParser.ParseInputs(null);
        }

        /// <summary>
        /// Input Length 0 Test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyInputException))]
        public void ParseInputsLenthZeroTest()
        {
            var inputs = new string[0];
            _inputParser.ParseInputs(inputs);
        }

        /// <summary>
        /// All Inputs Empty Test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(EmptyInputException))]
        public void ParseInputsAllEmptyTest()
        {
            var inputs = new string[10];
            _inputParser.ParseInputs(inputs);
        }

        #endregion

        #region Total Invalid Input Tests

        /// <summary>
        /// Total invalid input Test
        /// </summary>
        [TestMethod]
        public void ParseInputsInvalidInputTest()
        {
            var inputs = new string[] {
                "This string contains invalid data.",
                "This is ",
                "Work what is",
                "This is InvalidRoman",
                "Do you like the question?",
                "This invalid line contains credits",
                "This line is with empty  credits",
                "This metal is having invalid credits"
            };

            var contracts = _inputParser.ParseInputs(inputs);

            Assert.IsNotNull(contracts);
            Assert.IsNull(contracts.RomanSymbolsValues);
            Assert.IsNull(contracts.MetalValues);
            Assert.IsNotNull(contracts.Questions);
            Assert.AreEqual(1, contracts.Questions.Count);
            Assert.IsNotNull(contracts.Questions[0]);
            Assert.AreEqual(QuestionType.Invalid, contracts.Questions[0].Type);
        }

        #endregion

        #region Valid Input Test

        /// <summary>
        /// Total Valid input Test
        /// </summary>
        [TestMethod]
        public void ParseInputsValidTest()
        {
            var inputs = new string[] {
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
            };

            var expected = new DataContract();
            expected.AddRomanSymbolsValues("itt", "I");
            expected.AddRomanSymbolsValues("vtt", "V");
            expected.AddRomanSymbolsValues("xtt", "X");
            expected.AddRomanSymbolsValues("ltt", "L");
            expected.AddRomanSymbolsValues("ctt", "C");
            expected.AddRomanSymbolsValues("dtt", "D");
            expected.AddRomanSymbolsValues("mtt", "M");
            expected.AddMetalValues("Metal", 10.0F);
            expected.AddMetalValues("TestMetal", 10.0F);
            expected.AddMetalValues("AnotherTestMetal", 20.0F);

            var question1 = new Question
            {
                PuzzlePart = "mtt mtt mtt ctt mtt dtt dtt dtt ctt dtt ctt ctt ctt xtt ctt ltt ltt ltt xtt ltt xtt xtt xtt itt xtt vtt vtt vtt itt vtt itt itt itt",
                Type = QuestionType.RomanToCredit
            };

            var question2 = new Question
            {
                PuzzlePart = "xtt ltt vtt itt Metal",
                Type = QuestionType.RomanMetalToCredit
            };

            var question3 = new Question
            {
                PuzzlePart = "ctt ctt ctt xtt ctt xtt ltt TestMetal",
                Type = QuestionType.RomanMetalToCredit
            };

            expected.AddQuestion(question1);
            expected.AddQuestion(question2);
            expected.AddQuestion(question3);

            var actual = _inputParser.ParseInputs(inputs);

            Assert.IsNotNull(actual);

            Assert.IsNotNull(actual.RomanSymbolsValues);
            Assert.IsNotNull(actual.MetalValues);
            Assert.IsNotNull(actual.Questions);

            Assert.AreEqual(expected.RomanSymbolsValues.Count, actual.RomanSymbolsValues.Count);
            Assert.AreEqual(expected.MetalValues.Count, actual.MetalValues.Count);
            Assert.AreEqual(expected.Questions.Count, actual.Questions.Count);

            foreach(var item in expected.RomanSymbolsValues)
            {
                Assert.IsTrue(actual.RomanSymbolsValues.ContainsKey(item.Key));

                Assert.AreEqual(actual.RomanSymbolsValues[item.Key], item.Value);
            }

            foreach (var item in expected.MetalValues)
            {
                Assert.IsTrue(actual.MetalValues.ContainsKey(item.Key));

                Assert.AreEqual(actual.MetalValues[item.Key], item.Value);
            }
        }

        #endregion
    }
}
