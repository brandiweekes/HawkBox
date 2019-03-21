using Crawl.GameEngine;
using Crawl.Models;
using Crawl.ViewModels;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Mocks;

namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class HackathonTests
    {
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
            if (_instance.Dataset.Count > 0)
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

        // Hackathon #25
        // Testing helper engine code
        [Test]
        public void HelperEngine_ReboundAttack_Enable_100_Precent_Chance_Should_Pass()
        {
            // Arrange
            GameGlobals.EnableReboundAttack = true;
            GameGlobals.SetPercentageChanceToRebound(100);
            var _olddata = GameGlobals.ForcedRandomValue;

            var Expected = true;

            // Act
            var Actual = HelperEngine.ReboundAttack();

            // Reset
            GameGlobals.EnableReboundAttack = false;
            GameGlobals.SetPercentageChanceToRebound(0);

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        // Hackathon #25
        // Testing helper engine code
        [Test]
        public void HelperEngine_ReboundAttack_Enable_50_Precent_Chance_Should_Pass()
        {
            // Arrange
            GameGlobals.EnableReboundAttack = true;
            GameGlobals.SetPercentageChanceToRebound(50);
            var _olddata = GameGlobals.ForcedRandomValue;
            GameGlobals.ForcedRandomValue = 10;
            GameGlobals.ForceRollsToNotRandom = true;

            var Expected = true;

            // Act
            var Actual = HelperEngine.ReboundAttack();

            // Reset
            GameGlobals.EnableReboundAttack = false;
            GameGlobals.SetPercentageChanceToRebound(0);
            GameGlobals.ForcedRandomValue = _olddata;
            GameGlobals.ForceRollsToNotRandom = false;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion Hackathon Tests
    }
}
