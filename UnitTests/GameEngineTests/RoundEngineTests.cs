using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Mocks;

using Crawl.Models;
using Crawl.ViewModels;
using Crawl.GameEngine;


namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class RoundEngineTests
    {

        //steps:

        //arrange 

        //act 

        //reset

        //assert


        [Test]
        public void RoundEngine_Constructor_Should_Pass()
        {
            MockForms.Init();
            //arrange 
            //act 
            RoundEngine roundEngine = new RoundEngine();

            //reset

            //assert
            Assert.AreNotEqual(null, roundEngine, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Constructor_Item_Pool_Count_Should_Equal_Zero()
        {

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 0;
            //act 
            var Actual = roundEngine.ItemPool.Count;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Constructor_Monster_List_Count_Should_Equal_Zero()
        {

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 0;
            //act 
            var Actual = roundEngine.MonsterList.Count;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Start_Round_BattleScore_Round_Count_Should_Equal_One()
        {
            MockForms.Init();

            //arrange
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 1;
            //act 
            roundEngine.StartRound();
            var Actual = roundEngine.BattleScore.RoundCount;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test] 
        public void Round_Engine_New_Round_Should_Increment_Round_Count()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 1;
            //act 
            roundEngine.NewRound();
            var Actual = roundEngine.BattleScore.RoundCount;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_New_Round_Monster_List_Count_Should_Equal_Six()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 6;
            //act 
            roundEngine.NewRound();
            var Actual = roundEngine.MonsterList.Count;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_New_Round_Make_Player_List_Should_Pass()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 6 + roundEngine.CharacterList.Count;
            //act 
            roundEngine.NewRound();
            var Actual = roundEngine.PlayerList.Count;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_New_Round_End_Round_Should_Pass()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 0;
            //act 
            roundEngine.NewRound();
            var Actual = roundEngine.ItemPool.Count;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Average_Character_Level_Should_Pass()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();
            Character char1 = new Character();
            char1.Level = 5;
            Character char2 = new Character();
            char2.Level = 3;
            Character char3 = new Character();
            char3.Level = 2;
            roundEngine.CharacterList.Add(char1);
            roundEngine.CharacterList.Add(char2);
            roundEngine.CharacterList.Add(char3);
            var Expected = 3;
            //act 
            var Actual = roundEngine.GetAverageCharacterLevel();
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Average_Character_Empty_Character_List_Should_Return_Zero()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();
           
            var Expected = 0;
            //act 
            var Actual = roundEngine.GetAverageCharacterLevel();
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Min_Character_Level_Should_Pass()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();
            Character char1 = new Character();
            char1.Level = 5;
            Character char2 = new Character();
            char2.Level = 3;
            Character char3 = new Character();
            char3.Level = 2;
            roundEngine.CharacterList.Add(char1);
            roundEngine.CharacterList.Add(char2);
            roundEngine.CharacterList.Add(char3);
            var Expected = 2;
            //act 
            var Actual = roundEngine.GetMinCharacterLevel();
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Min_Character_Empty_Character_List_Should_Return_Zero()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();

            var Expected = 0;
            //act 
            var Actual = roundEngine.GetMinCharacterLevel();
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Max_Character_Level_Should_Pass()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();
            Character char1 = new Character();
            char1.Level = 5;
            Character char2 = new Character();
            char2.Level = 3;
            Character char3 = new Character();
            char3.Level = 2;
            roundEngine.CharacterList.Add(char1);
            roundEngine.CharacterList.Add(char2);
            roundEngine.CharacterList.Add(char3);
            var Expected = 5;
            //act 
            var Actual = roundEngine.GetMaxCharacterLevel();
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Max_Character_Empty_Character_List_Should_Return_Zero()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();

            var Expected = 0;
            //act 
            var Actual = roundEngine.GetMaxCharacterLevel();
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Round_Next_Turn_No_Characters_Should_Return_Game_Over()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();
            var Expected = RoundEnum.GameOver;
            //act 
            var Actual = roundEngine.RoundNextTurn();

            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Round_Next_Turn_No_Monsters_Should_Return_New_Round()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Add(new Character());
            roundEngine.MonsterList.Clear();
            var Expected = RoundEnum.NewRound;
            //act 
            var Actual = roundEngine.RoundNextTurn();

            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Round_Next_Turn_Two_Characters_Two_Monsters_Should_Return_Next_Turn()
        {
            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.CharacterList.Clear();
            roundEngine.CharacterList.Add(new Character());
            roundEngine.CharacterList.Add(new Character());
            roundEngine.MonsterList.Clear();
            roundEngine.MonsterList.Add(new Monster());
            roundEngine.MonsterList.Add(new Monster());
            var Expected = RoundEnum.NextTurn;
            //act 
            var Actual = roundEngine.RoundNextTurn();

            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
    }
}
