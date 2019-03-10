using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawl.GameEngine;
using Crawl.Models;
using Xamarin.Forms.Mocks;

namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class TurnEngineTests
    {
        [Test]
        public void TurnEngine_Character_TakeTurn_Monster_Target_Null_Should_Return_False()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testTurnEngine.MonsterList = null;

            // Act
            var returnResult = testTurnEngine.TakeTurn(testCharacter);

            // Assert          
            Assert.IsFalse(returnResult, "Expected Target: null, return false");
        }

        [Test]
        public void TurnEngine_Character_TakeTurn_Attack_Happens_Should_Return_True()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            var lowHealthDeadMonster = new Monster("Low Health Dead",
                                                "monster dead low health",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 1,
                                                null, null, null, null,
                                                null, null, null);
            var lowHealthMonster = new Monster("Low Health",
                                                "monster low health",
                                                "", 1, 1, true,
                                                1, 1, 1, 10, 2,
                                                null, null, null, null,
                                                null, null, null);

            var highHealthMonster = new Monster("High Health",
                                                "monster high health",
                                                "", 1, 1, true,
                                                1, 1, 1, 10, 10,
                                                null, null, null, null,
                                                null, null, null);
            testTurnEngine.MonsterList.Add(lowHealthDeadMonster);
            testTurnEngine.MonsterList.Add(lowHealthMonster);
            testTurnEngine.MonsterList.Add(highHealthMonster);          

            // Act
            var returnResult = testTurnEngine.TakeTurn(testCharacter);

            // Assert
            Assert.IsTrue(returnResult, "Expected Target: monster, return true");
        }

        [Test]
        public void TurnEngine_Character_AttackChoice_Monster_List_Null_Should_Return_Null()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testTurnEngine.MonsterList = null;

            // Act
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);

            // Assert
            Assert.IsNull(returnMonster, "Expected Monster choice: null");
        }

        [Test]
        public void TurnEngine_Character_AttackChoice_Monster_List_Empty_Should_Return_Null()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testTurnEngine.MonsterList = new List<Monster>();

            // Act
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);

            // Assert
            Assert.IsNull(returnMonster, "Expected Monster choice: null");
        }

        [Test]
        public void TurnEngine_Character_AttackChoice_Monster_List_Should_Return_Lowest_Health()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testTurnEngine.MonsterList = new List<Monster>();
            var lowHealthDeadMonster = new Monster("Low Health Dead",
                                                "monster dead low health",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 1,
                                                null, null, null, null,
                                                null, null, null);
            var lowHealthMonster = new Monster("Low Health", 
                                                "monster low health", 
                                                "", 1, 1, true, 
                                                1, 1, 1, 10, 2, 
                                                null, null, null, null, 
                                                null, null, null);

            var highHealthMonster = new Monster("High Health",
                                                "monster high health",
                                                "", 1, 1, true,
                                                1, 1, 1, 10, 10,
                                                null, null, null, null,
                                                null, null, null);
            testTurnEngine.MonsterList.Add(lowHealthDeadMonster);
            testTurnEngine.MonsterList.Add(lowHealthMonster);
            testTurnEngine.MonsterList.Add(highHealthMonster);

            // Act
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);

            // Assert
            Assert.AreSame(lowHealthMonster, returnMonster, "Expected Monster choice: Low Health");
        }

        [Test]
        public void TurnEngine_Character_AttackChoice_Monster_List_All_Alive_is_False_Should_Return_Null()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testTurnEngine.MonsterList = new List<Monster>();
            var lowHealthDeadMonster = new Monster("Low Health Dead",
                                                "monster dead low health 1",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 1,
                                                null, null, null, null,
                                                null, null, null);
            var lowHealthMonster = new Monster("Low Health",
                                                "monster dead health 2",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 2,
                                                null, null, null, null,
                                                null, null, null);

            var highHealthMonster = new Monster("High Health",
                                                "monster dead high health",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 10,
                                                null, null, null, null,
                                                null, null, null);
            testTurnEngine.MonsterList.Add(lowHealthDeadMonster);
            testTurnEngine.MonsterList.Add(lowHealthMonster);
            testTurnEngine.MonsterList.Add(highHealthMonster);

            // Act
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);

            // Assert
            Assert.IsNull(returnMonster, "Expected Monster choice: null");
        }
    }
}
