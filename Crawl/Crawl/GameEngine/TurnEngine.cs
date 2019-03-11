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

        /// <summary>
        /// Character will attack a monster
        /// a target monster is selected, called by AttackChoice
        /// AttackScore and DefendScore are set
        /// Character, Target, both scores sent to TurnAsAttack
        /// </summary>
        /// <param name="Attacker">current attacking player</param>
        /// <returns>
        /// false if no attack happens this turn, 
        /// else true
        /// </returns>
        public bool TakeTurn(Character Attacker)
        {
            // call to get a monster to attack
            var Target = this.AttackChoice(Attacker);

            // if target is null, return false
            // turn is over, no attack happens
            if(Target == null)
            {
                // all monsters dead or list null
                // no attack happened this turn
                return false;
            }

            // set the attacking score of Character
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            // set the defending score of Monster Target
            var DefendScore = Target.Level + Target.GetDefense();
            // call for this turn's attack (character vs monster)
            var attackStatus = this.TurnAsAttack(Attacker, 
                AttackScore, Target, DefendScore);
            
            // check if something happened preventing an attack
            if(!attackStatus)
            {
                // attack failed in TurnAsAttack
                return false;
            }

            // attack happened this turn, return true 
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
            this.AttackerName = Attacker.Name;
            this.TargetName = Target.Name;

            this.BattleScore.TurnCount++;

            // Roll To Hit
            var HitStatus = this.RollToHitTarget(AttackScore, DefenseScore);

            // Decide Hit or Miss then Determine Damage
            if(HitStatus == HitStatusEnum.Miss)
            {
                this.DamageAmount = 0;
                return true;
            }
            
            if(HitStatus == HitStatusEnum.CriticalMiss)
            {
                this.DamageAmount = 0;
                return true;
            }
            
            if(HitStatus == HitStatusEnum.Hit)
            {
                var damage = Attacker.GetDamageRollValue();
                this.DamageAmount = damage;
            }

            // Death
            // Drop Items
            
            // Turn Over
            return true;
        }

        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = HelperEngine.RollDice(1, 20);

            if(GameGlobals.ForceRollsToNotRandom)
            {
                d20 = GameGlobals.ForceToHitValue;
            }

            if(d20 == 1)
            {
                this.HitStatus = HitStatusEnum.CriticalMiss;
            }

            if(d20 == 20)
            {
                this.HitStatus = HitStatusEnum.CriticalHit;
            }

            if(d20 > 1 && d20 < 20)
            {
                if(d20 + AttackScore < DefenseScore)
                {
                    this.HitStatus = HitStatusEnum.Miss;
                }
                else
                {
                    this.HitStatus = HitStatusEnum.Hit;
                }               
            }
            

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
