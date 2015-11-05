using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSFakesSample.Fakes;
using Microsoft.QualityTools.Testing.Fakes.Stubs;
using Microsoft.QualityTools.Testing.Fakes;
using System.Fakes;
using System.IO.Fakes;
using Microsoft.QualityTools.Testing.Fakes.Shims;

namespace MSFakesSample.Tests
{
    [TestClass]
    public class TestFakesSample
    {
        [TestMethod]
        public void GetEventName_Test()
        {
            //Arrange
            var sLogger = new StubILogger
            {
                IsLoggerEnabledGet = () => true
            };
            var sut = new DiagonizeStubs();
            //Act
            var result = sut.GetEventName(sLogger);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
        [TestMethod]
        public void GetEventName_Test_Fakes()
        {
            //arrange 
            var stubLogger = new StubILogger { IsLoggerEnabledGet = () => true };
            var diagonizeStub = new DiagonizeStubs();
            var observer = new StubObserver();
            stubLogger.InstanceObserver = observer;

            //act
            diagonizeStub.GetEventName(stubLogger);
            var calls = observer.GetCalls();
            
           //assert
            Assert.IsNotNull(calls);
            Assert.AreEqual(2, calls.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DoomsDay_Test_Shims()
        {
            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2012, 12, 21);
                DiagonizeShims.DoomsDay();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ShimNotImplementedException))]
        public void DeleteDirectory_BehaveNotImplemented()
        {
            using (ShimsContext.Create())
            {
                ShimDirectory.BehaveAsNotImplemented();
                DiagonizeShims.DeleteTemporaryData(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }
    }
}
