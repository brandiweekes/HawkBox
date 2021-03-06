﻿using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;

namespace Crawl.Models
{
    public class Score : Entity<Score>
    {
        // This battle number, incremental int from the last int in the database
        public int BattleNumber { get; set; }

        // Total Score
        public int ScoreTotal { get; set; }

        // The Date the game played, and when the score was saved
        public DateTime GameDate { get; set; }

        // Tracks if auto battle is true, or if user battle = false
        public bool AutoBattle { get; set; }

        // The number of turns the battle took to finish
        public int TurnCount { get; set; }

        // The number of rounds the battle took to finish
        public int RoundCount { get; set; }

        // The count of monsters slain during battle
        public int MonsterSlainNumber { get; set; }

        // The total experience points all the characters received during the battle
        public int ExperienceGainedTotal { get; set; }

        // A list of all the characters at the time of death and their stats.  
        // Only use Get only, set will be done by the Add feature.
        private string CharacterAtDeathString { get; set; }

        // List used to display in UI as ListView. Ignored as Column in DB.
        [Ignore]
        public List<Character> CharacterAtDeathList { get; set; }

        // All of the monsters killed and their stats. 
        // Only use Get only, set will be done by the Add feature.
        private string MonstersKilledString { get; set; }

        // List used to display in UI as ListView. Ignored as Column in DB.
        [Ignore]
        public List<Monster> MonstersKilledList { get; set; }

        // All of the items dropped and their stats. 
        // Only use Get only, set will be done by the Add feature.
        private string ItemsDroppedString { get; set; }

        // List used to display in UI as ListView. Ignored as Column in DB.
        [Ignore]
        public List<Item> ItemsDroppedList { get; set; }

        /// <summary>
        /// Instantiate new Score
        /// </summary>
        public Score()
        {
            // Implement
            Name = "Score Name";
            Description = "This is Score Description";

            ScoreTotal = 0;
            TurnCount = 0;
            RoundCount = 0;
            MonsterSlainNumber = 0;
            ExperienceGainedTotal = 0;
            AutoBattle = false;

            GameDate = DateTime.Now;
            CharacterAtDeathList = new List<Character>();
            MonstersKilledList = new List<Monster>();
            ItemsDroppedList = new List<Item>();
        }

        /// <summary>
        /// create score object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="autoBattle"></param>
        public Score(string name, string desc, bool autoBattle)
        {
            Name = name;
            Description = desc;

            ScoreTotal = 0;
            TurnCount = 0;
            RoundCount = 0;
            MonsterSlainNumber = 0;
            ExperienceGainedTotal = 0;

            AutoBattle = autoBattle;
            GameDate = DateTime.Now;
            CharacterAtDeathList = new List<Character>();
            MonstersKilledList = new List<Monster>();
            ItemsDroppedList = new List<Item>();
        }

        /// <summary>
        /// Update the score based on the passed in values.
        /// </summary>
        /// <param name="newData"></param>
        public void Update(Score newData)
        {
            AutoBattle = newData.AutoBattle;
            MonsterSlainNumber = newData.MonsterSlainNumber;
            ExperienceGainedTotal = newData.ExperienceGainedTotal;
            GameDate = newData.GameDate;

            CharacterAtDeathString = newData.CharacterAtDeathString;
            ItemsDroppedString = newData.ItemsDroppedString;
            MonstersKilledString = newData.MonstersKilledString;

            CharacterAtDeathList = newData.CharacterAtDeathList;
            ItemsDroppedList = newData.ItemsDroppedList;
            MonstersKilledList = newData.MonstersKilledList;
            
            RoundCount = newData.RoundCount;
            ScoreTotal = newData.ScoreTotal;
            TurnCount = newData.TurnCount;

            // From Entity class
            Name = newData.Name;
            Description = newData.Description;
        }

        /// <summary>
        /// Format Score object to string to display.
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var myReturn = $"Game Date: {GameDate}\nTotal Score: {ScoreTotal}\nAuto Battle: {AutoBattle}\n" +
                $"No. of Rounds: {RoundCount}\nNo. of Turns: {TurnCount}\n" +
                $"Dead Monsters: {MonsterSlainNumber}\nXP Gained: {ExperienceGainedTotal}\n" +
                $"\n--- Died Aliens ---\n";
            if(CharacterAtDeathList.Count == 0)
            {
                myReturn += $"No Data \n";
            }
            else
            {
                foreach(Character c in CharacterAtDeathList)
                {
                    if (c != null)
                        myReturn += $"{c.FormatOutput()} \n";
                }
            }

            myReturn += $"\n--- Died Agents ---\n";
            if (MonstersKilledList.Count == 0)
            {
                myReturn += $"No Data \n";
            }
            else
            {
                foreach (Monster m in MonstersKilledList)
                {
                    if(m != null)
                        myReturn += $"{m.FormatOutput()} \n";
                }
            }

            myReturn += $"\n--- Items Dropped ---\n";
            if (ItemsDroppedList.Count == 0)
            {
                myReturn += $"No Data \n";
            }
            else
            {
                foreach (Item i in ItemsDroppedList)
                {
                    if (i != null)
                        myReturn += $"{i.FormatOutput()} \n";
                }
            }
            return myReturn;
        }

        #region ScoreItems

        /// <summary>
        /// Adding a character to the score output as a text string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddCharacterToList( Character data)
        {
            CharacterAtDeathList.Add(data);
            CharacterAtDeathString = JsonConvert.SerializeObject(CharacterAtDeathList);
            return true;
        }

        /// <summary>
        /// All a monster to the list of monsters and their stats
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddMonsterToList( Monster data)
        {
            MonstersKilledList.Add(data);
            MonstersKilledString = JsonConvert.SerializeObject(MonstersKilledList);
            return true;
           
        }

        /// <summary>
        /// All an item to the list of items for score and their stats
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddItemToList(Item data)
        {
            ItemsDroppedList.Add(data);
            ItemsDroppedString = JsonConvert.SerializeObject(ItemsDroppedList);
            return true;

        }

        /// <summary>
        /// All an item to the list of items for score and their stats
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddItemToList(List<Item> data)
        {
            ItemsDroppedList.AddRange(data);
            ItemsDroppedString = JsonConvert.SerializeObject(ItemsDroppedList);
            return true;

        }
        #endregion ScoreItems
    }
}