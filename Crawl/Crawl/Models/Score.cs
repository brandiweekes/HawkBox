using System;

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
        public string CharacterAtDeathList { get; set; }

        // All of the monsters killed and their stats. 
        // Only use Get only, set will be done by the Add feature.
        public string MonstersKilledList { get; set; }

        // All of the items dropped and their stats. 
        // Only use Get only, set will be done by the Add feature.
        public string ItemsDroppedList { get; set; }

        // Instantiate new Score
        public Score()
        {
            // Implement
            GameDate = DateTime.Now;

        }

        public Score(string name, string desc, string imageUri, bool autoBattle)
        {
            Name = name;
            Description = desc;
            ImageURI = imageUri;
            AutoBattle = autoBattle;
            GameDate = DateTime.Now;
        }

        // Update the score based on the passed in values.
        public void Update(Score newData)
        {
            this.AutoBattle = newData.AutoBattle;
            this.MonsterSlainNumber = newData.MonsterSlainNumber;
            this.ExperienceGainedTotal = newData.ExperienceGainedTotal;
            this.GameDate = newData.GameDate;

            this.CharacterAtDeathList = newData.CharacterAtDeathList;
            this.ItemsDroppedList = newData.ItemsDroppedList;
            this.MonstersKilledList = newData.MonstersKilledList;
            
            this.RoundCount = newData.RoundCount;
            this.ScoreTotal = newData.ScoreTotal;
            this.TurnCount = newData.TurnCount;

            // From Entity class
            this.Name = newData.Name;
            this.Description = newData.Description;
            this.ImageURI = newData.ImageURI;

        }

        #region ScoreItems

        // Adding a character to the score output as a text string
        public bool AddCharacterToList( Character data)
        {
            CharacterAtDeathList += data.FormatOutput();
            return true;
        }

        // All a monster to the list of monsters and their stats
        public bool AddMonsterToList( Monster data)
        {
            MonstersKilledList += data.FormatOutput();
            return true;
           
        }

        // All an item to the list of items for score and their stats
        public bool AddItemToList( Item data)
        {
            ItemsDroppedList += data.FormatOutput();
            return true;

        }
        #endregion ScoreItems
    }
}