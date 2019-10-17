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
    class DecodeTest
    {

        [SetUp]
        public void Setup()
        {
            //lav dummy decoder og dummy data
            //Tests does nothing so far.
        }
        [TestCase()]
        public void DecodeReturnTrue()
        {
            Assert.IsTrue(true);
        }
        public void DecodeReturnFalse()
        {
            Assert.IsFalse(false);
        }
        public void DecodeReturnNothing()
        {
            Assert.IsTrue(true);
        }

    }
}