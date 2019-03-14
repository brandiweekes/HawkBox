using NUnit.Framework;
using Crawl.GameEngine;
using Xamarin.Forms.Mocks;
using Crawl.ViewModels;
using Crawl.Models;
using System.Linq;
using System.Diagnostics;

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
        public void GetListOfCharacter_No_Characters_In_Datastore_Should_Fail()
        {
            // Initialize Mock
            MockForms.Init();

            // Arrange
            AutoBattleEngine auto = new AutoBattleEngine();
            var _instance = CharactersViewModel.Instance;
            _instance.LoadCharactersCommand.Execute(null);
            var myData = _instance.Dataset.ToList();
            foreach(Character c in myData)
            {
                _instance.DeleteAsync(c).GetAwaiter();
            }
            
            // Act
            var Actual = auto.GetListOfCharacter(2);

            // Reset
            foreach (Character c in myData)
            {
                _instance.AddAsync(c).GetAwaiter();
            }

            // Assert
            Assert.Null(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RunAutoBattle_No_Characters_Should_Fail()
        {
            // Initialize Mock
            MockForms.Init();

            // Arrange
            AutoBattleEngine auto = new AutoBattleEngine();
            GameGlobals.MaxNumberPartyPlayers = 0;

            // Act
            auto.RunAutoBattle();
            var Actual = auto.BattleEngine.BattleRunningState();

            // Reset
            GameGlobals.ResetMaxNumberPartyPlayers();

            // Assert
            Assert.IsFalse(Actual, TestContext.CurrentContext.Test.Name);
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
        public void BattleEngine_FormatOutput_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            AutoBattleEngine auto = new AutoBattleEngine();
            
            // Act
            var Actual = auto.FormatOutput();
            
            // Assert
            Assert.IsNotNull(Actual, TestContext.CurrentContext.Test.Name);
        }

        #region Hackathon Tests

        // testing Hackathin #1
        // Forced Random Value & Forced Random Hit
        [Test]
        public void HelperEngine_RollDice_Forced_Random_Value_100_Rolls_1_Dice_20_Should_Pass()
        {
            // Arrange
            GameGlobals.EnableRandomValues();
            var _oldData = GameGlobals.ForcedRandomValue;
            GameGlobals.SetForcedRandomNumbersValue(100);
            var dice = 20;
            var rolls = 1;
            var Expected = 100;

            // Act
            var Actual = HelperEngine.RollDice(rolls, dice);

            // Reset
            GameGlobals.ForcedRandomValue = _oldData;
            GameGlobals.DisableRandomValues();

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        // Hackathon #12
        // monsters steal items when dead.
        [Test]
        public void Character_DropAllItems_Monster_Steal_True_Should_Pass()
        {
            MockForms.Init();

            // Arrange
            GameGlobals.EnableMonstersToStealItems = true;
            GameGlobals.SetPercentageChanceToStealItems(100);
            Character character = new Character();
            var _instance = ItemsViewModel.Instance;
            _instance.ForceDataRefresh();
            if(_instance.Dataset.Count > 0)
            {
                var _itemId = _instance.ChooseRandomItemString(ItemLocationEnum.PrimaryHand, AttributeEnum.Attack);
                character.AddItem(ItemLocationEnum.PrimaryHand, _itemId);
            }

            var Expected = 0;

            // Act
            var _items = character.DropAllItems();
            var Actual = _items.Count;

            // Reset
            GameGlobals.EnableMonstersToStealItems = false;
            GameGlobals.SetPercentageChanceToStealItems(0);

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        // Hackathon #12
        // Testing helper engine code
        [Test]
        public void HelperEngine_DoesMonsterHaveChanceToSteal_Enable_100_Precent_Chance_Should_Pass()
        {
            // Arrange
            GameGlobals.EnableMonstersToStealItems = true;
            GameGlobals.SetPercentageChanceToStealItems(100);
            
            var Expected = true;

            // Act
            var Actual = HelperEngine.DoesMonsterHaveChanceToSteal();

            // Reset
            GameGlobals.EnableMonstersToStealItems = false;
            GameGlobals.SetPercentageChanceToStealItems(0);

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        // Hackathon #12
        // Testing helper engine code
        [Test]
        public void HelperEngine_DoesMonsterHaveChanceToSteal_Enable_50_Precent_Chance_Should_Pass()
        {
            // Arrange
            GameGlobals.EnableMonstersToStealItems = true;
            GameGlobals.SetPercentageChanceToStealItems(50);
            var _olddata = GameGlobals.ForcedRandomValue;
            GameGlobals.ForcedRandomValue = 10;
            GameGlobals.ForceRollsToNotRandom = true;

            var Expected = true;

            // Act
            var Actual = HelperEngine.DoesMonsterHaveChanceToSteal();

            // Reset
            GameGlobals.EnableMonstersToStealItems = false;
            GameGlobals.SetPercentageChanceToStealItems(0);
            GameGlobals.ForcedRandomValue = _olddata;
            GameGlobals.ForceRollsToNotRandom = false;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        // Hackathon #22
        // Testing helper engine code
        [Test]
        public void HelperEngine_CanMonsterMultiply_Enable_100_Precent_Chance_Should_Pass()
        {
            // Arrange
            GameGlobals.EnableMonstersToMultiply = true;
            GameGlobals.SetPercentageChanceToMultiply(100);

            var Expected = true;

            // Act
            var Actual = HelperEngine.CanMonsterMultiply();

            // Reset
            GameGlobals.EnableMonstersToMultiply = false;
            GameGlobals.SetPercentageChanceToMultiply(0);

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        // Hackathon #22
        // Testing helper engine code
        [Test]
        public void HelperEngine_CanMonsterMultiply_Enable_50_Precent_Chance_Should_Pass()
        {
            // Arrange
            GameGlobals.EnableMonstersToMultiply = true;
            GameGlobals.SetPercentageChanceToMultiply(50);
            var _olddata = GameGlobals.ForcedRandomValue;
            GameGlobals.ForcedRandomValue = 10;
            GameGlobals.ForceRollsToNotRandom = true;

            var Expected = true;

            // Act
            var Actual = HelperEngine.CanMonsterMultiply();

            // Reset
            GameGlobals.EnableMonstersToMultiply = false;
            GameGlobals.SetPercentageChanceToMultiply(0);
            GameGlobals.ForcedRandomValue = _olddata;
            GameGlobals.ForceRollsToNotRandom = false;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion Hackathon Tests
    }
}
