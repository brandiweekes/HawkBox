using Crawl.Models;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Mocks;

namespace UnitTests.ModelTests
{
    [TestFixture]
    public class CharacterTests
    {
        #region Tests: Constructors

        [Test]
        public void Character_Default_Constructor_Should_Pass()
        {
            // arrange-act
            Character character = new Character();

            // assert
            Assert.NotNull(character.Guid, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Id, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Name, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Description, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.ImageURI, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Level, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.Attack, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.Defense, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.Speed, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.CurrentHealth, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.MaxHealth, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_Parameterized_Constructor_Should_Pass()
        {
            // arrange
            string _testName = "Test Name", _testDesc = "Test Desc", _testURL = "Test URL";

            // act
            Character character = new Character(_testName, _testDesc, _testURL);

            // assert
            Assert.NotNull(character.Guid, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Id, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Name, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Description, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.ImageURI, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Level, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.Attack, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.Defense, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.Speed, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.CurrentHealth, TestContext.CurrentContext.Test.Name);
            Assert.NotNull(character.Attribute.MaxHealth, TestContext.CurrentContext.Test.Name);

            // assert
            Assert.AreEqual(character.Name, _testName, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(character.Description, _testDesc, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(character.ImageURI, _testURL, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_Parameterized_Constructor_Level_10_Should_Pass()
        {
            // arrange
            string _testName = "Test Name", _testDesc = "Test Desc", _testURL = "Test URL";
            int _level = 10;
            var Expected = LevelTable.Instance.LevelDetailsList[_level];

            // act
            Character character = new Character(_testName, _testDesc, _testURL, _level);

            // assert
            Assert.AreEqual(Expected.Attack, character.Attribute.Attack, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(Expected.Defense, character.Attribute.Defense, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(Expected.Speed, character.Attribute.Speed, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(Expected.Level, character.Level, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_Clone_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var newCharacter = new Character(character);

            // assert
            Assert.AreNotEqual(character.Id, newCharacter.Id, TestContext.CurrentContext.Test.Name);
            Assert.AreNotEqual(character.Guid, newCharacter.Guid, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(character.Name, newCharacter.Name, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(character.Level, newCharacter.Level, TestContext.CurrentContext.Test.Name);
        }

        #endregion Tests: Constructors

        #region Tests: Update

        [Test]
        public void Character_Update_Should_Pass()
        {
            // arrange
            Character character = new Character();

            string _testName = "Test Name", _testDesc = "Test Desc", _testURL = "Test URL";
            int _level = 10;
            Character newCharacter = new Character(_testName, _testDesc, _testURL, _level);

            //Act
            character.Update(newCharacter);

            // assert
            Assert.AreEqual(_testName, character.Name, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(_level, character.Level, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_Update_No_Character_Should_Pass()
        {
            // arrange
            Character character = new Character();

            string _testName = "Test Name", _testDesc = "Test Desc", _testURL = "Test URL";
            int _level = 10;
            Character newCharacter = new Character(_testName, _testDesc, _testURL, _level);

            //Act
            character.Update(null);

            // assert
            Assert.AreNotEqual(_testName, character.Name, TestContext.CurrentContext.Test.Name);
            Assert.AreNotEqual(_level, character.Level, TestContext.CurrentContext.Test.Name);
        }

        #endregion Tests: Update

        [Test]
        public void Character_FormatOutput_Should_Pass()
        {
            MockForms.Init();

            // arrange
            Character character = new Character();

            //Act
            var Actual = character.FormatOutput();

            // assert
            Assert.NotNull(Actual, TestContext.CurrentContext.Test.Name);
        }

        #region Test: ScaleLevel

        [Test]
        public void Character_ScaleLevel_NegLevel_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var Actual = character.ScaleLevel(-1);

            // assert
            Assert.IsFalse(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_ScaleLevel_Level_Less_Than_Current_Level_Should_Pass()
        {
            // arrange
            string _testName = "Test Name", _testDesc = "Test Desc", _testURL = "Test URL";
            int _level = 10;
            Character character = new Character(_testName, _testDesc, _testURL, _level);

            //Act
            var Actual = character.ScaleLevel(1);

            // assert
            Assert.IsFalse(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_ScaleLevel_Level_Greater_Than_MaxLevel_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var Actual = character.ScaleLevel(LevelTable.MaxLevel + 1);

            // assert
            Assert.IsFalse(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_ScaleLevel_Level_10_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var Actual = character.ScaleLevel(10);

            // assert
            Assert.IsTrue(Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion Test: ScaleLevel

        #region Tests: LevelUp

        [Test]
        public void Character_LevelUp_NegExperience_Should_Pass()
        {
            // arrange
            Character character = new Character();
            character.ExperienceTotal = -10000;

            //Act
            var Actual = character.LevelUp();

            // assert
            Assert.IsFalse(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_LevelUp_Should_Pass()
        {
            // arrange
            Character character = new Character();
            character.ExperienceTotal = 900;
            var _oldLevel = character.Level;

            //Act
            character.ScaleLevel(4);
            var Actual = character.LevelUp();
            var _newLevel = character.Level;

            // assert
            Assert.IsTrue(Actual, TestContext.CurrentContext.Test.Name);
            Assert.AreNotEqual(_oldLevel, _newLevel, TestContext.CurrentContext.Test.Name);
        }

        #endregion Tests: LevelUp

        #region Tests: LevelUpToValue

        [Test]
        public void Character_LevelUpToValue_NegLevel_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var Actual = character.LevelUpToValue(-1);

            // assert
            Assert.AreEqual(character.Level, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_LevelUpToValue_Same_As_Current_Level_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var Actual = character.LevelUpToValue(character.Level);

            // assert
            Assert.AreEqual(character.Level, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_LevelUpToValue_GreaterThan_MaxLevel_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var Actual = character.LevelUpToValue(LevelTable.MaxLevel + 1);

            // assert
            Assert.AreEqual(character.Level, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_LevelUpToValue_Should_Pass()
        {
            // arrange
            var _testLevel = 5;
            Character character = new Character("Test name", "Test desc", "Test Url", _testLevel);
            var Expected = 6;

            //Act
            var Actual = character.LevelUpToValue(6);

            // assert
            Assert.AreNotEqual(_testLevel, Actual, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion Tests: LevelUpToValue

        #region Tests: AddExperience

        [Test]
        public void Character_AddExperience_NegValue_Should_Pass()
        {
            // arrange
            Character character = new Character();

            //Act
            var Actual = character.AddExperience(-1);

            // assert
            Assert.IsFalse(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_AddExperience_MaxLevel_Should_Pass()
        {
            // arrange
            Character character = new Character();
            character.Level = LevelTable.MaxLevel;

            //Act
            var Actual = character.AddExperience(100);

            // assert
            Assert.IsFalse(Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion Tests: AddExperience
    }
}
