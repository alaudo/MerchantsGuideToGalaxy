namespace Guide.Universal
{
    /// <summary>
    /// Exchange rate for credits
    /// </summary>
    public struct ExchangeRate
    {
        public readonly string Name;
        public readonly int CurrencyAmount;
        public readonly int CreditAmount;

        public ExchangeRate(string name, int curamount, int crdamount)
        {
            Name = name;
            CurrencyAmount = curamount;
            CreditAmount = crdamount;
        }

        public static ExchangeRate Credits
        {
            get
            {
                return new ExchangeRate("Credits",1,1);
            }
        }
    }
}