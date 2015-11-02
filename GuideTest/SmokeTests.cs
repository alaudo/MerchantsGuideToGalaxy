using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guide.Convertor;
using Guide.Parser;

namespace GuideTest
{
    /// <summary>
    /// Basic test from task specification
    /// </summary>
    [TestClass]
    public class SmokeTests
    {
        [TestMethod]
        public void TestFromTaskSpecification()
        {
            // ARRANGE
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

            // ACT
            var translator = new MartianToRomanConverter();
            var result = MartianParser.Parse(input, translator);

            // ASSERT
            var rlines = result.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(5, rlines.Length, "There should be 5 lines in the result");
            Assert.AreEqual(42, Convert.ToInt32(rlines[0].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).Last()), "First conversion should give 42");
            Assert.AreEqual(68, Convert.ToInt32(rlines[1].Split(new char[] { ' ' })[4]), "Second conversion should give 68");
            Assert.AreEqual(57800, Convert.ToInt32(rlines[2].Split(new char[] { ' ' })[4]), "Third conversion should give 57800");
            Assert.AreEqual(782, Convert.ToInt32(rlines[3].Split(new char[] { ' ' })[4]), "Fourth conversion should give 782");
            Assert.AreEqual("I have no idea what you are talking about", rlines[4], "Last line should contain parsing error");
        }
    }
}
