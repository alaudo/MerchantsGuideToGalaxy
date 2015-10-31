using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Helpers
{
    public static class LinqHelper
    {
        public static IEnumerable<T> AllButLast<T>(this IList<T> input)
        {
            return input.Take(input.Count - 1);
        }

        public static string AllButLastToString(this IList<string> input)
        {
            return string.Join(" ", input.AllButLast());
        }
    }
}
