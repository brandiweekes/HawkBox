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

        /// <summary>
        ///  Constructor to initialize Battle objects.
        /// </summary>
        public BattleEngine() : base()
        {
            BattleEngineInit();
        }

        /// <summary>
        /// Sets the new state for the variables for Battle
        /// </summary>
        private void BattleEngineInit()
        {
            BattleScore = new Score();
            CharacterList = new List<Character>();
            ItemPool = new List<Item>();
        }

        /// <summary>
        /// Initializes the Battle to begin. 
        /// </summary>
        /// <param name="isAutoBattle"></param>
        public void StartBattle(bool isAutoBattle)
        {
            if(isBattleRunning)
            {
                return;
            }

            Debug.WriteLine("Battle Starting...");
            isBattleRunning = true;
            BattleScore.AutoBattle = isAutoBattle;
        }

        /// <summary>
        /// Battle is over
        /// Update Battle State, Log Score to Database
        /// </summary>
        public void EndBattle()
        {
            // Set off state
            isBattleRunning = false;

            // Clear Battle data
            ClearData();
        }

        /// <summary>
        /// Clear Score, Character List, Monster List and Item Pool.
        /// </summary>
        public void ClearData()
        {
            BattleScore = new Score();
            CharacterList.Clear();
            MonsterList.Clear();
            ItemPool.Clear();
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

            return myReturn;
        }

        /// <summary>
        /// Get final score object.
        /// </summary>
        /// <returns></returns>
        public Score GetFinalScore()
        {
            return BattleScore;
        }


        /// <summary>
        /// Determine if Auto Battle is On or Off.
        /// </summary>
        /// <returns></returns>
        public bool GetAutoBattleState()
        {
            return BattleScore.AutoBattle;
        }

        /// <summary>
        /// Determine if Battle is still running or not.
        /// </summary>
        /// <returns></returns>
        public bool BattleRunningState()
        {
            return isBattleRunning;
        }
    }
}
