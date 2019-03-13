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
using Crawl.Services;

namespace UnitTests.GameEngineTests
{
    [TestFixture]
    public class RoundEngineTests
    {
        #region Constructor
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
            MockForms.Init();

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
            MockForms.Init();

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            var Expected = 0;
            //act 
            var Actual = roundEngine.MonsterList.Count;
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
        #endregion Constructor

        #region StartRound
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
        #endregion StartRound

        #region NewRound
        [Test] 
        public void Round_Engine_New_Round_Should_Increment_Round_Count()
        {
            MockForms.Init();

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
            MockForms.Init();

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
            MockForms.Init();

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
            MockForms.Init();

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
        #endregion NewRound

        #region ScaleMethods
        [Test]
        public void Round_Engine_Get_Average_Character_Level_Should_Pass()
        {
            MockForms.Init();

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
            MockForms.Init();

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
            MockForms.Init();

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
            MockForms.Init();

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
            MockForms.Init();

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
            MockForms.Init();

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
        #endregion ScaleMethods

        #region RoundNextTurn
        [Test]
        public void Round_Engine_Round_Next_Turn_No_Characters_Should_Return_Game_Over()
        {
            MockForms.Init();

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
            MockForms.Init();

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
            MockForms.Init();

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
        #endregion RoundNextTurn

        #region GetNextPlayerInList
        [Test]
        public void Round_Engine_Get_Next_Player_In_List_Player_Current_Null_Player_List_Null_Should_Return_Null()
        {
            MockForms.Init();

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.PlayerCurrent = null;
            roundEngine.PlayerList = null;

            //act 
            var Actual = roundEngine.GetNextPlayerInList();
            //reset

            //assert
            Assert.AreEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Next_Player_In_List_Player_Current_Null_Should_Return_First_Player_In_Player_List()
        {
            MockForms.Init();

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.PlayerCurrent = null;
            roundEngine.PlayerList = new List<PlayerInfo>();
            roundEngine.PlayerList.Clear();
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            var Expected = roundEngine.PlayerList[0].Guid;

            //act 
            var Actual = roundEngine.GetNextPlayerInList().Guid;
            
            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Next_Player_In_List_Player_Current_In_Middle_Of_List_Should_Return_Next_Player_In_Player_List()
        {
            MockForms.Init();

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.PlayerList = new List<PlayerInfo>();
            roundEngine.PlayerList.Clear();
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));

            roundEngine.PlayerCurrent = roundEngine.PlayerList[3];
            var Expected = roundEngine.PlayerList[4].Guid;

            //act 
            var Actual = roundEngine.GetNextPlayerInList().Guid;

            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Next_Player_In_List_Player_Current_At_End_Of_List_Should_Return_First_Player_In_Player_List()
        {
            MockForms.Init();

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.PlayerList = new List<PlayerInfo>();
            roundEngine.PlayerList.Clear();
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));

            roundEngine.PlayerCurrent = roundEngine.PlayerList[5];
            var Expected = roundEngine.PlayerList[0].Guid;

            //act 
            var Actual = roundEngine.GetNextPlayerInList().Guid;

            //reset

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Next_Player_In_List_Player_Current_Not_In_List_Should_Return_Null()
        {
            MockForms.Init();

            //arrange 
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.PlayerList = new List<PlayerInfo>();
            roundEngine.PlayerList.Clear();
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Character()));
            roundEngine.PlayerList.Add(new PlayerInfo(new Monster()));

            roundEngine.PlayerCurrent = new PlayerInfo(new Character());

            //act 
            var Actual = roundEngine.GetNextPlayerInList();

            //reset

            //assert
            Assert.AreEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }
        #endregion GetNextPlayerInList

        #region OrderPlayerListByTurnOrder
        [Test]
        public void Round_Engine_Order_Player_List_By_Turn_Order_Order_By_Speed_Should_Pass()
        {
            MockForms.Init();

            //arrange
            RoundEngine roundEngine = new RoundEngine();
            Character char1 = new Character();
            char1.Attribute.Speed = 4;
            Character char2 = new Character();
            char2.Attribute.Speed = 6;
            Character char3 = new Character();
            char3.Attribute.Speed = 2;
            Monster mon1 = new Monster();
            mon1.Attribute.Speed = 3;
            Monster mon2 = new Monster();
            mon2.Attribute.Speed = 1;
            Monster mon3 = new Monster();
            mon3.Attribute.Speed = 5;

            //roundEngine.PlayerList = new List<PlayerInfo>();
            roundEngine.CharacterList.Add(char1);
            roundEngine.CharacterList.Add(char2);
            roundEngine.CharacterList.Add(char3);
            roundEngine.MonsterList.Add(mon1);
            roundEngine.MonsterList.Add(mon2);
            roundEngine.MonsterList.Add(mon3);


            var ex1 = 6;
            var ex2 = 5;
            var ex3 = 4;
            var ex4 = 3;
            var ex5 = 2;
            var ex6 = 1;


            //act
            roundEngine.OrderPlayerListByTurnOrder();
            var actual1 = roundEngine.PlayerList[0].Speed;
            var actual2 = roundEngine.PlayerList[1].Speed;
            var actual3 = roundEngine.PlayerList[2].Speed;
            var actual4 = roundEngine.PlayerList[3].Speed;
            var actual5 = roundEngine.PlayerList[4].Speed;
            var actual6 = roundEngine.PlayerList[5].Speed;

            //reset
            //assert
            Assert.AreEqual(ex1, actual1, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex2, actual2, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex3, actual3, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex4, actual4, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex5, actual5, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex6, actual6, TestContext.CurrentContext.Test.Name);

        }
        #endregion OrderPlayerListByTurnOrder

        #region GetNextPlayerTurn
        [Test]
        public void Round_Engine_Get_Next_Player_Turn_Should_Return_Current_Player()
        {
            MockForms.Init();

            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.PlayerCurrent = null;
            Character char1 = new Character();
            char1.Attribute.Speed = 4;
            Character char2 = new Character();
            char2.Attribute.Speed = 6;
            Character char3 = new Character();
            char3.Attribute.Speed = 2;
            Monster mon1 = new Monster();
            mon1.Attribute.Speed = 3;
            Monster mon2 = new Monster();
            mon2.Attribute.Speed = 1;
            Monster mon3 = new Monster();
            mon3.Attribute.Speed = 5;

            roundEngine.CharacterList.Add(char1);
            roundEngine.CharacterList.Add(char2);
            roundEngine.CharacterList.Add(char3);
            roundEngine.MonsterList.Add(mon1);
            roundEngine.MonsterList.Add(mon2);
            roundEngine.MonsterList.Add(mon3);

            var Expected = char2.Guid;
            //act
            var Actual = roundEngine.GetNextPlayerTurn().Guid;

            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
        #endregion GetNextPlayerTurn

        #region GetItemFromPoolIfBetter
        [Test]
        public void Round_Engine_Get_Item_From_Pool_If_Better_Item_Pool_Empty_Should_Return()
        {
            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            Character char1 = new Character();
            char1.Head = null;
            //act
            roundEngine.GetItemFromPoolIfBetter(char1, ItemLocationEnum.Head);
            var Actual = char1.Head;
            //reset

            //assert
            Assert.AreEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Item_From_Pool_If_Better_Item_Pool_Has_No_Items_In_Location_Should_Return()
        {
            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            Item it1 = new Item();
            it1.Location = ItemLocationEnum.Feet;
            Item it2 = new Item();
            it2.Location = ItemLocationEnum.Necklass;

            roundEngine.ItemPool.Add(it1);
            roundEngine.ItemPool.Add(it2);

            Character char1 = new Character();
            char1.Head = null;
            //act
            roundEngine.GetItemFromPoolIfBetter(char1, ItemLocationEnum.Head);
            var Actual = char1.Head;
            //reset

            //assert
            Assert.AreEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Item_From_Pool_If_Item_Pool_Has_No_Better_Item_Should_Do_Nothing()
        {
            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            Character char1 = new Character();

            Item it = new Item();
            it.Location = ItemLocationEnum.Feet;
            it.Value = 12;

            ItemsViewModel ivm = ItemsViewModel.Instance;
            MockDataStore.Instance.AddAsync_Item(it).GetAwaiter();
            ivm.ForceDataRefresh();
            char1.AddItem(ItemLocationEnum.Feet, it.Id);

            List<Item> ivmList = ivm.Dataset.ToList();

            foreach (var item in ivmList)
            {
                roundEngine.ItemPool.Add(item);
            }
            roundEngine.ItemPool.Remove(it);

            var Expected = it.Guid;
            //act
            roundEngine.GetItemFromPoolIfBetter(char1, ItemLocationEnum.Feet);
            var Actual = char1.GetItemByLocation(ItemLocationEnum.Feet).Guid;
            //reset
            MockDataStore.Instance.DeleteAsync_Item(it).GetAwaiter();
            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Get_Item_From_Pool_If_Current_Location_Has_Better_Item_Should_Add_Item()
        {
            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            Character char1 = new Character();

            Item it = new Item();
            it.Location = ItemLocationEnum.Feet;
            it.Value = 8;

            ItemsViewModel ivm = ItemsViewModel.Instance;
            MockDataStore.Instance.AddAsync_Item(it).GetAwaiter();
            ivm.ForceDataRefresh();
            char1.AddItem(ItemLocationEnum.Feet, it.Id);

            List<Item> ivmList = ivm.Dataset.ToList();

            foreach (var item in ivmList)
            {
                roundEngine.ItemPool.Add(item);
            }
            roundEngine.ItemPool.Remove(it);

            var Expected = roundEngine.ItemPool[0].Guid;
            //act
            roundEngine.GetItemFromPoolIfBetter(char1, ItemLocationEnum.Feet);
            var Actual = char1.GetItemByLocation(ItemLocationEnum.Feet).Guid;
            //reset
            MockDataStore.Instance.DeleteAsync_Item(it).GetAwaiter();
            //assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        #endregion GetItemFromPoolIfBetter

        #region PickupItemsFromPool
        [Test]
        public void Round_Engine_Pickup_Items_From_Pool_Item_Pool_Empty_Should_Return()
        {
            MockForms.Init();

            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            Character ch1 = new Character();
            ch1.Head = null;
            //act 
            roundEngine.PickupItemsFromPool(ch1);
            var Actual = ch1.Head;
            //reset 

            //assert
            Assert.AreEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_Pickup_Items_From_Pool_Has_Items_Should_Add_Items()
        {
            MockForms.Init();

            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            ItemsViewModel ivm = ItemsViewModel.Instance;
            ivm.ForceDataRefresh();

            List<Item> itemlist = ivm.Dataset.ToList();
            foreach (var item in itemlist)
            {
                roundEngine.ItemPool.Add(item);
            }

            Character ch1 = new Character();

            var ex1 = roundEngine.ItemPool[0].Guid;
            var ex2 = roundEngine.ItemPool[1].Guid;
            var ex3 = roundEngine.ItemPool[2].Guid;

            //act 
            roundEngine.PickupItemsFromPool(ch1);
            var actual1 = ch1.GetItemByLocation(ItemLocationEnum.Feet).Guid;
            var actual2 = ch1.GetItemByLocation(ItemLocationEnum.PrimaryHand).Guid;
            var actual3 = ch1.GetItemByLocation(ItemLocationEnum.Necklass).Guid;

            //reset 

            //assert
            Assert.AreEqual(ex1, actual1, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex2, actual2, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex3, actual3, TestContext.CurrentContext.Test.Name);

        }
        #endregion PickupItemsFromPool

        #region EndRound
        [Test]
        public void Round_Engine_End_Round_One_Character_Should_Add_Items()
        {
            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            ItemsViewModel ivm = ItemsViewModel.Instance;
            ivm.ForceDataRefresh();

            List<Item> itemlist = ivm.Dataset.ToList();
            foreach (var item in itemlist)
            {
                roundEngine.ItemPool.Add(item);
            }

            roundEngine.CharacterList.Clear();
            Character ch1 = new Character();
            roundEngine.CharacterList.Add(ch1);

            var ex1 = roundEngine.ItemPool[0].Guid;
            var ex2 = roundEngine.ItemPool[1].Guid;
            var ex3 = roundEngine.ItemPool[2].Guid;

            //act
            roundEngine.EndRound();
            var actual1 = ch1.GetItemByLocation(ItemLocationEnum.Feet).Guid;
            var actual2 = ch1.GetItemByLocation(ItemLocationEnum.PrimaryHand).Guid;
            var actual3 = ch1.GetItemByLocation(ItemLocationEnum.Necklass).Guid;
            //reset

            //assert
            Assert.AreEqual(ex1, actual1, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex2, actual2, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex3, actual3, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_End_Round_Two_Characters_Should_Add_Items()
        {
            //arrange
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();

            Item it1 = new Item();
            it1.Location = ItemLocationEnum.Feet;
            it1.Value = 7;

            Item it2 = new Item();
            it2.Location = ItemLocationEnum.PrimaryHand;
            it2.Value = 7;

            Item it3 = new Item();
            it3.Location = ItemLocationEnum.Necklass;
            it3.Value = 7;

            ItemsViewModel ivm = ItemsViewModel.Instance;
            MockDataStore.Instance.AddAsync_Item(it1).GetAwaiter();
            MockDataStore.Instance.AddAsync_Item(it2).GetAwaiter();
            MockDataStore.Instance.AddAsync_Item(it3).GetAwaiter();
            ivm.ForceDataRefresh();


            List<Item> itemlist = ivm.Dataset.ToList();
            foreach (var item in itemlist)
            {
                roundEngine.ItemPool.Add(item);
            }

            roundEngine.CharacterList.Clear();
            Character ch1 = new Character();
            ch1.AddItem(ItemLocationEnum.Feet, it1.Id);
            ch1.AddItem(ItemLocationEnum.PrimaryHand, it2.Id);
            ch1.AddItem(ItemLocationEnum.Necklass, it3.Id);

            roundEngine.ItemPool.Remove(it1);
            roundEngine.ItemPool.Remove(it2);
            roundEngine.ItemPool.Remove(it3);

            Character ch2 = new Character();
            roundEngine.CharacterList.Add(ch1);
            roundEngine.CharacterList.Add(ch2);

            var ex1 = roundEngine.ItemPool[0].Guid;
            var ex2 = roundEngine.ItemPool[1].Guid;
            var ex3 = roundEngine.ItemPool[2].Guid;
            var ex4 = it1.Guid;
            var ex5 = it2.Guid;
            var ex6 = it3.Guid;
            //act
            roundEngine.EndRound();
            var actual1 = ch1.GetItemByLocation(ItemLocationEnum.Feet).Guid;
            var actual2 = ch1.GetItemByLocation(ItemLocationEnum.PrimaryHand).Guid;
            var actual3 = ch1.GetItemByLocation(ItemLocationEnum.Necklass).Guid;
            var actual4 = ch2.GetItemByLocation(ItemLocationEnum.Feet).Guid;
            var actual5 = ch2.GetItemByLocation(ItemLocationEnum.PrimaryHand).Guid;
            var actual6 = ch2.GetItemByLocation(ItemLocationEnum.Necklass).Guid;
            //reset
            MockDataStore.Instance.DeleteAsync_Item(it1).GetAwaiter();
            MockDataStore.Instance.DeleteAsync_Item(it2).GetAwaiter();
            MockDataStore.Instance.DeleteAsync_Item(it3).GetAwaiter();

            //assert
            Assert.AreEqual(ex1, actual1, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex2, actual2, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex3, actual3, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex4, actual4, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex5, actual5, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex6, actual6, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Round_Engine_End_Round_Should_Clear_Lists()
        {
            //assert
            RoundEngine roundEngine = new RoundEngine();
            roundEngine.ItemPool.Clear();
            roundEngine.ItemPool.Add(new Item());
            roundEngine.ItemPool.Add(new Item());
            roundEngine.MonsterList.Clear();
            roundEngine.MonsterList.Add(new Monster());
            roundEngine.MonsterList.Add(new Monster());

            var ex1 = 0;
            var ex2 = 0;

            //act
            roundEngine.EndRound();
            var ac1 = roundEngine.ItemPool.Count();
            var ac2 = roundEngine.MonsterList.Count();

            //assert
            Assert.AreEqual(ex1, ac1, TestContext.CurrentContext.Test.Name);
            Assert.AreEqual(ex2, ac2, TestContext.CurrentContext.Test.Name);
        }
        #endregion EndRound
    }
}
