using Crawl.Models;
using Crawl.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    class AutoBattleEngine
    {
        /// <summary>
        /// Random should only be instantiated once
        /// Because each call to new Random will reset the seed value, and thus the numbers generated
        /// You can control the seed value for Random by passing a value to the constructor
        /// Do that if you want to be able able get the same sequence of Random over and over
        /// </summary>
        private static Random rnd = new Random();

        public BattleEngine BattleEngine = new BattleEngine();

        // Start here...

        public async void RunAutoBattle()
        {


            // * Pick 6 Characters
            var CharacterList = GetListOfCharacter(6);

            // Initialize the Battle
            BattleEngine = new BattleEngine();

            BattleEngine.CharacterList = CharacterList;

            BattleEngine.StartBattle(true);
            Debug.WriteLine("Battle Starting...");

            // * Start a Round
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

            await ScoresViewModel.Instance.AddAsync(myScore);
            Debug.WriteLine("Final score saved");

            BattleEngine.EndBattle();
            Debug.WriteLine("Battle ended.");
        }

        // Output Score

        public AutoBattleEngine()
        {

        }


        public List<Character> GetListOfCharacter(int number)
        {
            // Number of characters cannot be less than zero or equal to zero.
            if(number <= 0)
            {
                return null;
            }

            var _charactersDataset = CharactersViewModel.Instance.Dataset;

            // if Dataset is not defined
            if(_charactersDataset == null)
            {
                return null;
            }
            var _count = _charactersDataset.Count;

            // No. of characters selected cannot be greater than dataset count
            if(number > _count)
            {
                return null;
            }
            var myReturn = new List<Character>();

            // Iterate for given number of times and fetch characters from dataset randomly.
            for (var i = 0; i < number; i++)
            {
                myReturn.Add(_charactersDataset[rnd.Next(1, _count + 1)]);
            }

            return myReturn;
        }

        //public bool isRound()
        //{
        // implement in round engine
        //    return true;
        //}

        //public bool StartNewRound()
        //{
        //    // Implement Starting a New Round
        //    return true;
        //}

        public Score GetFinalScoreObject()
        {
            var myReturn = BattleEngine.BattleScore;

            return myReturn;
        }

        public string FormatOutput()
        {
            var myReturn = "END OF BATTLE REPORT: \n";

            foreach (var data in BattleEngine.CharacterList)
            {
                myReturn += " CHARACTER : " + data.Name;
            }

            myReturn += " " + "ROUND COUNT : " + BattleEngine.BattleScore.RoundCount.ToString();

            myReturn += " " + "TURN COUNT : " + BattleEngine.BattleScore.TurnCount.ToString();

            myReturn += " " + "SCORE TOTAL : " + BattleEngine.BattleScore.ScoreTotal.ToString();

            myReturn += " EXPERIENCE GAINED : " + BattleEngine.BattleScore.ExperienceGainedTotal;

            myReturn += " MONSTERS SLAIN COUNT : " + BattleEngine.BattleScore.MonsterSlainNumber;

            myReturn += " MONSTERS DEATH LIST : ";
            foreach (var data in BattleEngine.BattleScore.MonstersKilledList)
            {
                myReturn += BattleEngine.BattleScore.MonstersKilledList + ", ";
            }

            myReturn += " ITEMS DROPPED LIST : ";
            foreach (var data in BattleEngine.BattleScore.MonstersKilledList)
            {
                myReturn += BattleEngine.BattleScore.MonstersKilledList + ", ";
            }

            Debug.WriteLine(myReturn);

            return myReturn;
        }

    }
}
