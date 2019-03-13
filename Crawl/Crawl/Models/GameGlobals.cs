
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
        ///  Flip the Random State (false to true etc...)
        /// Call this after setting, to restore...
        /// </summary>
        public static void ToggleRandomState()
        {
            ForceRollsToNotRandom = !ForceRollsToNotRandom;
        }

        /// <summary>
        /// Maximum players is initialized to defalt value.
        /// </summary>
        public static void ResetMaxNumberPartyPlayers()
        {
            MaxNumberPartyPlayers = 6;
        }


        // Debug Settings
        public static bool EnableCriticalMissProblems = true;
        public static bool EnableCriticalHitDamage = true;
    }
}
