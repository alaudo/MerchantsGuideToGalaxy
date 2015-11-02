using System;
using System.Collections.Generic;
using System.Linq;
using Guide.Convertor;
using Guide.Helpers;
using Guide.Universal;

namespace Guide.Parser
{
    public class MartianParser
    {
        public static readonly char[] WORD_SEPARATORS = {' '};
        public static readonly string[] IS_SEPARATOR = {" is "};
        public static readonly string[] LINE_SEPARATOR = {Environment.NewLine};
        public static readonly string PARSE_ERROR = "I have no idea what you are talking about";

        /// <summary>
        /// Parsing one line from the conversion dialogue
        /// </summary>
        public static ParserResult ParseLine(string line, MartianToRomanConverter translator)
        {
            var words = line.SplitAndTrim(IS_SEPARATOR);

            // every valid line has "is" in it
            if (words.Count != 2) return new InvalidResult(line);


            // case 1 : "glob is I"
            if (words.Last().IsRoman())
            {
                return new DigitDefinition(words[0], words[1].ToRoman());
            }

            var left = words[0].SplitAndTrim(WORD_SEPARATORS);
            var right = words[1].SplitAndTrim(WORD_SEPARATORS);

            // case 2 : "glob glob Silver is 34 Credits"
            if (right.Count == 2 && right[0].IsInteger() && right[1].CompareToCI("Credits"))
            {
                RomanNumber num;
                // if the number is valid
                if (left.Count > 1 && translator.TryConvert(left.AllButLastToString(), out num))
                {
                    return new ConversionDefinition(num, left.Last(), right[0].ToInteger());
                }
                return new InvalidResult(line);
            }

            // case 3 : "how much is pish tegj glob glob ?"
            if (words[0].CompareToSI("how much") && right.Last() == "?")
            {
                RomanNumber num;
                if (translator.TryConvert(right.AllButLastToString(), out num))
                {
                    return new ConversionRequest(num, null, right.AllButLastToString());
                }
                return new InvalidResult(line);
            }

            // case 3 : "how much is pish tegj glob glob ?"
            if (words[0].CompareToSI("how many Credits") && right.Last() == "?")
            {
                RomanNumber num;
                var nums = right.AllButLast().ToList().AllButLastToString();
                var curs = right.AllButLast().Last();
                if (translator.TryConvert(nums, out num))
                {
                    return new ConversionRequest(num, curs, right.AllButLastToString());
                }
                return new InvalidResult(line);
            }

            // if nothing matched than we couldn't parse it
            return new InvalidResult(line);
        }

        /// <summary>
        /// Parses the complete text and returns the result as text
        /// </summary>
        public static string Parse(string text, MartianToRomanConverter translator)
        {
            var lines = text.SplitAndTrim(LINE_SEPARATOR);
            var output = new List<string>();

            foreach (var line in lines)
            {
                var res = ParseLine(line, translator);

                output.Add(ExecuteRequest(res, ref translator));
            }

            return
                string.Join(Environment.NewLine, output.Where(a => !string.IsNullOrWhiteSpace(a)));
        }

        /// <summary>
        /// Perform the requested action
        /// </summary>
        public static string ExecuteRequest(ParserResult res, ref MartianToRomanConverter translator)
        {
            if (res is DigitDefinition)
            {
                var r = res as DigitDefinition;
                if (translator.TryAddWordForNumeral(r.Word, r.Digit))
                {
                    return string.Empty;
                }

                return PARSE_ERROR;
            }

            if (res is ConversionDefinition)
            {
                var r = res as ConversionDefinition;
                if (r.Number.IsValid && translator.TryAddExchangeRate(r.Currency, r.Credits, r.Number.ToInteger()))
                {
                    return string.Empty;
                }

                return PARSE_ERROR;
            }

            if (res is ConversionRequest)
            {
                var r = res as ConversionRequest;
                if (r.Number.IsValid)
                {
                    var amount = r.Number.ToInteger();

                    ExchangeRate rate;

                    // determining exchange rate for conversion
                    if (string.IsNullOrWhiteSpace(r.Currency)) rate = ExchangeRate.Credits;
                    else rate = translator[r.Currency];

                    var result = rate.CurrencyAmount*amount/rate.CreditAmount;

                    var ret =
                        string.Format("{0} is {1} Credits", r.Original, result);

                    return ret;
                }
            }

            return PARSE_ERROR;
        }
    }
}