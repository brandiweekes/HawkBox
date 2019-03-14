using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Crawl.Models;
using Crawl.ViewModels;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    public class RoundEngine : TurnEngine
    {
        // Hold the list of players (monster, and character by guid), and order by speed
        public List<PlayerInfo> PlayerList;

        // Player currently engaged
        public PlayerInfo PlayerCurrent;
        //state of the round
        public RoundEnum RoundStateEnum = RoundEnum.Unknown; 
        //constructor
        public RoundEngine()
        {
            //clear item pool & monster list
            ClearLists();
        }

        //clear lists
        private void ClearLists()
        {
            //clear item pool
            ItemPool = new List<Item>();
            //clear monster list
            MonsterList = new List<Monster>();
        }

        // Start the round, need to get the ItemPool, and Characters
        public void StartRound()
        {
            //initialize round count to 0
            BattleScore.RoundCount = 0;
            //call for a new round
            NewRound();
        }

        // Call to make a new set of monsters...
        public void NewRound()
        {
            //end last round
            EndRound();
            //add monsters to new round
            AddMonstersToRound();

            int roll = HelperEngine.RollDice(1, 100);
            if (roll < GameGlobals.TimeWarpChance)
            {
                GameGlobals.setTimeWarp(true);
            }
            else
            {
                GameGlobals.setTimeWarp(false);
            }
            //create player list of monsters and characters
            MakePlayerList();
            //increment round count
            BattleScore.RoundCount++;
        }

        //get average level of characters in character list
        public int GetAverageCharacterLevel()
        {
            //if character list empty return 0
            if(CharacterList.Count < 1)
            {
                return 0;
            }

            //calculate avg & return
            var data = CharacterList.Average(m => m.Level);
            return (int)Math.Floor(data);
        }

        //get minimum level of characters in character list
        public int GetMinCharacterLevel()
        {
            //if character list empty return 0
            if (CharacterList.Count < 1)
            {
                return 0;
            }

            //get minimun & return
            var data = CharacterList.Min(m => m.Level);
            return data;
        }

        //get maximum level of characters in character list
        public int GetMaxCharacterLevel()
        {
            //if character list empty return 0
            if (CharacterList.Count < 1)
            {
                return 0;
            }

            //get maximum level & return
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
                //scale values
                var scalemin = 1;
                var scalemax = 1;
                var scaleavg = 1;

                //get scale values
                if (CharacterList.Any())
                {
                    //get minimum level
                    scalemin = GetMinCharacterLevel();
                    //get max level
                    scalemax = GetMaxCharacterLevel();
                    //get avg level
                    scaleavg = GetAverageCharacterLevel();
                }

                //scale monsters & add to list while monsterlist count is less than 6
                do
                {
                    //get random monster from monster viewmodel
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
            else  //if no monsters in viewmodel
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
            //if no characters remaining
            if(CharacterList.Count < 1)
            {
                //game is over, set round state enum & return
                RoundStateEnum = RoundEnum.GameOver;
                return RoundStateEnum;
            }

            //if no monsters remaining 
            if(MonsterList.Count < 1)
            {
                //call for a new round
                return RoundEnum.NewRound;
            }
            else if (MonsterList.Count < 6)
            {
                // check for monstser multiply chance
                // if true then add monsters
                // else skip
                if (HelperEngine.CanMonsterMultiply())
                {
                    AddMonstersToRound();
                }
            }

            //get player whose turn it is
            PlayerCurrent = GetNextPlayerTurn();
            //if current player is character
            if(PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                //character takes the turn
                var player = CharacterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();
                TakeTurn(player);
            }
            else if(PlayerCurrent.PlayerType == PlayerTypeEnum.Monster) //if current player a monster
            {
                //monster takes the turn
                var player = MonsterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();
                TakeTurn(player);
            }

            //call for next turn
            RoundStateEnum = RoundEnum.NextTurn;
            return RoundStateEnum;
        }

        //get the player next in line for a turn
        public PlayerInfo GetNextPlayerTurn()
        {
            // Recalculate Order
            if (GameGlobals.TimeWarp)
            {
                ReverseOrderPlayerList();
            }
            else
            {
                OrderPlayerListByTurnOrder();
            }
            //call getnextplayer in list
            var PlayerCurrent = GetNextPlayerInList();
            //return current player
            return PlayerCurrent;
        }

        //time warp player list
        public void ReverseOrderPlayerList()
        {
            MakePlayerList();

            PlayerList = PlayerList.OrderBy(a => a.Speed).ThenBy(a => a.Level).ThenBy(a => a.ExperiencePoints)
                .ThenBy(a => a.PlayerType)
                .ThenByDescending(a => a.Name)
                .ThenByDescending(a => a.ListOrder)
                .ToList();
        }

        //order player list according to game rules
        public void OrderPlayerListByTurnOrder()
        {
            var myReturn = new List<PlayerInfo>();
            //create player list
            MakePlayerList();
            //order by: Speed -> Level -> XP -> Character or Monster -> Name -> order in list
            PlayerList = PlayerList.OrderByDescending(a => a.Speed)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();
        }

        //create the player list for turns
        private void MakePlayerList()
        {
            //initialize player list
            PlayerList = new List<PlayerInfo>();
            //a player (character or monster)
            PlayerInfo player;
            //var for player's order in list
            var listOrder = 0;

            //go thru character list & add characters to player list
            foreach(var ch in CharacterList)
            {
                //only add living characters
                if (ch.Alive)
                {
                    //create playerinfo from character
                    player = new PlayerInfo(ch);
                    //set list order
                    player.ListOrder = listOrder;
                    //add to player list
                    PlayerList.Add(player);
                    //increment list order
                    listOrder++;
                }
            }

            //go thru monster list & add monsters to player list
            foreach(var mon in MonsterList)
            {
                //only add living monsters
                if (mon.Alive)
                {
                    //new player from monster
                    player = new PlayerInfo(mon);
                    //set list order
                    player.ListOrder = listOrder;
                    //add to player list
                    PlayerList.Add(player);
                    //increment list order
                    listOrder++;
                }
            }
        }

        //return next player in player list 
        public PlayerInfo GetNextPlayerInList()
        {
            //check if player list is null
            if (PlayerList == null)
            {
                return null;
            }
            
            //if current player not set
            if(PlayerCurrent == null)
            {
                //set current player to last player in list
                PlayerCurrent = PlayerList.LastOrDefault();
            }

            //look for current player in player list
            for(var i = 0; i < PlayerList.Count(); i++)
            {
                //current player found
                if(PlayerList[i].Guid == PlayerCurrent.Guid)
                {
                    //if current player not last in list
                    if(i < PlayerList.Count() - 1)
                    {
                        //return next player
                        return PlayerList[i + 1];
                    }
                    else //if current player last in list
                    {
                        //return first player in list
                        return PlayerList.FirstOrDefault();
                    }
                }
            }

            return null;
        }

        //equip character with items
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

        //equip character with a better item if applicable in a given location
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

            //go thru item pool looking for better item
            foreach(var item in itemList)
            {
                //if the item is better than the current eqipped item, replace item
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
