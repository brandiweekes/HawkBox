using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawl.GameEngine;
using Crawl.Models;

namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class TurnEngineTests
    {
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
            var lowHealthMonster = new Monster("Low Health", 
                                                "monster low health", 
                                                "", 1, 1, true, 
                                                1, 1, 1, 10, 1, 
                                                null, null, null, null, 
                                                null, null, null);
            var highHealthMonster = new Monster("High Health",
                                                "monster high health",
                                                "", 1, 1, true,
                                                1, 1, 1, 10, 10,
                                                null, null, null, null,
                                                null, null, null);
            testTurnEngine.MonsterList.Add(lowHealthMonster);
            testTurnEngine.MonsterList.Add(highHealthMonster);

            // Act
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);

            // Assert
            Assert.AreSame(lowHealthMonster, returnMonster, "Expected Monster choice: Low Health");
        }
    }
}
