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
            CharacterList = new List<Character>();
        }

        /// <summary>
        /// Initializes the Battle to begin. 
        /// </summary>
        /// <param name="isAutoBattle"></param>
        public bool StartBattle(bool isAutoBattle)
        {
            if(isBattleRunning)
            {
                return false;
            }

            Debug.WriteLine("Battle Starting...");
            BattleScore = new Score();
            isBattleRunning = true;
            BattleScore.AutoBattle = isAutoBattle;

            return true;
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
            CharacterList.Clear();
            MonsterList.Clear();
        }

        /// <summary>
        /// Retruns a formated String of the Results of the Battle
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var myReturn = $"Battle Report\n {BattleScore.FormatOutput()}";
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
