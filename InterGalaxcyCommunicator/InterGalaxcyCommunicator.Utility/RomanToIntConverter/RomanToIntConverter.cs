namespace InterGalaxcyCommunicator.Utility.RomanToIntConverter
{
    using CustomExceptions;
    using InterGalaxcyCommunicator.Contracts;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Converts Roman strings into Decimal Below mentioned Rules are followed.
    /// 
    /// Numbers are formed by combining symbols together and adding the values.
    /// For example, MMVI is 1000 + 1000 + 5 + 1 = 2006. Generally, symbols are
    /// placed in order of value, starting with the largest values. When smaller
    /// values precede larger values, the smaller values are subtracted from the
    /// larger values, and the result is added to the total.
    /// 
    /// For example MCMXLIV = 1000 + (1000 − 100) + (50 − 10) + (5 − 1) = 1944.
    /// 
    /// The symbols "I", "X", "C", and "M" can be repeated three times in succession, but no more.
    /// (They may appear four times if the third and fourth are separated by a smaller value,
    /// such as XXXIX.) "D", "L", and "V" can never be repeated.
    /// 
    /// "I" can be subtracted from "V" and "X" only.
    /// "X" can be subtracted from "L" and "C" only.
    /// "C" can be subtracted from "D" and "M" only.
    /// "V", "L", and "D" can never be subtracted.
    /// Only one small-value symbol may be subtracted from any large-value symbol.
    /// 
    /// A number written in Arabic numerals can be broken into digits.
    /// For example, 1903 is composed of 1, 9, 0, and 3.
    /// To write the Roman numeral, each of the non-zero digits should be treated separately.
    /// In the above example, 1,000 = M, 900 = CM, and 3 = III.Therefore, 1903 = MCMIII.
    /// </summary>
    public class RomanToIntConverter : IRomanToIntConverter
    {
        #region Private Variables

        /// <summary>
        /// Dictionary to hold Roman Chars and their values.
        /// </summary>
        private readonly IDictionary<char, int> RomanDictionary;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to build Roman Dictionary
        /// </summary>
        public RomanToIntConverter()
        {
            RomanDictionary = new Dictionary<char, int>
            {
                {'I',   1       },
                {'V',   5       },
                {'X',   10      },
                {'L',   50      },
                {'C',   100     },
                {'D',   500     },
                {'M',   1000    }
            };
        }

        #endregion

        #region Interface Conversion Methods

        /// <summary>
        /// Validate the given input string.
        /// </summary>
        /// <param name="input">Input String.</param>
        /// <returns>true if input is valid Roman string.</returns>
        public bool IsValidRomanString(string input)
        {
            var romanRegEx = new Regex(Constants.RomanValidation, RegexOptions.IgnoreCase);

            return !string.IsNullOrEmpty(input) && romanRegEx.IsMatch(input);
        }

        /// <summary>
        /// Convert the given Roman input into equal integer value.
        /// </summary>
        /// <param name="input">Roman Input.</param>
        /// <returns>Int.</returns>
        public int ConvertToInt(string input)
        {
            if (!IsValidRomanString(input))
                throw new InvalidRomanInputSequanceException(Constants.InvalidRomanSequenceMessge);

            var inputSequence = input.ToUpper().ToCharArray();

            var convertedOutput = 0;
            var lastProcessedValue = 0;
            var currentValue = 0;

            for (int i = inputSequence.Length - 1; i >= 0 ; i--)
            {
                currentValue = RomanDictionary[inputSequence[i]];

                if (currentValue < lastProcessedValue)
                    convertedOutput -= currentValue;
                else
                    convertedOutput += currentValue;

                lastProcessedValue = currentValue;
            }

            return convertedOutput;
        }
        #endregion
    }
}
