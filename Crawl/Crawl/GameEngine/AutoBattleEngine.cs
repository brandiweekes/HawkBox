﻿using Crawl.Models;
using Crawl.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            //instantiate battleengine
            BattleEngine = new BattleEngine();
            //set _instance to characterviewmodel instance
            _instance = CharactersViewModel.Instance;
            
        }

        /// <summary>
        /// Entry point to run auto battle.
        /// </summary>
        public void RunAutoBattle()
        {
            // Pick Characters based on maximum party number.
            var CharacterList = GetListOfCharacter(GameGlobals.MaxNumberPartyPlayers);
            //if getlistofcharacter fails
            if(CharacterList == null)
            {
                //debug line & return
                Debug.WriteLine($"No Characters. Battle didn't happen. returning....");
                return;
            }

            Debug.WriteLine($"Adding characters to Battle. count: {CharacterList.Count}");
            // Assign characters to Battle.
            BattleEngine.CharacterList = CharacterList;

            // Start Battle - AutoBattle mode enabled
            BattleEngine.StartBattle(true);
            Debug.WriteLine("Starting Battle.");

            // Start a Round
            BattleEngine.StartRound();
            Debug.WriteLine($"Round Starting... Round Count: {BattleEngine.BattleScore.RoundCount}");
            Debug.WriteLine($"Monster list count: {BattleEngine.MonsterList.Count}");

            RoundEnum result = RoundEnum.Unknown;

            do
            {
                //next turn
                Debug.WriteLine("Performing next turn...");
                BattleEngine.RoundNextTurn();
                Debug.WriteLine($"Turn over... Turn Count: {BattleEngine.BattleScore.TurnCount}");
                //get roundstateenum result
                result = BattleEngine.RoundStateEnum;
                Debug.WriteLine($"round enum result {result}");

                // do the Round
                // Turn loop happens inside the Round

                //if new round
                if (result == RoundEnum.NewRound)
                {
                    //call newround
                    BattleEngine.NewRound();
                    Debug.WriteLine($"New round beginning...Round Count: {BattleEngine.BattleScore.RoundCount}");
                }

            }
            while (result != RoundEnum.GameOver);//end condition-game over

            //call endround
            BattleEngine.EndRound();
            Debug.WriteLine("Round ended.");

            // Save Score
            var myScore = GetFinalScoreObject();
            Debug.WriteLine("Score retrieved, score total: " + myScore.ScoreTotal);

            ScoresViewModel.Instance.AddAsync(myScore).GetAwaiter().GetResult();
            Debug.WriteLine("Final score saved");
            //end battle
            BattleEngine.EndBattle();
            Debug.WriteLine("Battle ended.");
            //output results
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

            //load possible characters for the upcoming battle
            _instance.LoadCharactersCommand.Execute(null);
            //set dataset & count
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

            //init list of characters to return
            var myReturn = new List<Character>();

            // add Characters up to that strength...
            var ScaleLevelMax = GameGlobals.MaxCharacterLevelForBattle;
            var ScaleLevelMin = GameGlobals.MinCharacterLevelForBattle;

            Debug.WriteLine($"Getting Characters with level between {ScaleLevelMin} and {ScaleLevelMax}");

            // Get Characters.
            do
            {
                //get random character
                var Data = GetRandomCharacter(ScaleLevelMin, ScaleLevelMax);

                //if character not already in list, add copy of character to list
                if (myReturn.Any(ch => ch.Id == Data.Id) == false) {
                    myReturn.Add(new Character(Data));
                }
                
            } while (myReturn.Count < number);

            //return list
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
            var rnd = HelperEngine.RollDice(1, _instance.Dataset.Count);
            var myData = _instance.Dataset[rnd - 1];

            // roll dice to selecte random level between given min. level and max. level.
            var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
            myData.ScaleLevel(rndScale);
            //return character
            return myData;
        }

        /// <summary>
        /// Final score object.
        /// </summary>
        /// <returns></returns>
        public Score GetFinalScoreObject()
        {
            //get score
            return BattleEngine.GetFinalScore();
        }

        /// <summary>
        /// Format score object to display.
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            //call formatoutput
            return BattleEngine.FormatOutput();
        }

    }
}
