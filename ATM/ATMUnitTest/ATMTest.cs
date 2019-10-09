using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM_Application;

namespace ATMUnitTest
{

    [TestFixture]
    class ATMTests_SomethingToTest
    {
        private ATM_Placeholder uut;

        [SetUp]
        public void Setup()
        {
            uut = new ATM_Placeholder();
        }
        //[TestCase()]
        public void Test_Something()
        {
            //Assert.That();
        }
    }
}