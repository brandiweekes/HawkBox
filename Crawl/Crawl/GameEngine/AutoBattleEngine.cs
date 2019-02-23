using Crawl.Models;
using Crawl.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    class AutoBattleEngine
    {
        public BattleEngine BattleEngine = new BattleEngine();

        // Start here...

        public async void RunAutoBattle()
        {


            // * Pick 6 Characters
            var CharacterList = GetListOfCharacter(6);

            // Initialize the Battle
            BattleEngine = new BattleEngine();

            BattleEngine.CharacterList = CharacterList;

            // * Start a Round
            BattleEngine.StartRound();

            do
            {
                // do the Round
                // Turn loop happens inside the Round
                //if (//round is over && characters)
                //    //start new round
            }
            while (false);//end condition);

            // Save Score
            //var myScore = new Score(BattleEngine.BattleScore);

            await ScoresViewModel.Instance.AddAsync(BattleEngine.BattleScore);
        }

        // Output Score

        public AutoBattleEngine()
        {

        }


        public List<Character> GetListOfCharacter(int number)
        {
            var myReturn = new List<Character>();

            // some call to Character DataStore to get characters
            for (var i = 0; i < number; i++)
            {
                myReturn.Add(CharactersViewModel.Instance.Dataset[i]);
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

            // Put a Real score Object here

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
