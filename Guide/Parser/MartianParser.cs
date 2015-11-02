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

        /// <summary>
        /// Parsing one line from the conversion dialogue
        /// </summary>
        public static ParserResult ParseLine(string line, MartianToRomanConverter translator)
        {
            var words = line.SplitAndTrim(IS_SEPARATOR);

            // every valid line has "is" in it
            if (words.Count < 2) return new InvalidResult(line);
            else // " is " is a part of the number, we need to handle this properly
            {
                // in the conversion requests the number is to the right from "is", so we can safely split into 2 parts
                if (line.StartsWithSI("how much") || line.StartsWithSI("how many"))
                {
                    words = line.Split(IS_SEPARATOR, 2, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                // in all other cases the number is to the left and we can split by the last occurrence of the "is"
                else
                {
                    var isplit = line.LastIndexOf(IS_SEPARATOR[0]);
                    words = new List<string>()
                    {
                        line.Substring(0, isplit).Trim(),
                        line.Substring(isplit + IS_SEPARATOR[0].Length).Trim()
                    };
                }
            }
        
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
                int rate;
                // if there is space for currency and conversion rate is a positive integer
                if (left.Count > 1 && int.TryParse(right[0], out rate) && rate > 0)
                {
                    return new ConversionDefinition(left.AllButLastToString(), left.Last(), rate);
                }
                return new InvalidResult(line);
            }

            // case 3 : "how much is pish tegj glob glob ?"
            if (words[0].StartsWithSI("how much") && right.Last() == "?")
            {
                    return new ConversionRequest(right.AllButLastToString(), null);
            }

            // case 3 : "how many Credits is pish tegj glob glob ?"
            if (words[0].CompareToSI("how many Credits") && right.Last() == "?" && right.Count > 1)
            {
                var nums = right.AllButLast().ToList().AllButLastToString();
                var curs = right.AllButLast().Last();
                return new ConversionRequest(nums, curs);
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

                output.Add(translator.ExecuteRequest(res));
            }

            return
                string.Join(Environment.NewLine, output.Where(a => !string.IsNullOrWhiteSpace(a)));
        }

    }
}