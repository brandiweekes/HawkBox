using System;

namespace Crawl.Models
{
    /// <summary>
    /// Base Player is either a Monster or a Character.  It has the shared aspects of both...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePlayer<T> : BasePlayerItemSlots<T>
    {
        // Level of the character, or difficulty level of the monster
        public int Level { get; set; }

        // Current experience gained, or to give
        public int ExperienceTotal { get; set; }

        public bool Alive { get; set; }

        // The AttributeString will be unpacked and stored in the top level of Character as actual attributes, 
        // but it needs to go here as a string so it can be saved to the database.
        public string AttributeString { get; set; }

        // The Dice to use when leveling up, defualt is d10
        public int HealthDice { get; set; } = 10;

        /// <summary>
        /// Causes death by setting Alive property to false;
        /// </summary>
        public void CauseDeath()
        {
            Alive = false;
        }

        /// <summary>
        /// Get Level based Damage. 1/4 of the Level of the Player is the base damage they do.
        /// </summary>
        /// <returns></returns>
        public int GetLevelBasedDamage()
        {
            return (int)Math.Ceiling(Level * .25);
        }

    }
}