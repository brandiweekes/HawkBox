using Crawl.Models;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ModelTests
{
    [TestFixture]
    public class MonsterTests
    {
        #region Tests: ScaleLevel

        [Test]
        public void Monster_ScaleLevel_NegLevel_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();

            // act
            var result = monster.ScaleLevel(-5);

            // assert
            Assert.IsFalse(result, TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public void Monster_ScaleLevel_Less_Than_Current_Level_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();
            monster.Level = 3;

            // act
            var result = monster.ScaleLevel(monster.Level -2);

            // assert
            Assert.IsFalse(result, TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public void Monster_ScaleLevel_Greater_Than_MaxLevel_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();

            // act
            var result = monster.ScaleLevel(LevelTable.MaxLevel + 1);

            // assert
            Assert.IsFalse(result, TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public void Monster_ScaleLevel_Level_10_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();

            // act
            var result = monster.ScaleLevel(10);

            // assert
            Assert.IsTrue(result, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(monster.Attribute.Attack, LevelTable.Instance.LevelDetailsList[10].Attack, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(monster.Attribute.Defense, LevelTable.Instance.LevelDetailsList[10].Defense, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(monster.Attribute.Speed, LevelTable.Instance.LevelDetailsList[10].Speed, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(monster.Level, LevelTable.Instance.LevelDetailsList[10].Level, TestContext.CurrentContext.Test.Name);
        }

        #endregion Tests: ScaleLevel

        #region Tests: TakeDamage

        [Test]
        public void Monster_TakeDamage_NegLevel_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();
            var expected = monster.Attribute.CurrentHealth;

            // act
            monster.TakeDamage(-10);
            var actual = monster.Attribute.CurrentHealth;

            // assert
            Assert.AreEqual(expected, actual, TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public void Monster_TakeDamage_10_Cause_Death_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();
            var expected = monster.Attribute.CurrentHealth;

            // act
            monster.TakeDamage(expected+4);
            var actual = monster.Alive;

            // assert
            Assert.IsFalse(actual, TestContext.CurrentContext.Test.Name);

        }

        #endregion Tests: TakeDamage

        #region Tests: CalculateExperienceEarned

        [Test]
        public void Monster_CalculateExperienceEarned_NegDamage_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();
            var expected = 0;
            // act
            var actual = monster.CalculateExperienceEarned(-4);

            // assert
            Assert.AreEqual(expected, actual, TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public void Monster_CalculateExperienceEarned_Should_Pass()
        {
            // arrange
            Monster monster = new Monster();
            var expected = monster.ExperienceTotal;
            // act
            var actual = monster.CalculateExperienceEarned(monster.Attribute.CurrentHealth + 5);

            // assert
            Assert.AreEqual(expected, actual, TestContext.CurrentContext.Test.Name);

        }

        #endregion Tests: CalculateExperienceEarned
    }
}
