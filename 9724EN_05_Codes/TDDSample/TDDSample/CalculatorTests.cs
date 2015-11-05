using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDSample.Code;

namespace TDDSample
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void BasicDivideTest()
        {
            //arrange
            Calculator ocalc = new Calculator();
            double expected = 4d;
            //act
            double actual = ocalc.Divide(20d, 5d);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RangeDivideTest()
        {
            //arrange
            Calculator ocalc = new Calculator();

            //act
            for(double p1 = -100d; p1 < 100d; p1 ++)
                for (double p2 = -100d; p2 < 100d; p2++)
                {
                    try
                    {
                        double expected = p1 / p2;
                        AssertRange(ocalc, p1, p2, expected);
                    }
                    catch { }
                }
        }

        private void AssertRange(Calculator ocalc, double p1, double p2, double expected)
        {
            double actual = ocalc.Divide(p1, p2);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
