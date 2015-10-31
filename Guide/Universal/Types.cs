using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Universal
{
   /// <summary>
   /// Enumeration with intergrated mapping
   /// </summary>
   public enum Roman 
    {
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000
    }

    public class RomanNumber     {

        /// <summary>
        /// Internal representation of the number
        /// </summary>
        readonly List<Roman> _digits;

        public override string ToString()
        {
            return string.Join(string.Empty, _digits.Select(d => d.ToString()));
        }

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="digits"></param>
        public RomanNumber(List<Roman> digits)
        {
            _digits = digits;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public RomanNumber()
        {
            _digits = new List<Roman>();
        }
        
    }



}
