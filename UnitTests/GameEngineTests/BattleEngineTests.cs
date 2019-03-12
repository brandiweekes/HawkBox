using Crawl.GameEngine;
using Crawl.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class BattleEngineTests
    {
        [Test]
        public void BattleEngine_Initialize_Should_Pass()
        {
            // Act
            BattleEngine battleEngine = new BattleEngine();

            //Assert
            Assert.NotNull(battleEngine.BattleScore, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(battleEngine.CharacterList, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_StartBattle_Should_Pass()
        {
            // Arrange
            BattleEngine battleEngine = new BattleEngine();

            // Act
            var Excepted = battleEngine.StartBattle(false);

            //Assert
            Assert.IsTrue(Excepted, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_StartBattle_Twice_Should_Fail()
        {
            // Arrange
            BattleEngine battleEngine = new BattleEngine();

            // Act
            battleEngine.StartBattle(false);
            var Excepted = battleEngine.StartBattle(false);

            //Assert
            Assert.IsFalse(Excepted, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_StartBattle_AutoBattle_True_Should_Pass()
        {
            // Arrange
            BattleEngine battleEngine = new BattleEngine();

            // Act
            battleEngine.StartBattle(true);
            var Excepted = battleEngine.GetAutoBattleState();

            //Assert
            Assert.IsTrue(Excepted, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_EndBattle_Should_Pass()
        {
            // Arrange
            BattleEngine battleEngine = new BattleEngine();

            // Act
            battleEngine.StartBattle(false);
            battleEngine.EndBattle();

            //Assert
            Assert.IsFalse(battleEngine.BattleRunningState(), TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_ClearData_Should_Pass()
        {
            // Arrange
            BattleEngine battleEngine = new BattleEngine();
            battleEngine.CharacterList = new List<Character>()
            {
                new Character(),
                new Character()
            };
            var Expected = 0; 

            // Act
            battleEngine.StartBattle(false);
            battleEngine.ClearData();
            var Actual = battleEngine.CharacterList.Count;

            //Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_GetFinalScore_Should_Pass()
        {
            // Arrange
            BattleEngine battleEngine = new BattleEngine();

            //Assert
            Assert.NotNull(battleEngine.GetFinalScore(), TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BattleEngine_FormatOutput_Should_Pass()
        {
            // Arrange
            BattleEngine battleEngine = new BattleEngine();
            battleEngine.CharacterList = new List<Character>()
            {
                new Character(),
                new Character()
            };

            //Assert
            Assert.NotNull(battleEngine.FormatOutput(), TestContext.CurrentContext.Test.Name);
        }
    }
}
