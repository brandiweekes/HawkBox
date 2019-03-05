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

        public BattleEngine BattleEngine;

        /// <summary>
        /// initalize properties.
        /// </summary>
        public AutoBattleEngine()
        {
            BattleEngine = new BattleEngine();
        }

        /// <summary>
        /// Entry point to initialize auto battle.
        /// </summary>
        public void RunAutoBattle()
        {

            BattleEngine.StartBattle(true);


            //// * Pick 6 Characters
            //var CharacterList = GetListOfCharacter(6);

            //// Initialize the Battle
            //BattleEngine = new BattleEngine();

            //BattleEngine.CharacterList = CharacterList;

            //BattleEngine.StartBattle(true);
            //Debug.WriteLine("Battle Starting...");

            //// * Start a Round
            //BattleEngine.StartRound();
            //Debug.WriteLine("Round Starting...");

            //RoundEnum result;

            //do
            //{
            //    Debug.WriteLine("Performing next turn...");
            //    result = BattleEngine.RoundNextTurn();
            //    Debug.WriteLine("Turn over...");

            //    // do the Round
            //    // Turn loop happens inside the Round
            //    if (result == RoundEnum.NewRound)
            //    {
            //        BattleEngine.NewRound();
            //        Debug.WriteLine("New round beginning...");
            //    }

            //}
            //while (result != RoundEnum.GameOver);//end condition);

            //// Save Score
            //var myScore = GetFinalScoreObject();
            //Debug.WriteLine("Score retrieved, score total: " + myScore.ScoreTotal);

            //await ScoresViewModel.Instance.AddAsync(myScore);
            //Debug.WriteLine("Final score saved");

            //BattleEngine.EndBattle();
            //Debug.WriteLine("Battle ended.");

            //var myOutput = FormatOutput();
            //Debug.WriteLine("End of AutoBattle RunAutoBattle()");
        }

        // Output Score




        //public List<Character> GetListOfCharacter(int number)
        //{
        //    // Number of characters cannot be less than zero or equal to zero.
        //    if (number <= 0)
        //    {
        //        return null;
        //    }

        //    var _instance = CharactersViewModel.Instance;
        //    _instance.LoadCharactersCommand.Execute(null);

        //    var _charactersDataset = _instance.Dataset;
        //    var _count = _charactersDataset.Count;

        //    // No. of characters selected cannot be greater than dataset count
        //    if (number > _count)
        //    {
        //        return null;
        //    }

        //    var myReturn = new List<Character>();

        //    // Iterate for given number of times and fetch characters from dataset randomly.
        //    for (var i = 0; i < number; i++)
        //    {
        //        myReturn.Add(_charactersDataset[rnd.Next(0, _count)]);
        //    }

        //    return myReturn;
        //}

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
