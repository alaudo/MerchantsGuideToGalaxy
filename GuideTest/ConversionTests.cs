using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guide.Convertor;
using Guide.Parser;

namespace GuideTest
{
    /// <summary>
    /// Test unit and vocabulary conversions
    /// </summary>
    [TestClass]
    public class ConversionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = @"glob is I
                            prok is V
                            pish is X
                            tegj is L
                            glob glob Silver is 34 Credits
                            glob prok Gold is 57800 Credits
                            pish pish Iron is 3910 Credits
                            how much is pish tegj glob glob ?
                            how many Credits is glob prok Silver ?
                            how many Credits is glob prok Gold ?
                            how many Credits is glob prok Iron ?
                            how much wood could a woodchuck chuck if a woodchuck could chuck wood ?";
            var translator = new MartianToRomanConverter();
            MartianParser.Parse(input, translator);

        }
    }
}
