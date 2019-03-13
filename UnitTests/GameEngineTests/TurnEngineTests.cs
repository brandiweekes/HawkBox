using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawl.GameEngine;
using Crawl.Models;
using Xamarin.Forms.Mocks;
using Crawl.ViewModels;

namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class TurnEngineTests
    {
        //Character vs Monster: character turn happens
        #region Tests: TakeTurn(Character Attacker)
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
        #endregion

        //Character vs Monster: character chooses attack against lowest health monster
        #region Tests: AttackChoice(Character Attacker)
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
        #endregion

        //Monster vs Character: monster chooses attack against highest speed character
        #region Tests: AttackChoice(Monster Attacker)
        [Test]
        public void TurnEngine_Monster_AttackChoice_CharacterList_Null_Should_Return_Null()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testTurnEngine.CharacterList = null;

            // Act
            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);

            // Assert
            Assert.IsNull(chosenCharacter, "Expected Character choice: null");
        }

        [Test]
        public void TurnEngine_Monster_AttackChoice_CharacterList_Empty_Should_Return_Null()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testTurnEngine.CharacterList = new List<Character>();

            // Act
            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);

            // Assert
            Assert.IsNull(chosenCharacter, "Expected Character choice: null");
        }

        [Test]
        public void TurnEngine_Monster_AttackChoice_CharacterList_Should_Return_Highest_Speed()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();

            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;
          
            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            // Act
            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);

            // Assert
            Assert.AreSame(highSpeedCharacter, chosenCharacter, "Expected Character choice: High Speed");
        }

        [Test]
        public void TurnEngine_Monster_AttackChoice_CharacterList_All_Alive_is_False_Should_Return_Null()
        {
            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();

            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Alive = false;
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Alive = false;
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            // Act
            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);

            // Assert
            Assert.IsNull(chosenCharacter, "Expected Character choice: null");
        }
        #endregion

        //Character vs Monster: character hits, deals damage, gains XP, levels up, item pool 
        #region Tests: TurnAsAttack(Character Attacker, int AttackScore, Monster Target, int DefenseScore)
        [Test]
        public void TurnEngine_Character_TurnAsAttack_Set_Attacker_Name_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Assert
            Assert.AreSame(testCharacter.Name, testTurnEngine.AttackerName, "Expected Attacker Name: Test Name");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Set_Defender_Name_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Assert
            Assert.AreSame(returnMonster.Name, testTurnEngine.TargetName, "Expected Target Defender Name: monster low health");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Increment_TurnCount_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Assert
            Assert.AreEqual(testTurnEngine.BattleScore.TurnCount, 1, "Expected TurnCount: 1");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Force_Miss_Should_Set_DamageAmount_Equal_Zero()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
                                                1, 1, 10, 10, 2,
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 2;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.Zero(testTurnEngine.DamageAmount, "Expected DamageAmount from Miss: 0");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Force_CriticalMiss_Should_Set_DamageAmount_Equal_Zero()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
                                                1, 1, 10, 10, 2,
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 1;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.Zero(testTurnEngine.DamageAmount, "Expected DamageAmount from CriticalMiss: 0");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Force_Hit_Should_Set_DamageAmount_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Attribute.Attack = 1;
            var testAttackDamage = testCharacter.GetDamageRollValue();
            testCharacter.Name = "Test Name";
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
                                                1, 1, 10, 10, 2,
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(testAttackDamage, testTurnEngine.DamageAmount, "Expected DamageAmount from Hit: greater than 0");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Force_Hit_Should_Deal_DamageAmount_To_Monster_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Attribute.Attack = 1;
            var testAttackDamage = testCharacter.GetDamageRollValue();
            testCharacter.Name = "Test Name";
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
                                                1, 1, 10, 10, 4,
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;
            var testMonsterHealthAfterDamageDealt = returnMonster.GetHealthCurrent() - testAttackDamage;

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(testMonsterHealthAfterDamageDealt, returnMonster.GetHealthCurrent(), "Expected Health after Damage");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Force_CriticalHit_Should_Deal_DamageAmount_To_Monster_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Attribute.Attack = 1;
            var testAttackDamage = testCharacter.GetDamageRollValue();
            testCharacter.Name = "Test Name";
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
                                                1, 1, 10, 10, 4,
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 20;
            var testDamageDealt = returnMonster.GetHealthCurrent() - (testAttackDamage * 2);

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(testDamageDealt, returnMonster.GetHealthCurrent(), "Expected Health after Damage");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Force_Hit_DamageAmount_Should_Give_Experience_To_Character_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Attribute.Attack = 5;
            var testAttackDamage = testCharacter.GetDamageRollValue();
            testCharacter.Name = "Test Name";
            testTurnEngine.MonsterList = new List<Monster>();
            var lowHealthDeadMonster = new Monster("Low Health Dead",
                                                "monster dead low health",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 1,
                                                null, null, null, null,
                                                null, null, null);
            var lowHealthMonster = new Monster("Low Health",
                                                "monster low health",
                                                "", 1, 300, true,
                                                1, 1, 10, 10, 5,
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;
            var testMonster = new Monster(returnMonster);
            testMonster.Name = "Test Monster";
            testMonster.Attribute.CurrentHealth = 5;
            testMonster.Attribute.MaxHealth = 10;
            testMonster.TakeDamage(testAttackDamage);
            var testXPchar = new Character(testCharacter);
            testXPchar.Name = "Test XP Character";
            testXPchar.Description = "Test XP";
            var testXPtoChar = testMonster.CalculateExperienceEarned(testAttackDamage);
            var testExperienceGained = testXPchar.ExperienceTotal + testXPtoChar;

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(testExperienceGained, testCharacter.ExperienceTotal, "Expected XP from Monster");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Monster_Dies_Should_Remove_From_List_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Attribute.Attack = 5;
            var testAttackDamage = testCharacter.GetDamageRollValue();
            testCharacter.Name = "Test Name";
            testTurnEngine.MonsterList = new List<Monster>();
            var lowHealthDeadMonster = new Monster("Low Health Dead",
                                                "monster dead low health",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 1,
                                                null, null, null, null,
                                                null, null, null);
            var lowHealthMonster = new Monster("Low Health",
                                                "monster low health",
                                                "", 1, 300, true,
                                                1, 1, 10, 10, 1,
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
            var testMonsterListCount = testTurnEngine.MonsterList.Count();
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);
            var checkIfContains = testTurnEngine.MonsterList.Contains(returnMonster);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.IsFalse(checkIfContains, "Expected MonsterList to contain returnMonster: false");
            Assert.Greater(testMonsterListCount, testTurnEngine.MonsterList.Count(), "Expected MonsterList Count: 1 less");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Character_TurnAsAttack_Monster_Dies_Should_Drop_Items_ItemPool_Should_Have_Items()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Attribute.Attack = 5;
            var testAttackDamage = testCharacter.GetDamageRollValue();
            testCharacter.Name = "Test Name";
            testTurnEngine.MonsterList = new List<Monster>();
            var lowHealthDeadMonster = new Monster("Low Health Dead",
                                                "monster dead low health",
                                                "", 1, 1, false,
                                                1, 1, 1, 10, 1,
                                                null, null, null, null,
                                                null, null, null);
            var lowHealthMonster = new Monster("Low Health",
                                                "monster low health",
                                                "", 1, 300, true,
                                                1, 1, 10, 10, 1,
                                                null, null, null, null,
                                                null, null, null);

            var highHealthMonster = new Monster("High Health",
                                                "monster high health",
                                                "", 1, 1, true,
                                                1, 1, 1, 10, 10,
                                                null, null, null, null,
                                                null, null, null);
            testTurnEngine.MonsterList.Add(lowHealthDeadMonster);
            var testItemPoolCount = testTurnEngine.ItemPool.Count();
            var testFeetItem = new Item("Anti-Gravity Shoes",
                "These shoes allow the wearer to hover at any given height. When not in use, they revert to their casual form as an ordinary black leather office shoes.",
                "https://vignette.wikia.nocookie.net/finders-keepers-roblox/images/2/2b/Rocket_Boots.png/revision/latest?cb=20181213142618",
                 0, 10, 10, ItemLocationEnum.Feet, AttributeEnum.Speed, true);
            ItemsViewModel.Instance.AddAsync(testFeetItem).GetAwaiter().GetResult();
            lowHealthMonster.Feet = testFeetItem.Guid;
            testTurnEngine.MonsterList.Add(lowHealthMonster);
            testTurnEngine.MonsterList.Add(highHealthMonster);
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testCharacter, testAttackScore, returnMonster, testDefendScore);
            var checkIfContains = testTurnEngine.ItemPool.Contains(testFeetItem);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.IsTrue(checkIfContains, "Expected ItemPool to contain testFeetItem: true");
            Assert.Less(testItemPoolCount, testTurnEngine.ItemPool.Count(), "Expected MonsterList Count: 1 less");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }
        #endregion


        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Set_Attacker_Name_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Assert
            Assert.AreSame(testMonster.Name, testTurnEngine.AttackerName, "Expected Attacker Name: Test Monster");
        }

        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Set_Defender_Name_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Assert
            Assert.AreSame(chosenCharacter.Name, testTurnEngine.TargetName, "Expected Defender Name: High Speed Character");
        }

        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Increment_TurnCount_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();

            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Assert
            Assert.AreEqual(testTurnEngine.BattleScore.TurnCount, 1, "Expected TurnCount: 1");
        }

        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Force_Miss_Should_Set_DamageAmount_Equal_Zero()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;
            highSpeedCharacter.Attribute.Defense = 10;
            highSpeedCharacter.Attribute.MaxHealth = 10;
            highSpeedCharacter.Attribute.CurrentHealth = 2;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 2;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.Zero(testTurnEngine.DamageAmount, "Expected DamageAmount from Miss: 0");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Force_CriticalMiss_Should_Set_DamageAmount_Equal_Zero()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;
            highSpeedCharacter.Attribute.Defense = 10;
            highSpeedCharacter.Attribute.MaxHealth = 10;
            highSpeedCharacter.Attribute.CurrentHealth = 2;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 1;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.Zero(testTurnEngine.DamageAmount, "Expected DamageAmount from Miss: 0");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Force_Hit_Should_Set_DamageAmount_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testMonster.Attribute.Attack = 1;
            var testAttackDamage = testMonster.GetDamageRollValue();
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;
            highSpeedCharacter.Attribute.Defense = 10;
            highSpeedCharacter.Attribute.MaxHealth = 10;
            highSpeedCharacter.Attribute.CurrentHealth = 4;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(testAttackDamage, testTurnEngine.DamageAmount, "Expected DamageAmount from Hit: greater than 0");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Force_Hit_Should_Deal_DamageAmount_To_Character_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testMonster.Attribute.Attack = 1;
            var testAttackDamage = testMonster.GetDamageRollValue();
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;
            highSpeedCharacter.Attribute.Defense = 10;
            highSpeedCharacter.Attribute.MaxHealth = 10;
            highSpeedCharacter.Attribute.CurrentHealth = 4;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();

            var chosenCharacterHealthAfterDamageDealt = chosenCharacter.GetHealthCurrent() - testAttackDamage;

            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(chosenCharacterHealthAfterDamageDealt, chosenCharacter.GetHealthCurrent(), "Expected Health after Damage");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }

        [Test]
        public void TurnEngine_Monster_TurnAsAttack_Force_CriticalHit_Should_Deal_DamageAmount_To_Character_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testMonster = new Monster();
            testMonster.Name = "Test Monster";
            testMonster.Attribute.Attack = 1;
            var testAttackDamage = testMonster.GetDamageRollValue();
            testTurnEngine.CharacterList = new List<Character>();

            var lowSpeedCharacter = new Character();
            lowSpeedCharacter.Name = "Low Speed Character";
            lowSpeedCharacter.Description = "Low Speed Character";
            lowSpeedCharacter.Attribute.Speed = 1;

            var highSpeedCharacter = new Character();
            highSpeedCharacter.Name = "High Speed Character";
            highSpeedCharacter.Description = "High Speed Character should be chosen";
            highSpeedCharacter.Attribute.Speed = 10;
            highSpeedCharacter.Attribute.Defense = 10;
            highSpeedCharacter.Attribute.MaxHealth = 10;
            highSpeedCharacter.Attribute.CurrentHealth = 4;

            var highSpeedDeadCharacter = new Character();
            highSpeedDeadCharacter.Alive = false;
            highSpeedDeadCharacter.Name = "Dead High Speed Character";
            highSpeedDeadCharacter.Description = "Dead High Speed Character";
            highSpeedDeadCharacter.Attribute.Speed = 10;

            testTurnEngine.CharacterList.Add(lowSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedCharacter);
            testTurnEngine.CharacterList.Add(highSpeedDeadCharacter);

            var chosenCharacter = testTurnEngine.AttackChoice(testMonster);
            var testAttackScore = testMonster.Level + testMonster.GetAttack();
            var testDefendScore = chosenCharacter.Level + chosenCharacter.GetDefense();

            var chosenCharacterHealthAfterDamageDealt = chosenCharacter.GetHealthCurrent() - (testAttackDamage * 2);

            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 20;


            // Act
            var returnBool = testTurnEngine.TurnAsAttack(testMonster, testAttackScore, chosenCharacter, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(chosenCharacterHealthAfterDamageDealt, chosenCharacter.GetHealthCurrent(), "Expected Health after Damage");
            Assert.IsTrue(returnBool, "Expected return bool: true");
        }



        //d20 rolled to determine if hit, miss, critical hit, critical miss
        #region Tests: RollToHitTarget(int AttackScore, int DefenseScore)
        [Test]
        public void TurnEngine_RollToHitTarget_Force_CriticalMiss_Should_Set_HitStatus_CriticalMiss()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 1;


            // Act
            var returnHitStatus = testTurnEngine.RollToHitTarget(testAttackScore, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(returnHitStatus, HitStatusEnum.CriticalMiss, "Expected HitStatus: 4, CriticalMiss");
        }

        [Test]
        public void TurnEngine_RollToHitTarget_Force_CriticalHit_Should_Set_HitStatus_CriticalHit()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 20;


            // Act
            var returnHitStatus = testTurnEngine.RollToHitTarget(testAttackScore, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(returnHitStatus, HitStatusEnum.CriticalHit, "Expected HitStatus: 2, CriticalHit");
        }

        [Test]
        public void TurnEngine_RollToHitTarget_Force_Miss_Should_Set_HitStatus_Miss()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
                                                1, 1, 10, 10, 2,
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 2;


            // Act
            var returnHitStatus = testTurnEngine.RollToHitTarget(testAttackScore, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(HitStatusEnum.Miss, returnHitStatus, "Expected HitStatus: 3, Miss");
        }

        [Test]
        public void TurnEngine_RollToHitTarget_Force_Hit_Should_Set_HitStatus_Hit()
        {
            MockForms.Init();

            // Arrange
            var testTurnEngine = new TurnEngine();
            var testCharacter = new Character();
            testCharacter.Name = "Test Name";
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
            var returnMonster = testTurnEngine.AttackChoice(testCharacter);
            var testAttackScore = testCharacter.Level + testCharacter.GetAttack();
            var testDefendScore = returnMonster.Level + returnMonster.GetDefense();
            GameGlobals.ForceRollsToNotRandom = true;
            GameGlobals.ForceToHitValue = 19;


            // Act
            var returnHitStatus = testTurnEngine.RollToHitTarget(testAttackScore, testDefendScore);

            // Reset
            GameGlobals.ToggleRandomState();

            // Assert
            Assert.AreEqual(returnHitStatus, HitStatusEnum.Hit, "Expected HitStatus: 1, Hit");
        }
        #endregion
        
    }
}
