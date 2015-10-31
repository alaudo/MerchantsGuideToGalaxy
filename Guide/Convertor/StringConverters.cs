using Guide.Universal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Convertor
{
    public class MartianToRomanConverter
    {
        private readonly char[] NUMBER_SEPARATORS = { ' ' };

        Dictionary<string, Roman> _conversions;



        public bool TryAddConversion(string word, Roman r)
        {
            // if this conversion already defined then report error
            if (_conversions.ContainsKey(word)) return false;

            // else add this conversion
            _conversions[word] = r;
            return true;

        }

        public bool TryConvert(string input, out RomanNumber result)
        {
            result = new RomanNumber();

            // get our words
            var words = input
                        .Split(NUMBER_SEPARATORS, StringSplitOptions.RemoveEmptyEntries)
                        .Select(word => word.Trim())
                        .ToList();

            // check if we have an empty string
            if (words.Count == 0) return false;

            // check if all words are in the conversion dictionary
            if (!words.All(word => _conversions.ContainsKey(word))) return false;

            // now it is safe to convert
            result = new RomanNumber
                (
                    words
                        .Select(word => _conversions[word])
                        .ToList()
                );

            return true;

        }

    }
}
