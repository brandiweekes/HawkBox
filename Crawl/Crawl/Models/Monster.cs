using System;
using SQLite;
using Crawl.Controllers;
using Crawl.ViewModels;
using System.Collections.Generic;

namespace Crawl.Models
{
    // The Monster is the higher level concept.  This is the Character with all attirbutes defined.
    public class Monster : BaseMonster
    {
        // Remaining Experience Points to give
        public int ExperienceRemaining { get; set; }

        // Add in the actual attribute class
        public AttributeBase Attribute { get; set; }

        // Make sure Attribute is instantiated in the constructor
        public Monster()
        {
            Name = "Monster name";
            Description = "This is a Monster description.";
            ImageURI = HawkboxResources.Monsters_Male_Agent_A;

            Level = 1;
            ExperienceTotal = 0;
            Alive = true;

            Attribute = new AttributeBase(1, 1, 1, 10, 10);
            AttributeString = AttributeBase.GetAttributeString(Attribute);
        }

        public Monster(string name, string description, string imageUri,
            int level = 1, int xpTotal = 0, bool alive = true,
            int speed = 0, int attack = 0, int defense = 0, int maxHealth = 10, int currentHealth = 10,
            string head = null, string feet = null, string necklace = null, 
            string primaryHand = null, string offhand = null, string rightFinger = null, string leftFinger = null)
        {
            Name = name;
            Description = description;
            ImageURI = imageUri;

            Level = level;
            ExperienceTotal = xpTotal;
            Alive = alive;
            ExperienceRemaining = 0;
            
            // TODO: Not sure of formula. Needed some work here
            //ExperienceRemaining = ExperienceTotal - CalculateExperienceEarned(Damage);

            Attribute = new AttributeBase(speed, attack, defense, maxHealth, currentHealth);
            AttributeString = AttributeBase.GetAttributeString(Attribute);

            Head = head;
            Feet = feet;
            Necklace = necklace;
            PrimaryHand = primaryHand;
            OffHand = offhand;
            RightFinger = rightFinger;
            LeftFinger = leftFinger;
        }

        // Passed in from creating via the Database, so use the guid passed in...
        public Monster(BaseMonster newData)
        {
            // Base information
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;
            Damage = newData.Damage;
            UniqueItem = newData.UniqueItem;

            // TODO: Not sure of formula. Needed some work here
            ExperienceRemaining = ExperienceTotal - CalculateExperienceEarned(Damage);

            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            // Populate the Attributes
            AttributeString = newData.AttributeString;

            Attribute = new AttributeBase(newData.AttributeString);

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklace = newData.Necklace;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;

        }

        // For making a new one for lists etc..
        public Monster(Monster newData)
        {
            Update(newData);
        }

        // Update the values passed in
        public void Update(Monster newData)
        {
            // Base information
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;
            Damage = newData.Damage;
            UniqueItem = newData.UniqueItem;
            ExperienceRemaining = newData.ExperienceRemaining;

            // Populate the Attributes
            AttributeString = newData.AttributeString;
            Attribute = new AttributeBase(newData.AttributeString);

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklace = newData.Necklace;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = $"Name : {Name} \n" +
                $"Description : {Description} \n" +
                $"Image : {ImageURI} \n" +
                $"Level : {Level} \t XP : {ExperienceTotal} \t Remaining XP : {ExperienceRemaining} \t Damage : {Damage} \n" +
                $"*** Attributes ***\n" +
                $"Attack : {Attribute.Attack}\t" +
                $"Defense : {Attribute.Defense}\t" +
                $"Speed : {Attribute.Speed}\t" +
                $"Current Health : {Attribute.CurrentHealth}\t" +
                $"Max. Health : {Attribute.MaxHealth}" +
                $"*** Items at given location ***\n" +
                $"Head : {(Head == null ? null : GetItem(Head).Name)}\t" +
                $"Necklace : {(Necklace == null ? null : GetItem(Necklace).Name)}\t" +
                $"Primary Hand : {(PrimaryHand == null ? null : GetItem(PrimaryHand).Name)}\t" +
                $"Off Hand : {(OffHand == null ? null : GetItem(OffHand).Name)}\t" +
                $"Right Finger : {(RightFinger == null ? null : GetItem(RightFinger).Name)}\t" +
                $"Left Finger : {(LeftFinger == null ? null : GetItem(LeftFinger).Name)}\t" +
                $"Feet : {(Feet == null ? null : GetItem(Feet).Name)}";

            if(UniqueItem != null)
            {
                myReturn += $"\nUnique Item : {GetItem(UniqueItem)}";
            }
            return myReturn;
        }

        // Add or Replace Unique Item to Monster
        public void AddOrReplaceUniqueItem(string itemId)
        {
            UniqueItem = itemId;
        }

        // Update Image for Monster
        public void UpdateMonsterImageURL(string imageUrl)
        {
            ImageURI = imageUrl;
        }

        // Update Attributes for Monster
        public void UpdateMonsterAttributes(AttributeBase attributeBase)
        {
            Attribute = attributeBase;
            AttributeString = AttributeBase.GetAttributeString(Attribute);
        }

        // Upgrades a monster to a set level
        public void ScaleLevel(int level)
        {
            // Level must be between 1-20
            if (level < 1 || level > 20)
                return;

            // Dont update if it's same level
            if (level == Level)
                return;

            Level = level;

        }

        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public void TakeDamage(int damage)
        {
            // Implement
            return;

            // Implement   CauseDeath();
        }

        // Calculate How much experience to return
        // Formula is the % of Damage done up to 100%  times the current experience
        // Needs to be called before applying damage
        public int CalculateExperienceEarned(int damage)
        {
            // Implement
            return 0;

        }

        #region GetAttributes
        // Get Attributes

        // Get Attack
        public int GetAttack()
        {
            // Base Attack
            var myReturn = Attribute.Attack;

            return myReturn;
        }

        // Get Speed
        public int GetSpeed()
        {
            // Base value
            var myReturn = Attribute.Speed;

            return myReturn;
        }

        // Get Defense
        public int GetDefense()
        {
            // Base value
            var myReturn = Attribute.Defense;

            return myReturn;
        }

        // Get Max Health
        public int GetHealthMax()
        {
            // Base value
            var myReturn = Attribute.MaxHealth;

            return myReturn;
        }

        // Get Current Health
        public int GetHealthCurrent()
        {
            // Base value
            var myReturn = Attribute.CurrentHealth;

            return myReturn;
        }

        // Get the Level based damage
        // Then add in the monster damage
        public int GetDamage()
        {
            var myReturn = 0;
            myReturn += Damage;

            return myReturn;
        }

        // Get the Level based damage
        // Then add the damage for the primary hand item as a Dice Roll
        public int GetDamageRollValue()
        {
            return GetDamage();
        }

        #endregion GetAttributes

        #region Items
        // Gets the unique item (if any) from this monster when it dies...
        public Item GetUniqueItem()
        {
            var myReturn = ItemsViewModel.Instance.GetItem(UniqueItem);

            return myReturn;
        }

        // Drop all the items the monster has
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Drop all Items
            Item myItem;

            // Implement

            return myReturn;
        }

        // Get the Item at a known string location (head, foot etc.)
        public Item GetItem(string itemString)
        {
            return ItemsViewModel.Instance.GetItem(itemString);
        }

        #endregion Items
    }
}