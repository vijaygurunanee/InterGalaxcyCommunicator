namespace InterGalaxcyCommunicator.Utility.Tests
{
    using InterGalaxcyCommunicator.Utility.AnswerGenerator;
using InterGalaxcyCommunicator.Utility.CommunicationProcessor;
using InterGalaxcyCommunicator.Utility.FileReader;
using InterGalaxcyCommunicator.Utility.InputParser;
using InterGalaxcyCommunicator.Utility.RomanToIntConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class to create test cases of CommunicationProcessor.
    /// </summary>
    [TestClass]
    public class CommunicationProcessorTests
    {
        #region Private Read Only Variables

        /// <summary>
        /// File Reader.
        /// </summary>
        private readonly IFileReader _fileReader;

        /// <summary>
        /// Roman to Int Converter.
        /// </summary>
        private readonly IRomanToIntConverter _romanToIntConverter;

        /// <summary>
        /// Input Parser.
        /// </summary>
        private readonly IInputParser _inputParser;

        /// <summary>
        /// Answer Generator.
        /// </summary>
        private readonly IAnswerGenerator _answerGenerator;

        /// <summary>
        /// Communication Processor - Target Object.
        /// </summary>
        private readonly ICommunicationProcessor _communicationProcessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to assign private variables
        /// </summary>
        public CommunicationProcessorTests()
        {
            _fileReader = new MockFileReader();
            _romanToIntConverter = new RomanToIntConverter();
            _inputParser = new InputParser(_romanToIntConverter);
            _answerGenerator = new AnswerGenerator(_romanToIntConverter);
            _communicationProcessor = new CommunicationProcessor(_fileReader, _inputParser, _answerGenerator);
        }

        #endregion

        #region Constructor Tests

        /// <summary>
        /// File Reader Null Constructor Test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileReaderNullConstructorTest()
        {
            var cp = new CommunicationProcessor(null, _inputParser, _answerGenerator);
            Assert.IsNotNull(cp);
            Assert.Fail();
        }

        /// <summary>
        /// Input Parser Null Constructor Test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InputParserNullConstructorTest()
        {
            var cp = new CommunicationProcessor(_fileReader, null, _answerGenerator);
            Assert.IsNotNull(cp);
            Assert.Fail();
        }

        /// <summary>
        /// Answer Generator Null Constructor Test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AnswerGeneratorNullConstructorTest()
        {
            var cp = new CommunicationProcessor(_fileReader, _inputParser, null);
            Assert.IsNotNull(cp);
            Assert.Fail();
        }

        #endregion

        #region Method Test

        /// <summary>
        /// Main method tests.
        /// </summary>
        [TestMethod]
        public void ProcessInputsTests()
        {
            var expected = new List<string>
            {
                " mtt mtt mtt ctt mtt dtt dtt dtt ctt dtt ctt ctt ctt xtt ctt ltt ltt ltt xtt ltt xtt xtt xtt itt xtt vtt vtt vtt itt vtt itt itt itt  is 6441",
                " xtt ltt vtt itt Metal  is 460 Credits",
                " ctt ctt ctt xtt ctt xtt ltt TestMetal  is 4300 Credits",
                " AnotherTestMetal  is 20 Credits"
            };

            var actual = _communicationProcessor.ProcessInputs();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var ans in expected)
            {
                Assert.IsTrue(actual.Contains(ans));
            }
        }

        #endregion
    }
}
