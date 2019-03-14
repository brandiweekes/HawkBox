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
        /// Character will attack this turn
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

        /// <summary>
        /// Monster will attack this turn
        /// a target character is selected, called by AttackChoice
        /// AttackScore and DefendScore are set
        /// Monster, Target, both scores sent to TurnAsAttack
        /// </summary>
        /// <param name="Attacker">current attacking player</param>
        /// <returns>
        /// false if no attack happens this turn, 
        /// else true
        /// </returns>
        public bool TakeTurn(Monster Attacker)
        {
            // call to get a character to attack
            var Target = this.AttackChoice(Attacker);

            // if target is null, return false
            // turn is over, no attack happens
            if (Target == null)
            {
                // all characters dead or list null
                // no attack happened this turn
                return false;
            }

            // set the attacking score of Monster
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            // set the defending score of Character Target
            var DefendScore = Target.Level + Target.GetDefense();

            // call for this turn's attack (monster vs character)
            var attackStatus = this.TurnAsAttack(Attacker,
                AttackScore, Target, DefendScore);

            // check if something happened preventing an attack
            if (!attackStatus)
            {
                // attack failed in TurnAsAttack
                return false;
            }

            // attack happened this turn, return true 
            return true;
        }

        /// <summary>
        /// Monster attacks Character
        /// determines hit status (hit, miss, crits)
        /// deals damage to character as appropriate
        /// if character dies, items dropped and added to ItemPool
        /// </summary>
        /// <param name="Attacker">monster</param>
        /// <param name="AttackScore">monster strength</param>
        /// <param name="Target">character</param>
        /// <param name="DefenseScore">character defense</param>
        /// <returns>true if attack happens</returns>
        public bool TurnAsAttack(Monster Attacker, int AttackScore, Character Target, int DefenseScore)
        {
            // set name variables for messages
            this.AttackerName = Attacker.Name;
            this.TargetName = Target.Name;

            // increment turn count
            this.BattleScore.TurnCount++;

            // Roll To Hit: determine hit or miss
            var HitStatus = this.RollToHitTarget(AttackScore, DefenseScore);

            // Decide Hit or Miss then Determine Damage

            // On miss, no damage dealt
            if (HitStatus == HitStatusEnum.Miss)
            {
                this.DamageAmount = 0;
                return true;
            }

            // On CriticalMiss, no damage dealt
            if (HitStatus == HitStatusEnum.CriticalMiss)
            {
                this.DamageAmount = 0;

                //TODO CriticalMissProblem
                return true;
            }

            // On Hit or CriticalHit, damage dealt or double damage dealt
            if (HitStatus == HitStatusEnum.Hit || HitStatus == HitStatusEnum.CriticalHit)
            {
                // calculate the damage amount
                var damage = Attacker.GetDamageRollValue();
                // set variable for messages
                this.DamageAmount = damage;

                // check for criticalHit status, apply double damage
                if (GameGlobals.EnableCriticalHitDamage)
                {
                    // CriticalHit deals double damage
                    if (HitStatus == HitStatusEnum.CriticalHit)
                    {
                        this.DamageAmount += damage;
                    }
                }

                // deals damage to Character
                Target.TakeDamage(this.DamageAmount);
            }

            // Check for Death and handle items dropped to ItemPool
            if (Target.Alive == false)
            {
                // remove monster from list of available monsters
                this.CharacterList.Remove(Target);

                // get item count
                int _count = Target.GetItemsCount();

                // Drop Items from monster killed
                var droppedItemsList = Target.DropAllItems();
                // monster dropped at least 1 item
                if (droppedItemsList.Count > 0)
                {
                    // add all items dropped to the item pool 
                    // for end of round
                    this.ItemPool.AddRange(droppedItemsList);
                }
                else
                {
                    if(_count != 0)
                    {
                        Debug.WriteLine("Items are gone. Monsters stole them...");
                    }
                }
            }

            // Turn Over
            return true;
        }

        /// <summary>
        /// Character attacks Monster
        /// determines hit status (hit, miss, crits)
        /// deals damage to monster as appropriate
        /// determines XP earned for character
        /// checks for level up
        /// if monster dies, items dropped and added to ItemPool
        /// </summary>
        /// <param name="Attacker">character</param>
        /// <param name="AttackScore">character strength</param>
        /// <param name="Target">monster</param>
        /// <param name="DefenseScore">monster defense</param>
        /// <returns>true if attack happens</returns>
        public bool TurnAsAttack(Character Attacker, int AttackScore, Monster Target, int DefenseScore)
        { //TODO battle messages
            // set name variables for messages
            this.AttackerName = Attacker.Name;
            this.TargetName = Target.Name;

            // increment turn count
            this.BattleScore.TurnCount++;

            // Roll To Hit: determine hit or miss
            var HitStatus = this.RollToHitTarget(AttackScore, DefenseScore);

            // Decide Hit or Miss then Determine Damage

            // On miss, no damage dealt
            if (HitStatus == HitStatusEnum.Miss)
            {
                this.DamageAmount = 0;
                return true;
            }
            
            // On CriticalMiss, no damage dealt
            if(HitStatus == HitStatusEnum.CriticalMiss)
            {
                this.DamageAmount = 0;

                if (GameGlobals.EnableCriticalMissProblems)
                {
                    this.TurnMessage += DetermineCriticalMissProblem(Attacker);
                }

                //TODO CriticalMissProblem
                return true;
            }
            
            // On Hit or CriticalHit, damage dealt or double damage dealt
            if(HitStatus == HitStatusEnum.Hit || HitStatus == HitStatusEnum.CriticalHit)
            {
                // calculate the damage amount
                var damage = Attacker.GetDamageRollValue();
                // set variable for messages
                this.DamageAmount = damage;

                // check for criticalHit status, apply double damage
                if(GameGlobals.EnableCriticalHitDamage)
                {
                    // CriticalHit deals double damage
                    if(HitStatus == HitStatusEnum.CriticalHit)
                    {
                        this.DamageAmount += damage;
                    }
                }
                // deals damage to Monster
                Target.TakeDamage(this.DamageAmount);

                // Calculate how much experience the character 
                // is awarded from hit on monster
                var XPtoCharacter = Target.CalculateExperienceEarned(this.DamageAmount);
                // Determine if XP gain causes character level up
                var LevelUp = Attacker.AddExperience(XPtoCharacter);

                // set variables for messages
                if (LevelUp)
                {
                    this.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetHealthMax();
                    Debug.WriteLine(LevelUpMessage);
                }
            }


            // Check for Death and handle items dropped to ItemPool
            if(Target.Alive == false)
            {
                // Sleepless Zombies in Seattle
                /* if(SleeplessZombies == true) 
                { 
                    
                
                } 
                else{}
                */

                // remove monster from list of available monsters
                this.MonsterList.Remove(Target);

                // Add one to the monsters killed count...
                BattleScore.MonsterSlainNumber++;

                // Add the monster to the killed list
                this.BattleScore.AddMonsterToList(Target);
                //BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                // Drop Items from monster killed
                var droppedItemsList = Target.DropAllItems();
                // monster dropped at least 1 item
                if (droppedItemsList.Count > 0)
                {
                    // add all items dropped to the item pool 
                    // for end of round
                    this.ItemPool.AddRange(droppedItemsList);
                }
            }
            
            // Turn Over
            return true;
        }

        /// <summary>
        /// Determines if the attacker successfully lands hit
        /// </summary>
        /// <param name="AttackScore">value of attack</param>
        /// <param name="DefenseScore">value of defense</param>
        /// <returns>hit,miss,critical</returns>
        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            // set dice random seed for attack roll dice
            var d20 = HelperEngine.RollDice(1, 20);

            // used for testing to set value of dice roll
            if(GameGlobals.ForceRollsToNotRandom)
            {
                d20 = GameGlobals.ForceToHitValue;
            }

            // critical miss, sets HitStatus
            if(d20 == 1)
            {
                this.HitStatus = HitStatusEnum.CriticalMiss;
            }

            // critical hit, sets HitStatus
            if (d20 == 20)
            {
                this.HitStatus = HitStatusEnum.CriticalHit;
            }

            // miss or hit, sets HitStatus
            if(d20 > 1 && d20 < 20)
            {
                // determine if hit or miss
                if(d20 + AttackScore < DefenseScore)
                {
                    // the attack won't break through the defense shield
                    this.HitStatus = HitStatusEnum.Miss;
                }
                else
                {
                    // the attack will deal damage
                    this.HitStatus = HitStatusEnum.Hit;
                }               
            }
            
            // Hit or Miss or CriticalHit or CriticalMiss
            return HitStatus;
        }

        /// <summary>
        /// Selects the monster with the lowest health
        /// for character to attack
        /// </summary>
        /// <param name="data">attacker info</param>
        /// <returns>monster with lowest health</returns>
        public Monster AttackChoice(Character data)
        {
            // variable to cycle through sorted list to find alive, if needed
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

        /// <summary>
        /// Selects the character with the highest speed
        /// for monster to attack
        /// </summary>
        /// <param name="data">attacker info</param>
        /// <returns>character with highest speed</returns>
        public Character AttackChoice(Monster data)
        {
            // variable to cycle through sorted list to find alive, if needed
            int current = 0;

            // check if CharacterList is null; return null
            if (this.CharacterList == null)
            {
                return null;
            }

            // check if CharacterList instantiated but no monster in it; return null
            if (this.CharacterList.Count() < 1)
            {
                return null;
            }

            // CharacterList has characters in it, 
            // order list by living characters highest to lowest speed
            var orderCharacterSpeed = this.CharacterList
                .OrderByDescending(s => s.Attribute.Speed)
                .ThenByDescending(a => a.Alive);

            // check if first character is alive 
            if (orderCharacterSpeed.First().Alive != true)
            {
                // first character is dead, 
                // so cycle through list until find first Alive character
                while (current < orderCharacterSpeed.Count() &&
                  orderCharacterSpeed.ElementAt(current).Alive == false)
                {
                    current++;
                }
            }

            // found a living character with highest speed
            if (current < orderCharacterSpeed.Count())
            {
                // current is index of living character with highest speed
                var highestSpeedCharacter = orderCharacterSpeed.ElementAt(current);
                // return the character with the highest speed to attack
                return highestSpeedCharacter;
            }

            // all characters are dead, return null
            return null;
        }

        // Will drop between 1 and 4 items from the item set...
        public List<Item> GetRandomMonsterItemDrops(int round)
        {
            var myList = new List<Item>();

            if (!GameGlobals.AllowMonsterDropItems)
            {
                return myList;
            }

            var myItemsViewModel = ItemsViewModel.Instance;

            if (myItemsViewModel.Dataset.Count > 0)
            {
                // Random is enabled so build up a list of items dropped...
                var ItemCount = HelperEngine.RollDice(1, 4);
                for (var i = 0; i < ItemCount; i++)
                {
                    var rnd = HelperEngine.RollDice(1, myItemsViewModel.Dataset.Count);
                    var itemBase = myItemsViewModel.Dataset[rnd - 1];
                    var item = new Item(itemBase);
                    item.ScaleLevel(round);

                    // Make sure the item is added to the global list...
                    var myItem = ItemsViewModel.Instance.CheckIfItemExists(item);
                    if (myItem == null)
                    {
                        // Item does not exist, so add it to the datstore
                        ItemsViewModel.Instance.AddItem_Sync(item);
                    }
                    else
                    {
                        // Swap them because it already exists, no need to create a new one...
                        item = myItem;
                    }

                    // Add the item to the local list...
                    myList.Add(item);
                }
            }

            return myList;
        }

        public string DetermineCriticalMissProblem(Character attacker)
        {
            if (attacker == null)
            {
                return " Invalid Character ";
            }

            var myReturn = " Nothing Bad Happened ";
            Item droppedItem;

            // It may be a critical miss, roll again and find out...
            var rnd = HelperEngine.RollDice(1, 10);
            /*
                1. Primary Hand Item breaks, and is lost forever
                2-4, Character Drops the Primary Hand Item back into the item pool
                5-6, Character drops a random equipped item back into the item pool
                7-10, Nothing bad happens, luck was with the attacker
             */

            switch (rnd)
            {
                case 1:
                    myReturn = " Luckily, nothing to drop from " + ItemLocationEnum.PrimaryHand;
                    var myItem = ItemsViewModel.Instance.GetItem(attacker.PrimaryHand);
                    if (myItem != null)
                    {
                        myReturn = " Item " + myItem.Name + " from " + ItemLocationEnum.PrimaryHand + " Broke, and lost forever";
                    }

                    attacker.PrimaryHand = null;
                    break;

                case 2:
                case 3:
                case 4:
                    // Put on the new item, which drops the one back to the pool
                    myReturn = " Luckily, nothing to drop from " + ItemLocationEnum.PrimaryHand;
                    droppedItem = attacker.AddItem(ItemLocationEnum.PrimaryHand, null);
                    if (droppedItem != null)
                    {
                        // Add the dropped item to the pool
                        ItemPool.Add(droppedItem);
                        myReturn = " Dropped " + droppedItem.Name + " from " + ItemLocationEnum.PrimaryHand;
                    }
                    break;

                case 5:
                case 6:
                    var LocationRnd = HelperEngine.RollDice(1, ItemLocationList.GetListCharacter.Count);
                    var myLocationEnum = ItemLocationList.GetLocationByPosition(LocationRnd);
                    myReturn = " Luckily, nothing to drop from " + myLocationEnum;

                    // Put on the new item, which drops the one back to the pool
                    droppedItem = attacker.AddItem(myLocationEnum, null);
                    if (droppedItem != null)
                    {
                        // Add the dropped item to the pool
                        ItemPool.Add(droppedItem);
                        myReturn = " Dropped " + droppedItem.Name + " from " + myLocationEnum;
                    }
                    break;
            }

            return myReturn;
        }
    
    }
}
