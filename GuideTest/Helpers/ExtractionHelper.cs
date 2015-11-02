using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTest.Helpers
{
    public static class ExtractionHelper
    {
        public static List<int> ExtractValues(this string conversionresult)
        {
            var rlines = conversionresult.Split(new string[] {Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);
            var ret = new List<int>();
            foreach (var line in rlines)
            {
                var data = line.Trim();
                int num = -1;
                if (!data.EndsWith("about"))
                {
                    
                    var dwords = data.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var foo = ((dwords.Last() == "Credits" && int.TryParse(dwords[dwords.Length - 2], out num))
                               || (int.TryParse(dwords.Last(), out num)));
                }
                ret.Add(num);
            }

            return ret;
        }
    }
}
