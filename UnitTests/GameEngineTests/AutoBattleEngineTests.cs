using NUnit.Framework;
using Crawl.GameEngine;
using Xamarin.Forms.Mocks;
using Crawl.ViewModels;

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
            AutoBattleEngine auto = new AutoBattleEngine();

            // Act
            var _list = auto.GetListOfCharacter(Number);
            var Actual = _list.Count;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GetListOfCharacter_Number_0_Should_Fail()
        {
            // Initialize Mock
            MockForms.Init();

            // Arrange
            var Number = 0;
            AutoBattleEngine auto = new AutoBattleEngine();

            // Act
            var Actual = auto.GetListOfCharacter(Number);

            // Assert
            Assert.Null(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GetListOfCharacter_Neg_Number_Should_Fail()
        {
            // Initialize Mock
            MockForms.Init();

            // Arrange
            var Number = -1;
            AutoBattleEngine auto = new AutoBattleEngine();

            // Act
            var Actual = auto.GetListOfCharacter(Number);

            // Assert
            Assert.Null(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GetListOfCharacter_Number_Greater_Than_Dataset_Count_Should_Fail()
        {
            // Initialize Mock
            MockForms.Init();

            // Arrange
            AutoBattleEngine auto = new AutoBattleEngine();
            var _instance = CharactersViewModel.Instance;
            _instance.LoadCharactersCommand.Execute(null);
            var _count = _instance.Dataset.Count;
            var Number = ++_count;

            // Act
            var Actual = auto.GetListOfCharacter(Number);

            // Assert
            Assert.Null(Actual, TestContext.CurrentContext.Test.Name);
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
