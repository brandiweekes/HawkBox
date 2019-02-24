using NUnit.Framework;
using Crawl.GameEngine;
using Xamarin.Forms.Mocks;

namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class AutoBattleEngineTests
    {

        [Test]
        public void GetListOfCharacter_Number_2_Should_Pass()
        {
            // Initialize Mock
           MockForms.Init();

            // Arrange
            var Number = 2;
            var Expected = 2;

            // Act
            var _list = new AutoBattleEngine().GetListOfCharacter(Number);
            var Actual = _list.Count;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
    }
}
