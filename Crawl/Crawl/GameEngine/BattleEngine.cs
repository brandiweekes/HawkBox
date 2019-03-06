using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.GameEngine
{
    public class BattleEngine : RoundEngine
    {
        // The status of the actual battle, running or not (over)
        private bool isBattleRunning = false;

        // Character view model to get characters from datastore.
        private CharactersViewModel _instance;

        // Constructor calls Init
        public BattleEngine() : base()
        {
            BattleEngineInit();
            _instance = CharactersViewModel.Instance;
            AddCharactersToBattle();

            var ItemList = ItemsViewModel.Instance.Dataset.ToList();
            foreach (var item in ItemList)
            {
                ItemPool.Add(item);
            }

        }

        // Sets the new state for the variables for Battle
        private void BattleEngineInit()
        {
            BattleScore = new Score();
            CharacterList = new List<Character>();
            ItemPool = new List<Item>();
        }

        // Determine if Auto Battle is On or Off
        public bool GetAutoBattleState()
        {
            return BattleScore.AutoBattle;
        }

        // Return if the Battle is Still running
        public bool BattleRunningState()
        {
            return isBattleRunning;
        }

        // Battle is over
        // Update Battle State, Log Score to Database
        public void EndBattle()
        {
            // Set Score
            BattleScore.ScoreTotal = BattleScore.ExperienceGainedTotal;

            // Set off state
            isBattleRunning = false;

            // Save the Score to the DataStore
            ScoresViewModel.Instance.AddAsync(BattleScore).GetAwaiter().GetResult();

            // Clear Battle data
            ClearData();
        }

        public void ClearData()
        {
            BattleScore = new Score();
            CharacterList.Clear();
            MonsterList.Clear();
            ItemPool.Clear();
        }

        // Initializes the Battle to begin
        public void StartBattle(bool isAutoBattle)
        {
            if(isBattleRunning)
            {
                return;
            }

            Debug.WriteLine("Battle Starting...");
            isBattleRunning = true;
            BattleScore.AutoBattle = isAutoBattle;

            if(isAutoBattle)
            {
                Debug.WriteLine("AutoBattle set to true...");
                ClearData();
                _instance.ForceDataRefresh();
                Debug.WriteLine("Picking random Characters...");
                var _res = AddCharactersToBattle();
                if (!_res)
                {
                    return;
                }
            }

            Debug.WriteLine("Starting Round...");
            // start round 
            StartRound();
        }

        /// <summary>
        ///  Add Characters. Scale them to meet Character Strength...
        /// </summary>
        /// <returns></returns>
        public bool AddCharactersToBattle()
        {
            // Check if the Character list is empty
            if (_instance.Dataset.Count < 1)
            {
                return false;
            }

            // Check to see if the Character list is full, if so, no need to add more...
            if (CharacterList.Count >= GameGlobals.MaxNumberPartyPlayers)
            {
                return true;
            }

            // TODO, determine the character strength
            // add Characters up to that strength...
            var ScaleLevelMax = GameGlobals.MaxCharacterLevelForBattle;
            var ScaleLevelMin = GameGlobals.MinCharacterLevelForBattle;

            Debug.WriteLine($"Getting Characters with level between {ScaleLevelMin} and {ScaleLevelMax}");

            // Get 6 Characters
            do
            {
                var Data = GetRandomCharacter(ScaleLevelMin, ScaleLevelMax);
                CharacterList.Add(Data);
            } while (CharacterList.Count < GameGlobals.MaxNumberPartyPlayers);

            return true;
        }

        /// <summary>
        /// get character between given levels and assign random items at each location.
        /// </summary>
        /// <param name="ScaleLevelMin"></param>
        /// <param name="ScaleLevelMax"></param>
        /// <returns></returns>
        public Character GetRandomCharacter(int ScaleLevelMin, int ScaleLevelMax)
        {
            var rnd = HelperEngine.RollDice(1, _instance.Dataset.Count);

            var myData = new Character(_instance.Dataset[rnd - 1]);

            var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
            myData.ScaleLevel(rndScale);

            // Add Items...
            myData.Head = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Head, AttributeEnum.Unknown);
            myData.Necklace = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Necklass, AttributeEnum.Unknown);
            myData.PrimaryHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.PrimaryHand, AttributeEnum.Unknown);
            myData.OffHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.OffHand, AttributeEnum.Unknown);
            myData.RightFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.RightFinger, AttributeEnum.Unknown);
            myData.LeftFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.LeftFinger, AttributeEnum.Unknown);
            myData.Feet = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Feet, AttributeEnum.Unknown);

            return myData;
        }

        /// <summary>
        /// Retruns a formated String of the Results of the Battle
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var myReturn = $"Battle Report\n" +
                $"*** Characters played in battle ***\n";

            foreach (var data in CharacterList)
            {
                myReturn += $"{data.Name}\n";
            }

            myReturn += BattleScore.FormatOutput();

            Debug.WriteLine(myReturn);

            return myReturn;
        }

        /// <summary>
        /// get final score object
        /// </summary>
        /// <returns></returns>
        public Score GetFinalScore()
        {
            return BattleScore;
        }
    }
}
