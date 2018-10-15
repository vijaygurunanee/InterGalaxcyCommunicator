namespace InterGalaxcyCommunicator.Utility.Tests
{
    using InterGalaxcyCommunicator.Contracts;
    using InterGalaxcyCommunicator.Utility.AnswerGenerator;
    using InterGalaxcyCommunicator.Utility.CustomExceptions;
    using InterGalaxcyCommunicator.Utility.InputParser;
    using InterGalaxcyCommunicator.Utility.RomanToIntConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class AnswerGeneratorTests
    {
        #region Private Readonly Variables

        /// <summary>
        /// Roman to Int Converter.
        /// </summary>
        private readonly IRomanToIntConverter _romanToIntConverter;

        /// <summary>
        /// Input Parser to invoke target methods
        /// </summary>
        private readonly IAnswerGenerator _answerGenerator;

        #endregion

        #region Constructor

        /// <summary>
        /// To assign private readonly property
        /// </summary>
        public AnswerGeneratorTests()
        {
            _romanToIntConverter = new RomanToIntConverter();
            _answerGenerator = new AnswerGenerator(_romanToIntConverter);
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
            var answerGenerater = new AnswerGenerator(null);
            Assert.IsNotNull(answerGenerater);
            Assert.Fail();
        }

        /// <summary>
        /// Normal Constructor Test.
        /// </summary>
        public void NormalConstructorTest()
        {
            var answerGenerator = new InputParser(_romanToIntConverter);
            Assert.IsNotNull(answerGenerator);
        }

        #endregion

        #region Empty Input Tests

        /// <summary>
        /// Null input Test
        /// </summary>
        [TestMethod]
        public void GenerateAnswersNullTest()
        {
            var answers = _answerGenerator.GenerateAnswers(null);
            Assert.IsNull(answers);
        }

        /// <summary>
        /// question null input Test
        /// </summary>
        [TestMethod]
        public void GenerateAnswersQuestionNullTest()
        {
            var contract = new DataContract();
            var answers = _answerGenerator.GenerateAnswers(contract);
            Assert.IsNull(answers);
        }

        /// <summary>
        /// questions empty input Test
        /// </summary>
        [TestMethod]
        public void GenerateAnswersQuestionEmptyTest()
        {
            var contract = new DataContract();
            contract.AddQuestion(new Question());
            var answers = _answerGenerator.GenerateAnswers(contract);
            Assert.IsNull(answers);
        }

        #endregion

        #region Valid Input Tests

        /// <summary>
        /// A complete method to test the logics.
        /// </summary>
        [TestMethod]
        public void GenerateAnswersTest()
        {
            var input = new DataContract();
            input.AddRomanSymbolsValues("itt", "I");
            input.AddRomanSymbolsValues("vtt", "V");
            input.AddRomanSymbolsValues("xtt", "X");
            input.AddRomanSymbolsValues("ltt", "L");
            input.AddRomanSymbolsValues("ctt", "C");
            input.AddRomanSymbolsValues("dtt", "D");
            input.AddRomanSymbolsValues("mtt", "M");
            input.AddMetalValues("Metal", 10.0F);
            input.AddMetalValues("TestMetal", 10.0F);
            input.AddMetalValues("AnotherTestMetal", 20.0F);

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

            var question4 = new Question
            {
                PuzzlePart = "AnotherTestMetal",
                Type = QuestionType.RomanMetalToCredit
            };

            input.AddQuestion(question1);
            input.AddQuestion(question2);
            input.AddQuestion(question3);
            input.AddQuestion(question4);

            var expected = new List<string>
            {
                "mtt mtt mtt ctt mtt dtt dtt dtt ctt dtt ctt ctt ctt xtt ctt ltt ltt ltt xtt ltt xtt xtt xtt itt xtt vtt vtt vtt itt vtt itt itt itt is 6441",
                "xtt ltt vtt itt Metal is 460 Credits",
                "ctt ctt ctt xtt ctt xtt ltt TestMetal is 4300 Credits",
                "AnotherTestMetal is 20 Credits"
            };

            var actual = _answerGenerator.GenerateAnswers(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);

            foreach(var ans in expected)
            {
                Assert.IsTrue(actual.Contains(ans));
            }
        }

        #endregion
    }
}
