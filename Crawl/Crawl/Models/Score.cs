using Newtonsoft.Json;
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

        // Instantiate new Score
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
            AutoBattle = true;

            GameDate = DateTime.Now;
            CharacterAtDeathList = new List<Character>();
            MonstersKilledList = new List<Monster>();
            ItemsDroppedList = new List<Item>();
        }

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

        // Update the score based on the passed in values.
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
            var myReturn = $"Game Date : {GameDate} \t Total Score : {ScoreTotal} \t Auto Battle : {AutoBattle}" +
                $"No. of Rounds : {RoundCount} \t No. of Turns : {TurnCount} \t Dead Monsters : {MonsterSlainNumber} \t XP Gained : {ExperienceGainedTotal}" +
                $"*** Died Aliens ***\n";
            if(CharacterAtDeathList.Count == 0)
            {
                myReturn += $"No Data \n";
            }
            else
            {
                foreach(Character c in CharacterAtDeathList)
                {
                    myReturn += $"{c.FormatOutput()} \n";
                }
            }

            myReturn += $"*** Died Agents ***\n";
            if (MonstersKilledList.Count == 0)
            {
                myReturn += $"No Data \n";
            }
            else
            {
                foreach (Monster m in MonstersKilledList)
                {
                    myReturn += $"{m.FormatOutput()} \n";
                }
            }

            myReturn += $"*** Items Dropped ***\n";
            if (ItemsDroppedList.Count == 0)
            {
                myReturn += $"No Data \n";
            }
            else
            {
                foreach (Item i in ItemsDroppedList)
                {
                    myReturn += $"{i.FormatOutput()} \n";
                }
            }
            return myReturn;
        }

        #region ScoreItems

        // Adding a character to the score output as a text string
        public bool AddCharacterToList( Character data)
        {
            CharacterAtDeathList.Add(data);
            CharacterAtDeathString = JsonConvert.SerializeObject(CharacterAtDeathList);
            return true;
        }

        // All a monster to the list of monsters and their stats
        public bool AddMonsterToList( Monster data)
        {
            MonstersKilledList.Add(data);
            MonstersKilledString = JsonConvert.SerializeObject(MonstersKilledList);
            return true;
           
        }

        // All an item to the list of items for score and their stats
        public bool AddItemToList( Item data)
        {
            ItemsDroppedList.Add(data);
            ItemsDroppedString = JsonConvert.SerializeObject(ItemsDroppedList);
            return true;

        }
        #endregion ScoreItems
    }
}