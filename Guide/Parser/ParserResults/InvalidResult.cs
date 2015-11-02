namespace Guide.Parser
{
    /// <summary>
    /// Failed to parse result or parsed data failed validation
    /// F# syntax: InvalidResult of string
    /// </summary>
    public class InvalidResult : ParserResult
    {
        public readonly string Original;

        public InvalidResult(string original)
        {
            Original = original;
        }

    }
}