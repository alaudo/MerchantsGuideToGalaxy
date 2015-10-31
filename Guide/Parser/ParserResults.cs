using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Parser
{
    public abstract class ParserResult
    {
    }

    public class Definition (string word) : ParserResult
    {
        public string Word { get; private set; } = word;

    }
}
