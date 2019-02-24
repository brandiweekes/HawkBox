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

        [Test]
        public void GetFinalScoreObject_Default_Score_Should_Pass()
        {
            MockForms.Init();

            //arrange
            AutoBattleEngine auto = new AutoBattleEngine();

            var Expected = 0;

            //act
            var score = auto.GetFinalScoreObject();
            var Actual = score.ScoreTotal;

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GetFinalScoreObject_Score_Total_Equals_25_Should_Pass()
        {
            MockForms.Init();

            //arrange
            AutoBattleEngine auto = new AutoBattleEngine();
            auto.BattleEngine.BattleScore.ScoreTotal = 25;

            var Expected = 25;

            //act
            var score = auto.GetFinalScoreObject();
            var Actual = score.ScoreTotal;

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
    }
}
