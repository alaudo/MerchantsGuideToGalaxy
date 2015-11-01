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

        Dictionary<string, Roman> _conversions = new Dictionary<string, Roman>();
        Dictionary<string, Tuple<int,int>> _exchanges = new Dictionary<string, Tuple<int,int>>();

        public Tuple<int,int> this[string currency]
        {
            get
            {
                return _exchanges[currency];
            }

            private set { }

        }



        public bool TryAddConversion(string word, Roman r)
        {
            // if this conversion already defined then report error
            if (_conversions.ContainsKey(word)) return false;

            // else add this conversion
            _conversions[word] = r;
            return true;

        }

        public bool TryAddExchangeRate(string currency, int currencyvalue, int moneyvalue)
        {
            // if this conversion already defined then report error
            if (_conversions.ContainsKey(currency)) return false;

            // else add this conversion
            _exchanges[currency] = Tuple.Create(currencyvalue, moneyvalue);
            return true;

        }

        public bool TryConvert(string input, out RomanNumber result)
        {
            var seq = new List<Roman>();
            var ret = ParseNumber(input, _conversions, out seq);
            result = new RomanNumber(seq);
            return ret;
        }

        private bool ParseNumber(string input, Dictionary<string,Roman> algebra, out List<Roman> onumber)
        {
            onumber = new List<Roman>();

            input = input.Trim();
            while (input.Length > 0)
            {
                var next = algebra.Keys.Where(k => input.StartsWith(k)).ToList();

                if (next.Count() < 1)
                {
                    return false;
                }
                else
                {
                    var number = next.OrderByDescending(num => num.Length).First(); // take the longest that fits
                    onumber.Add(algebra[number]);
                    input = input.Substring(number.Length);
                }
                input = input.Trim();
            }
            return true;
        }

        internal bool IsValidCurrency(string currency)
        {
            return _exchanges.ContainsKey(currency);
        }
    }
}
