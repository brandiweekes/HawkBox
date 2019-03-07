using Crawl.GameEngine;
using Crawl.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;

namespace Crawl.Models
{
    // The Character is the higher level concept.  This is the Character with all attributes defined.
    public class Character : BasePlayer<Character>
    {
        // Add in the actual attribute class
        [Ignore]
        public AttributeBase Attribute { get; set; }

        /// <summary>
        /// Create new Charater with default values.
        /// </summary>
        public Character()
        {
            Name = "Character Name";
            Description = "This is a Character description.";
            ImageURI = HawkboxResources.Aliens_Char_1;

            Level = 1;
            ExperienceTotal = 0;
            Alive = true;

            Attribute = new AttributeBase(1, 1, 1, 10, 10);
            AttributeString = AttributeBase.GetAttributeString(Attribute);
        }

        /// <summary>
        /// Create new Character by passing Name, Description, ImageURL.
        /// rest all parameters are optional.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="imageUri"></param>
        /// <param name="level"></param>
        /// <param name="xpTotal"></param>
        /// <param name="alive"></param>
        /// <param name="speed"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="maxHealth"></param>
        /// <param name="currentHealth"></param>
        /// <param name="head"></param>
        /// <param name="feet"></param>
        /// <param name="necklace"></param>
        /// <param name="primaryHand"></param>
        /// <param name="offhand"></param>
        /// <param name="rightFinger"></param>
        /// <param name="leftFinger"></param>
        public Character(string name, string description, string imageUri,
            int level = 1, int xpTotal = 0, bool alive = true, 
            int speed = 1, int attack = 1, int defense = 1, int maxHealth = 10, int currentHealth = 10,
            string head = null, string feet = null, string necklace = null, string primaryHand = null, 
            string offhand = null, string rightFinger = null, string leftFinger = null)
        {
            Name = name;
            Description = description;
            ImageURI = imageUri;

            Level = level;
            ExperienceTotal = xpTotal;
            Alive = alive;

            Attribute = new AttributeBase(speed, attack, defense, maxHealth,currentHealth);
            AttributeString = AttributeBase.GetAttributeString(Attribute);

            Head = head;
            Feet = feet;
            Necklace = necklace;
            PrimaryHand = primaryHand;
            OffHand = offhand;
            RightFinger = rightFinger;
            LeftFinger = leftFinger;
        }

        /// <summary>
        /// Create a new character, based on existing Character
        /// </summary>
        /// <param name="newData"></param>
        public Character(Character newData)
        {
            Update(newData);
        }

        /// <summary>
        /// Update the character information based on newData provided.
        /// </summary>
        /// <param name="newData"></param>
        public void Update(Character newData)
        {
            if(newData == null)
                return;

            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;

            // Populate the Attributes
            AttributeString = newData.AttributeString;
            Attribute = new AttributeBase(newData.AttributeString);

            // Set the strings for the items
            Head = newData.Head;
            Necklace = newData.Necklace;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
        }

        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string.
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var _instance = ItemsViewModel.Instance;
            var myReturn = $"Name : {Name} \n" +
                $"Description : {Description} \n" +
                $"Image : {ImageURI} \n" +
                $"Level : {Level} \t XP : {ExperienceTotal} \n" +
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
            return myReturn;
        }

        #region Basics

        // Upgrades to a set level
        public void ScaleLevel(int level)
        {
            // Implement
        }

        // Level Up
        public bool LevelUp()
        {
            // Implement
            return false;
        }

        // Level up to a number, say Level 3
        public int LevelUpToValue(int value)
        {
            // Implement
            return Level;
        }

        // Add experience
        public bool AddExperience(int newExperience)
        {
            // Implement
            return false;
        }

        // Take Damage
        // If the damage received is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept experience from monsters
        public void TakeDamage(int damage)
        {
            // Implement
        }

        #endregion Basics

        #region GetAttributes
        // Get Attributes

        // Get Attack
        public int GetAttack()
        {
            // Base Attack
            var myReturn = Attribute.Attack;

            // Implement

            // Attack Bonus from Level

            // Get Attack bonus from Items

            return myReturn;
        }

        // Get Speed
        public int GetSpeed()
        {
            // Base value
            var myReturn = Attribute.Speed;

            // Implement

            // Get Bonus from Level

            // Get bonus from Items

            return myReturn;
        }

        // Get Defense
        public int GetDefense()
        {
            // Base value
            var myReturn = Attribute.Defense;

            // Implement

            // Get Bonus from Level

            // Get bonus from Items

            return myReturn;
        }

        // Get Max Health
        public int GetHealthMax()
        {
            // Base value
            var myReturn = Attribute.MaxHealth;

            // Implement

            // Get bonus from Items
            
            return myReturn;
        }

        // Get Current Health
        public int GetHealthCurrent()
        {
            // Base value
            var myReturn = Attribute.CurrentHealth;

            // Implement

            // Get bonus from Items

            return myReturn;
        }

        // Returns the Dice for the item
        // Sword 10, is Sword Dice 10
        public int GetDamageDice()
        {
            var myReturn = 0;

            // Implement

            
            return myReturn;
        }

        // Get the Level based damage
        // Then add the damage for the primary hand item as a Dice Roll
        public int GetDamageRollValue()
        {
            var myReturn = GetLevelBasedDamage();

            // Implement

            
            return myReturn;
        }

        #endregion GetAttributes

        #region Items
        // Drop All Items
        // Return a list of items for the pool of items
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Implement

            // Drop all Items
            
            return myReturn;
        }

        // Remove Item from a set location
        // Does this by adding a new item of Null to the location
        // This will return the previous item, and put null in its place
        // Returns the item that was at the location
        // Nulls out the location
        public Item RemoveItem(ItemLocationEnum itemlocation)
        {
            var myReturn = AddItem(itemlocation, null);

            // Save Changes
            return myReturn;
        }

        // Get the Item at a known string location (head, foot etc.)
        public Item GetItem(string itemString)
        {
            return ItemsViewModel.Instance.GetItem(itemString);
        }

        // Get the Item at a known string location (head, foot etc.)
        public Item GetItemByLocation(ItemLocationEnum itemLocation)
        {
            // Implement

            return null;
        }

        // Add Item
        // Looks up the Item
        // Puts the Item ID as a string in the location slot
        // If item is null, then puts null in the slot
        // Returns the item that was in the location
        public Item AddItem(ItemLocationEnum itemlocation, string itemID)
        {
            Item myReturn = new Item();

            // Implement

            return myReturn;
        }

        // Walk all the Items on the Character.
        // Add together all Items that modify the Attribute Enum Passed in
        // Return the sum
        public int GetItemBonus(AttributeEnum attributeEnum)
        {
            var myReturn = 0;
            Item myItem;
            // Implement

            return myReturn;
        }

        #endregion Items
    }
}