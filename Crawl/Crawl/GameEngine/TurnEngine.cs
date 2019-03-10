using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Crawl.Models;
using Crawl.ViewModels;
using System.Linq;

namespace Crawl.GameEngine
{

    /// * 
    // * Need to decide who takes the next turn
    // * Target to Attack
    // * Should Move, or Stay put (can hit with weapon range?)
    // * Death
    // * Manage Round...
    // * /

    public class TurnEngine
    {
        // Holds the official score
        public Score BattleScore = new Score();

        public string AttackerName = string.Empty;
        public string TargetName = string.Empty;
        public string AttackStatus = string.Empty;

        public string TurnMessage = string.Empty;
        public string TurnMessageSpecial = string.Empty;
        public string LevelUpMessage = string.Empty;

        public int DamageAmount = 0;
        public HitStatusEnum HitStatus = HitStatusEnum.Unknown;

        public List<Item> ItemPool = new List<Item>();

        //public List<Item> ItemList = new List<Item>();

        public List<Monster> MonsterList = new List<Monster>();
        public List<Character> CharacterList = new List<Character>();

        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over

        // Character Attacks...
        public bool TakeTurn(Character Attacker)
        {
            return true;
        }

        // Monster Attacks...
        public bool TakeTurn(Monster Attacker)
        {
            return true;
        }

        // Monster Attacks Character
        public bool TurnAsAttack(Monster Attacker, int AttackScore, Character Target, int DefenseScore)
        {
            return true;
        }

        // Character attacks Monster
        public bool TurnAsAttack(Character Attacker, int AttackScore, Monster Target, int DefenseScore)
        {
            return true;
        }

        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {

            HitStatus = HitStatusEnum.Unknown;

            return HitStatus;
        }

        /// <summary>
        /// Character will attack the monster with the lowest health
        /// </summary>
        /// <param name="data"></param>
        /// <returns>monster with lowest health</returns>
        public Monster AttackChoice(Character data)
        {
            int current = 0;

            // check if MonsterList is null; return null
            if (this.MonsterList == null)
            {
                return null;
            }

            // check if MonsterList instantiated but no monster in it; return null
            if(this.MonsterList.Count < 1)
            {
                return null;
            }

            // MonsterList has monsters in it, 
            // order the list by living monsters lowest health
            var orderMonsterHealth = this.MonsterList
                .OrderBy(m => m.Attribute.CurrentHealth)
                .ThenByDescending(a => a.Alive);
            
            // check if first monster is alive 
            if(orderMonsterHealth.First().Alive != true)
            {
                // first monster is dead, 
                // so cycle through list until find first Alive monster
                while (current < orderMonsterHealth.Count() && 
                  orderMonsterHealth.ElementAt(current).Alive == false)
                {
                    current++;
                }
            }

            // found a living monster with lowest health
            if(current < orderMonsterHealth.Count())
            {
                // current is index of living monster with lowest health
                var lowestHealthMonster = orderMonsterHealth.ElementAt(current);
                // return the monster with the lowest health to attack
                return lowestHealthMonster;
            }

            // all monsters are dead, return null
            return null;
        }

        // Decide which to attack
        public Character AttackChoice(Monster data)
        {
            return null;
        }

        // Will drop between 1 and 4 items from the item set...
        public List<Item> GetRandomMonsterItemDrops(int round)
        {
            var myList = new List<Item>();

                return myList;
        }

        public string DetermineCriticalMissProblem(Character attacker)
        {
            return " Not Implemented ";
        }
    }
}
