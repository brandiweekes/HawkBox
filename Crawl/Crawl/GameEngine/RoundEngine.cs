using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Crawl.Models;
using Crawl.ViewModels;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    class RoundEngine : TurnEngine
    {
        // Hold the list of players (monster, and character by guid), and order by speed
        public List<PlayerInfo> PlayerList;

        // Player currently engaged
        public PlayerInfo PlayerCurrent;

        public RoundEnum RoundStateEnum = RoundEnum.Unknown; 

        public RoundEngine()
        {
            ClearLists();
        }

        private void ClearLists()
        {
            ItemPool = new List<Item>();
            MonsterList = new List<Monster>();
        }

        // Start the round, need to get the ItemPool, and Characters
        public void StartRound()
        {
            BattleScore.RoundCount = 0;
            NewRound();
        }

        // Call to make a new set of monsters...
        public void NewRound()
        {
            EndRound();
            AddMonstersToRound();
            MakePlayerList();
            BattleScore.RoundCount++;
        }

        public int GetAverageCharacterLevel()
        {
            var data = CharacterList.Average(m => m.Level);
            return (int)Math.Floor(data);
        }

        public int GetMinCharacterLevel()
        {
            var data = CharacterList.Min(m => m.Level);
            return data;
        }

        public int GetMaxCharacterLevel()
        {
            var data = CharacterList.Max(m => m.Level);
            return data;
        }


        // Add Monsters
        // Scale them to meet Character Strength...
        private void AddMonstersToRound()
        {
            //if monsters already there
            if(MonsterList.Count >= 6)
            {
                return;
            }

            //get current monster in viewmodel
            var monsterModel = MonstersViewModel.Instance;
            monsterModel.ForceDataRefresh();

            //if already monsters in db
            if(monsterModel.Dataset.Count() > 0)
            {
                var scalemin = 1;
                var scalemax = 1;
                var scaleavg = 1;

                //get scale values
                if (CharacterList.Any())
                {
                    scalemin = GetMinCharacterLevel();
                    scalemax = GetMaxCharacterLevel();
                    scaleavg = GetAverageCharacterLevel();
                }

                //scale monsters & add to list
                do
                {
                    var round = HelperEngine.RollDice(1, monsterModel.Dataset.Count());
                    {
                        //new monster
                        var monster = new Monster(monsterModel.Dataset[round - 1]);
                        //set monsters name
                        monster.Name += " " + (1 + MonsterList.Count()).ToString();
                        //scale round and add to list
                        var roundscale = HelperEngine.RollDice(1, scaleavg + 1);
                        monster.ScaleLevel(roundscale);
                        MonsterList.Add(monster);
                    }
                } while (MonsterList.Count < 6);

            }
            else
            {
                //add 6 monsters
                for(var i = 0; i < 6; i++)
                {
                    //new monster
                    var monster = new Monster();
                    //set monster name
                    monster.Name += " " + MonsterList.Count + 1;
                    //add to list
                    MonsterList.Add(monster);
                }
            }
        }

        // At the end of the round
        // Clear the Item List
        // Clear the Monster List
        public void EndRound()
        {
            //have each character pick up items
            foreach(var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }

            //clear lists
            ClearLists();
        }

        // Get Round Turn Order

        // Rember Who's Turn

        // RoundNextTurn
        public RoundEnum RoundNextTurn()
        {
            if(CharacterList.Count < 1)
            {
                RoundStateEnum = RoundEnum.GameOver;
                return RoundStateEnum
            }

            if(MonsterList.Count < 1)
            {
                return RoundEnum.NewRound;
            }

            PlayerCurrent = GetNextPlayerTurn();

            if(PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                var player = CharacterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();
                TakeTurn(player);
            }
            else if(PlayerCurrent.PlayerType == PlayerTypeEnum.Monster)
            {
                var player = MonsterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();
                TakeTurn(player);
            }

            RoundStateEnum = RoundEnum.NextTurn;
            return RoundStateEnum;
        }

        public PlayerInfo GetNextPlayerTurn()
        {
            // Recalculate Order
            OrderPlayerListByTurnOrder();

            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        public void OrderPlayerListByTurnOrder()
        {
            var myReturn = new List<PlayerInfo>();

            MakePlayerList();

            PlayerList = PlayerList.OrderByDescending(a => a.Speed)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();
        }

        private void MakePlayerList()
        {
            PlayerList = new List<PlayerInfo>();
            PlayerInfo player;

            var listOrder = 0;

            foreach(var ch in CharacterList)
            {
                if (ch.Alive)
                {
                    player = new PlayerInfo(ch);
                    player.ListOrder = listOrder;
                    PlayerList.Add(player);
                    listOrder++;
                }
            }

            foreach(var mon in MonsterList)
            {
                if (mon.Alive)
                {
                    player = new PlayerInfo(mon);
                    player.ListOrder = listOrder;
                    PlayerList.Add(player);
                    listOrder++;
                }
            }


        }

        public PlayerInfo GetNextPlayerInList()
        {
            if(PlayerCurrent == null)
            {
                PlayerCurrent = PlayerList.LastOrDefault();
            }

            for(var i = 0; i < PlayerList.Count(); i++)
            {
                if(PlayerList[i].Guid == PlayerCurrent.Guid)
                {
                    if(i < PlayerList.Count() - 1)
                    {
                        return PlayerList[i + 1];
                    }
                    else
                    {
                        return PlayerList.FirstOrDefault();
                    }
                }
            }


            return null;
        }

        public void PickupItemsFromPool(Character character)
        {
            //if no items
            if(ItemPool.Count < 1)
            {
                return;
            }

            //get items for each location
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
        }

        public void GetItemFromPoolIfBetter(Character character, ItemLocationEnum setLocation)
        {
            //get items with location = setlocation
            var itemList = ItemPool.Where(a => a.Location == setLocation).OrderByDescending(a => a.Value).ToList();

            //if no items found
            if(itemList.Count < 1)
            {
                return;
            }

            //get item currently in setlocation
            var currentItem = character.GetItemByLocation(setLocation);
            //if no item currently in location
            if(currentItem == null)
            {
                //add item to character
                character.AddItem(setLocation, itemList.FirstOrDefault().Id);
                return;
            }

            foreach(var item in itemList)
            {
                if(item.Value > currentItem.Value)
                {
                    //add item to character
                    var dropped = character.AddItem(setLocation, item.Id);
                    //remove item from pool
                    ItemPool.Remove(item);
                    //add dropped item back into pool
                    if(dropped != null)
                    {
                        ItemPool.Add(dropped);
                    }
                }
            }
            
        }
    }
}
