using System;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guide.Convertor;
using Guide.Parser;
using GuideTest.Helpers;

namespace GuideTest
{
    /// <summary>
    /// Test unit and vocabulary conversions
    /// </summary>
    [TestClass]
    public class ConversionTests
    {
        internal readonly string ROMAN_VOCABULARY =
                          @"I is I
                            V is V
                            X is X
                            L is L
                            C is C
                            D is D
                            M is M";

        /// <summary>
        /// Test basic conversions
        /// </summary>
        [TestMethod]
        public void MultipleSimpleConversionsTest()
        {
            // ARRANGE
            var input = string.Join(Environment.NewLine,
                                    ROMAN_VOCABULARY,

                          @"how much is I ?
                            how much is II ?
                            how much is III ?
                            how much is IV ?
                            how much is V ?
                            how much is VI ?
                            how much is VII ?
                            how much is VIII ?
                            how much is IX ?
                            how much is X ?
                            how much is XI ?
                            how much is XII ?
                            how much is XIII ?
                            how much is XIV ?
                            how much is XV ?
                            how much is XVI ?
                            how much is XVII ?
                            how much is XVIII ?
                            how much is XIX ?
                            how much is XX ?
                            how much is XXXIX ?
                            how much is XL ?
                            how much is LIV ?
                            how much is LIX ?
                            how much is LX ?
                            how much is LXX ?
                            how much is LXXX ?
                            how much is LXXXIV ?
                            how much is LXXXV ?
                            how much is XC ?
                            how much is XCI ?
                            how much is XCII ?
                            how much is XCIV ?
                            how much is XCVI ?
                            how much is XCIX ?
                            how much is C ?
                            how much is CXL ?
                            how much is CXC ?
                            how much is CC ?
                            how much is CCC ?
                            how much is CCCXCIX ?
                            how much is CD ?
                            how much is CDIV ?
                            how much is CDIX ?
                            how much is DCCCXLIV ?
                            how much is CMXCIX ?
                            how much is M ?
                            how much is MMM ?
                            how much is MMMCMXCIX ?");

            // ACT
            var translator = new MartianToRomanConverter();
            var result = MartianParser.Parse(input, translator);

            // ASSERT
            var rdata = result.ExtractValues();
            var refdata = new int[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 39, 40, 54, 59, 60, 70, 80, 84, 85,
                90, 91, 92, 94, 96, 99, 100, 140, 190, 200, 300, 399, 400, 404, 409, 844, 999, 1000, 3000, 3999
            };
            CollectionAssert.AreEquivalent(refdata, rdata, "Some number conversions failed");
        }

        /// <summary>
        /// Numerals consisting of several words some of which might repeat in different numerals
        /// </summary>
        [TestMethod]
        public void MultipleAndRepetitiveWordsAsNumeralsTest()
        {
            // ARRANGE
            var input = @"glok is I
                            glok bok is V
                            glok wok is X
                            glok rok is L
                            how much is glok glok bok ?
                            how much is glok glok wok ?
                            how much is glok bok glok ?
                            how much is glok wok glok ?
                            how much is glok wok glok rok glok glok bok ?"; 

            // ACT
            var translator = new MartianToRomanConverter();
            var result = MartianParser.Parse(input, translator);

            // ASSERT
            var rdata = result.ExtractValues();
            var rs = string.Join(",", rdata);
            var refdata = new int[]
            {
                4, 9, 6, 11, 44
            };

            CollectionAssert.AreEquivalent(refdata, rdata, "Some conversions with repetitive words failed");
        }


        /// <summary>
        /// Test basic conversions with currency
        /// </summary>
        [TestMethod]
        public void MultipleExchangeRateConversionsTest()
        {
            // ARRANGE
            var input = string.Join(Environment.NewLine,
                                    ROMAN_VOCABULARY,

                          @"I Cent is 1 Credits
                            IV Quater is 100 Credits
                            X Nickel is 50 Credits
                            XX Dime is 200 Credits                            
                            how many Credits is MMCD Cent ?
                            how many Credits is I Quater ?
                            how many Credits is II Quater ?
                            how many Credits is III Quater ?
                            how many Credits is V Quater ?
                            how many Credits is VI Quater ?
                            how many Credits is I Nickel ?
                            how many Credits is III Nickel ?
                            how many Credits is IV Nickel ?
                            how many Credits is V Nickel ?
                            how many Credits is VI Nickel ?
                            how many Credits is VII Nickel ?
                            how many Credits is IX Nickel ?
                            how many Credits is XI Nickel ?
                            how many Credits is XIII Nickel ?
                            how many Credits is XIX Nickel ?
                            how many Credits is MDC Nickel ?
                            how many Credits is I Dime ?
                            how many Credits is II Dime ?
                            how many Credits is IX Dime ?
                            how many Credits is XIX Dime ?
                            how many Credits is XXI Dime ?
                            how many Credits is CIV Dime ?
                            how many Credits is DL Dime ?");

            // ACT
            var translator = new MartianToRomanConverter();
            var result = MartianParser.Parse(input, translator);

            // ASSERT
            var rdata = result.ExtractValues();
            var rs = string.Join(",", rdata);
            var refdata = new int[]
            {
                2400, 25, 50, 75, 125, 150, 5, 15, 20, 25, 30, 35, 45, 55, 65, 95, 8000, 10, 20, 90, 190, 210, 1040,
                5500
            };

            CollectionAssert.AreEquivalent(refdata, rdata, "Some currency conversions failed");
        }

        /// <summary>
        /// Numerals consisting of several words some of which might repeat in different numerals
        /// </summary>
        [TestMethod]
        public void NegativeConversionTest()
        {
            // ARRANGE
            var input = @"I is I
                            I is V
                            V is V
                            X is X
                            L is L
                            XV I is 49 Credits
                            XVI Gold is -40 Credits
                            XV Gold is 45 Credits
                            III Silver is 1.5 Credits
                            X Gold is 30 Credits";

            // ACT
            var translator = new MartianToRomanConverter();
            var result = MartianParser.Parse(input, translator);

            // ASSERT
            var rdata = result.ExtractValues();
            var rs = string.Join(",", rdata);
            var refdata = new int[]
            {
                -1, -1, -1, -1, -1
            };
            //                I is I
            //                I is V                               => you cannot override existing number
            //                V is V
            //                X is X
            //                L is L
            //                XV I is 49 Credits                    => currency cannot be same as digit
            //                XVI Gold is -40 Credits               => negative conversion is invalid
            //                XV Gold is 45 Credits
            //                III Silver is 1.5 Credits             => conversion values cannot be fractional (only integer)
            //                X Gold is 30 Credits                  => redefinition of once declared conversion is impossible    

            CollectionAssert.AreEquivalent(refdata, rdata, "Some conversions with repetitive words failed");
        }


    }
}
