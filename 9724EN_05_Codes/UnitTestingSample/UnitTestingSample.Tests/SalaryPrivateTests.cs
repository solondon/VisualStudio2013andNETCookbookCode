using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingSample.Tests
{
    [TestClass]
    public class SalaryPrivateTests
    {
        [TestMethod]
        public void TestPrivateMember()
        {
            //arrange
            string name = "Abhishek";
            int age = 30;
            Salary osalary = new Salary(name, age);

            //act
            PrivateObject pObj = new PrivateObject(osalary);

            //assert
            Assert.AreEqual<int>(age, Convert.ToInt32(pObj.GetField("age")));
            Assert.AreEqual<string>(name, pObj.GetFieldOrProperty("name") as string);
        }

        [TestMethod]
        public void GetName_StaticMethodTest()
        {
            //arrange
            string name = "Abhishek";
            int age = 30;
            Salary osalary = new Salary(name, age);

            //act
            PrivateType pType = new PrivateType(typeof(Salary));
            string returnValue = pType.InvokeStatic("GetName", osalary) as string;

            //assert
            Assert.AreEqual(name, returnValue);
        }
    }
}
