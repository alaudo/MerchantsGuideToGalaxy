using Guide.Universal;

namespace Guide.Parser
{
    /// <summary>
    /// Request to convert value ("how much/many xxx SMT ?")
    /// F# syntax: ConversionRequest of RomanNumber * string * string
    /// </summary>
    public class ConversionRequest : ParserResult
    {
        public readonly string Number;

        public readonly string Currency;

        public ConversionRequest(string number, string currency)
        {
            Number = number; Currency = currency; 
        }

    }
}