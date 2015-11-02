using Guide.Universal;

namespace Guide.Parser
{
    /// <summary>
    /// Digit definition class ("xxx is L")
    /// F# syntax: DigitDefinition of string * Roman
    /// </summary>
    public class DigitDefinition: ParserResult
    {
        public readonly string Word;

        public readonly Roman Digit;

        public DigitDefinition(string word, Roman digit)
        {
            Word = word; Digit = digit;
        }
    }
}