using Crawl.Models;
using Crawl.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    public class AutoBattleEngine
    {
        /// <summary>
        /// Random should only be instantiated once
        /// Because each call to new Random will reset the seed value, and thus the numbers generated
        /// You can control the seed value for Random by passing a value to the constructor
        /// Do that if you want to be able able get the same sequence of Random over and over
        /// </summary>
        private static Random rnd = new Random();

        // BattleEngine object. used in initialize battle.
        public BattleEngine BattleEngine;

        // character viewmodel. used to fetch characters from datastore.
        private CharactersViewModel _instance;

        /// <summary>
        /// initalize properties.
        /// </summary>
        public AutoBattleEngine()
        {
            BattleEngine = new BattleEngine();
            _instance = CharactersViewModel.Instance;
            
        }

        /// <summary>
        /// Entry point to run auto battle.
        /// </summary>
        public void RunAutoBattle()
        {
            // Pick Characters based on maximum party number.
            var CharacterList = GetListOfCharacter(GameGlobals.MaxNumberPartyPlayers);

            if(CharacterList == null)
            {
                Debug.WriteLine($"No Characters. Battle didn't happen. returning....");
                return;
            }

            Debug.WriteLine("Adding characters to Battle.");
            // Assign characters to Battle.
            BattleEngine.CharacterList = CharacterList;

            // Start Battle - AutoBattle mode enabled
            BattleEngine.StartBattle(true);
            Debug.WriteLine("Starting Battle.");

            // Start a Round
            BattleEngine.StartRound();
            Debug.WriteLine("Round Starting...");

            RoundEnum result;

            do
            {
                Debug.WriteLine("Performing next turn...");
                result = BattleEngine.RoundNextTurn();
                Debug.WriteLine("Turn over...");

                // do the Round
                // Turn loop happens inside the Round
                if (result == RoundEnum.NewRound)
                {
                    BattleEngine.NewRound();
                    Debug.WriteLine("New round beginning...");
                }

            }
            while (result != RoundEnum.GameOver);//end condition);

            // Save Score
            var myScore = GetFinalScoreObject();
            Debug.WriteLine("Score retrieved, score total: " + myScore.ScoreTotal);

            ScoresViewModel.Instance.AddAsync(myScore).GetAwaiter().GetResult();
            Debug.WriteLine("Final score saved");

            BattleEngine.EndBattle();
            Debug.WriteLine("Battle ended.");

            var myOutput = FormatOutput();
            Debug.WriteLine("End of AutoBattle RunAutoBattle()");
        }

        /// <summary>
        /// Get characters from datastore.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public List<Character> GetListOfCharacter(int number)
        {
            // Number of characters cannot be less than zero or equal to zero.
            if (number <= 0)
            {
                Debug.WriteLine($"Party members is less than or equal to zero. returned null.");
                return null;
            }

            _instance.LoadCharactersCommand.Execute(null);

            var _charactersDataset = _instance.Dataset;
            var _count = _charactersDataset.Count;

            // Check if the Character list is empty
            if (_count < 1)
            {
                Debug.WriteLine($"No characters in datastore. returned null.");
                return null;
            }

            // No. of characters selected cannot be greater than dataset count
            if (number > _count)
            {
                Debug.WriteLine($"Party members count is greater than characters in datastore. returned null.");
                return null;
            }

            var myReturn = new List<Character>();

            // add Characters up to that strength...
            var ScaleLevelMax = GameGlobals.MaxCharacterLevelForBattle;
            var ScaleLevelMin = GameGlobals.MinCharacterLevelForBattle;

            Debug.WriteLine($"Getting Characters with level between {ScaleLevelMin} and {ScaleLevelMax}");

            // Get Characters and scale them.
            do
            {
                var Data = GetRandomCharacter(ScaleLevelMin, ScaleLevelMax);
                myReturn.Add(Data);
            } while (myReturn.Count < number);

            Debug.WriteLine($"Characters randomly choosen from datastore.");
            return myReturn;
        }

        /// <summary>
        /// get character between given levels and assign random items at each location.
        /// </summary>
        /// <param name="ScaleLevelMin"></param>
        /// <param name="ScaleLevelMax"></param>
        /// <returns></returns>
        public Character GetRandomCharacter(int ScaleLevelMin, int ScaleLevelMax)
        {
            // roll dice to pick characters from datastore.
            var rnd = HelperEngine.RollDice(0, _instance.Dataset.Count);
            var myData = _instance.Dataset[rnd];

            // roll dice to selecte random level between given min. level and max. level.
            var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
            myData.ScaleLevel(rndScale);

            return myData;
        }

        /// <summary>
        /// Final score object.
        /// </summary>
        /// <returns></returns>
        public Score GetFinalScoreObject()
        {
            return BattleEngine.GetFinalScore();
        }

        /// <summary>
        /// Format score object to display.
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            return BattleEngine.FormatOutput();
        }

    }
}
