using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingSample.Tests
{
    [TestClass]
    public class SalaryTests
    {
        [TestMethod]
        public void GetSalary_ValidValues_TestFailed()
        {
            //arrange
            int age = 50;
            float expected = (20000 * (age - 20)) * 0.5f;
            Salary osalary = new Salary("John", age);

            //act
            float actual = osalary.GetSalary();

            //assert
            Assert.AreEqual(expected, actual, string.Format("Expected {0} not equals actual {1}", expected, actual));
        }

        [TestMethod]
        public void GetSalary_ValidValues_TestPassed()
        {
            //arrange
            int age = 50;
            float expected = (20000 * (age - 20)) * 1;
            Salary osalary = new Salary("John", age);

            //act
            float actual = osalary.GetSalary();

            //assert
            Assert.AreEqual(expected, actual, string.Format("Expected {0} not equals actual {1}", expected, actual));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetSalary_WhenAgeLessThanTwenty__ShouldThrowException()
        {
            //arrange
            int age = 15;
            Salary osalary = new Salary("John", age);

            //act
            float actual = osalary.GetSalary();

            //assert

        }

        [TestMethod]
        public void GetSalary_WhenGreaterThanEightyFive_ShouldThrowException()
        {
            //arrange
            int age = 90;
            Salary osalary = new Salary("John", age);

            //act
            try
            {
                float actual = osalary.GetSalary();
            }
            catch (ArgumentOutOfRangeException e)
            {
                //assert
                StringAssert.Contains(e.Message, Salary.ARGUMENTGREATERTHAN85);
                return;
            }
            Assert.Fail("No exception was thrown");
        }
        [TestMethod]
        public void GetSalary_WhenGreaterThan60_ShouldReturnPensionAmount()
        {
            //arrange
            int age = 65;
            float expected = (20000 * 40) * 0.5f;
            Salary osalary = new Salary("John", age);

            //act
            float actual = osalary.GetSalary();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
