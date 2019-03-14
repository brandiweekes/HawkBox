using System;
using Crawl.Models;

namespace Crawl.GameEngine
{
    public static class HelperEngine
    {
        private static Random rnd = new Random();

        /// <summary>
        /// get random number based on dice. rolls is used to try multiple times.
        /// </summary>
        /// <param name="rolls"></param>
        /// <param name="dice"></param>
        /// <returns></returns>
        public static int RollDice (int rolls, int dice)
        {
            if (rolls < 1)
            {
                return 0;
            }

            if (dice < 1)
            {
                return 0;
            }

            if (Models.GameGlobals.ForceRollsToNotRandom)
            {
                return rolls * Models.GameGlobals.ForcedRandomValue;
            }

            var myReturn = 0;

            for (var i = 0; i < rolls; i++)
            {
                // Add one to the dice, because random is between.  So 1-10 is rnd.Next(1,11)
                myReturn += rnd.Next(1, dice + 1);
            }

            return myReturn;
        }

        /// <summary>
        /// Find out if Monster can steal Items.
        /// </summary>
        /// <returns></returns>
        public static bool DoesMonsterHaveChanceToSteal()
        {
            // check if monster steal items flag is On or Off.
            if (GameGlobals.EnableMonstersToStealItems)
            {
                int diceRoll = RollDice(1, 20);
                int _chance = (int)Math.Floor((GameGlobals.PercentageChanceToStealItems * 20) / (double)100);
                if (diceRoll <= _chance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find out if Monster can multiply.
        /// </summary>
        /// <returns></returns>
        public static bool CanMonsterMultiply()
        {
            if (GameGlobals.EnableMonstersToMultiply)
            {
                int diceRoll = RollDice(1, 20);
                int _chance = (int)Math.Floor((GameGlobals.PercentageChanceToMultiply * 20) / (double)100);
                if (diceRoll <= _chance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find out if rebound attack is possible or not.
        /// </summary>
        /// <returns></returns>
        public static bool ReboundAttack()
        {
            if (GameGlobals.EnableReboundAttack)
            {
                int diceRoll = RollDice(1, 20);
                int _chance = (int)Math.Floor((GameGlobals.PercentageChanceToRebound * 20) / (double)100);
                if (diceRoll <= _chance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
