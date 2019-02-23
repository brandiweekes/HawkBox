using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawl.GameEngine;

namespace UnitTests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");

            var Actual = 10;
            var Expected = 10;

            Assert.AreEqual(Expected, Actual);
        }

        [Test]
        public void TestFormatOutput()
        {

            var Actual = new AutoBattleEngine().FormatOutput();
            var Expected = "";

            Assert.AreNotEqual(Expected, Actual);
        }
    }
}
