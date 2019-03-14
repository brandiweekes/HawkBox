
// Global switches for the overall game to use...

namespace Crawl.Models
{
    /// <summary>
    /// Set of global variables used in application. They are used to control game behavior.
    /// </summary>
    public static class GameGlobals
    {
        // Max number of Players in a Party
        public static int MaxNumberPartyPlayers = 6;

        // Minimum character level during battle.
        public static int MinCharacterLevelForBattle = 1;

        // Maximum character level during battle.
        public static int MaxCharacterLevelForBattle = 3;

        // Turn on to force Rolls to be non random
        public static bool ForceRollsToNotRandom = false;

        // What number should return for random numbers (1 is good choice...)
        public static int ForcedRandomValue = 1;

        // What number to use for ToHit values (1,2, 19, 20)
        public static int ForceToHitValue = 20;

        // Forces Monsters to hit with a set value
        // Zero, because don't want to add it in unless it is used...
        public static int ForceMonsterDamangeBonusValue = 0;

        // Forces Characters to hit with a set value
        // Zero, because don't want to add it in unless it is used...
        public static int ForceCharacterDamangeBonusValue = 0; 

        // Allow Random Items when monsters die...
        public static bool AllowMonsterDropItems = true;


        public static bool TimeWarp = false;

        public static int TimeWarpChance = 0;

        public static void setTimeWarp(bool value)
        {
            TimeWarp = value;
        }

        public static void setTimeWarpChance(int value)
        {
            TimeWarpChance = value;
        }

        /// <summary>
        /// Turn Off Random Number Generation, and use the passed in values.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="hit"></param>
        public static void SetForcedRandomNumbers(int value, int hit)
        {
            ForceRollsToNotRandom = true;
            ForcedRandomValue = value;
            ForceToHitValue = hit;
        }

        /// <summary>
        /// Flip the Random State (false to true etc...)
        /// Call this after setting, to restore...
        /// </summary>
        public static void ToggleRandomState()
        {
            ForceRollsToNotRandom = !ForceRollsToNotRandom;
        }

        /// <summary>
        /// Turn Random State Off
        /// </summary>
        public static void DisableRandomValues()
        {
            ForceRollsToNotRandom = false;
        }

        /// <summary>
        /// Turn Random State On
        /// </summary>
        public static void EnableRandomValues()
        {
            ForceRollsToNotRandom = true;
        }

        /// <summary>
        /// Turn On random force flag and set force random value.
        /// </summary>
        /// <param name="value"></param>
        public static void SetForcedRandomNumbersValue(int value)
        {
            EnableRandomValues();
            ForcedRandomValue = value;
        }

        /// <summary>
        /// Turn on Random Force flag and set for to hit value.
        /// </summary>
        /// <param name="value"></param>
        public static void SetForceToHitValue(int hit)
        {
            EnableRandomValues();
            ForceToHitValue = hit;
        }

        /// <summary>
        /// Maximum players is initialized to defalt value.
        /// </summary>
        public static void ResetMaxNumberPartyPlayers()
        {
            MaxNumberPartyPlayers = 6;
        }


        // Debug Settings
        public static bool EnableCriticalMissProblems = false;
        public static bool EnableCriticalHitDamage = true;

        #region Monsters steal Items

        // flag to enable/disable monster steal items
        public static bool EnableMonstersToStealItems = false;
        // default % of chance
        public static int PercentageChanceToStealItems = 0;

        /// <summary>
        /// set % of chance that monster can steal items.
        /// </summary>
        /// <param name="chance"></param>
        public static void SetPercentageChanceToStealItems(int chance)
        {
            EnableMonstersToStealItems = true;
            PercentageChanceToStealItems = chance;
        }

        #endregion Monsters steal Items

        #region Multiply Monsters

        // flag to enable/disable monster steal items
        public static bool EnableMonstersToMultiply = false;
        // default % of chance
        public static int PercentageChanceToMultiply = 0;

        /// <summary>
        /// set % of chance that monster can steal items.
        /// </summary>
        /// <param name="chance"></param>
        public static void SetPercentageChanceToMultiply(int chance)
        {
            EnableMonstersToStealItems = true;
            PercentageChanceToMultiply = chance;
        }

        #endregion Multiply Monsters

        #region Rebound Attack

        // flag to enable/disable monster steal items
        public static bool EnableReboundAttack= false;
        // default % of chance
        public static int PercentageChanceToRebound = 0;

        /// <summary>
        /// set % of chance that monster can steal items.
        /// </summary>
        /// <param name="chance"></param>
        public static void SetPercentageChanceToRebound(int chance)
        {
            EnableReboundAttack = true;
            PercentageChanceToRebound = chance;
        }

        #endregion Rebound Attack

        #region Miracle Max

        public static bool EnableMiracleMaxOnCharacters = false;

        public static void ToogleMiracleMaxOnCharacters()
        {
            EnableMiracleMaxOnCharacters = !EnableMiracleMaxOnCharacters;
        }

        #endregion Miracle Max
    }
}
