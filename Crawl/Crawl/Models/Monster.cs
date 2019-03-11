using System;
using SQLite;
using Crawl.Controllers;
using Crawl.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Crawl.GameEngine;

namespace Crawl.Models
{
    // The Monster is the higher level concept.  This is the Character with all attirbutes defined.
    public class Monster : BasePlayer<Monster>
    {
        // Remaining Experience Points to give
        public int ExperienceRemaining { get; set; }

        [Ignore]
        // Add in the actual attribute class
        public AttributeBase Attribute { get; set; }

        // Unique Item for Monster
        public string UniqueItem { get; set; }

        // Damage the Monster can do.
        public int Damage { get; set; }

        // Make sure Attribute is instantiated in the constructor
        public Monster()
        {
            Name = "Monster name";
            Description = "This is a Monster description.";
            ImageURI = HawkboxResources.Monsters_Male_Agent_A;

            Level = 1;
            Alive = true;

            Attribute = new AttributeBase();

            ScaleLevel(Level);
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
            ExperienceRemaining = ExperienceTotal;
            Damage = GetLevelBasedDamage() + LevelTable.Instance.LevelDetailsList[Level].Attack;

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

        // For making a new one for lists etc..
        public Monster(Monster newData)
        {
            Update(newData);

            ScaleLevel(Level);
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
        public bool ScaleLevel(int level)
        {
            // minimum level is 0
            if (level < 1)
            {
                return false;
            }

            // Dont update if given level is less than current level
            if (level < Level)
            {
                return false;
            }

            // given level shold not exceed max level.
            if(level > LevelTable.MaxLevel)
            {
                return false;
            }

            Level = level;

            // Set Attributes
            Attribute.Attack = LevelTable.Instance.LevelDetailsList[level].Attack;
            Attribute.Defense = LevelTable.Instance.LevelDetailsList[level].Defense;
            Attribute.Speed = LevelTable.Instance.LevelDetailsList[level].Speed;
            // Roll dice
            Attribute.MaxHealth = HelperEngine.RollDice(Level, HealthDice);
            Attribute.CurrentHealth = Attribute.MaxHealth;
            AttributeString = AttributeBase.GetAttributeString(Attribute);

            // Set XP
            ExperienceTotal = LevelTable.Instance.LevelDetailsList[level + 1].Experience;
            ExperienceRemaining = ExperienceTotal;

            // Set Damage
            Damage = GetLevelBasedDamage() + LevelTable.Instance.LevelDetailsList[Level].Attack;

            return true;
        }

        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public void TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                return;
            }

            Attribute.CurrentHealth = Attribute.CurrentHealth - damage;
            if (Attribute.CurrentHealth <= 0)
            {
                Attribute.CurrentHealth = 0;
                // Death...
                CauseDeath();
            }
        }

        // Calculate How much experience to return
        // Formula is the % of Damage done up to 100%  times the current experience
        // Needs to be called before applying damage
        public int CalculateExperienceEarned(int damage)
        {
            if (damage < 1)
            {
                return 0;
            }

            int remainingHealth = Math.Max(Attribute.CurrentHealth - 1, 0); // Go to 0 is OK...
            double rawPercent = (double)remainingHealth / (double)Attribute.CurrentHealth;
            double deltaPercent = 1 - rawPercent;
            var pointsAllocate = (int)Math.Floor(ExperienceRemaining * deltaPercent);

            // Catch rounding of low values, and force to 1.
            if (pointsAllocate < 1)
            {
                pointsAllocate = 1;
            }

            // Take away the points from remaining experience
            ExperienceRemaining -= pointsAllocate;
            if (ExperienceRemaining < 0)
            {
                pointsAllocate = 0;
            }

            return pointsAllocate;

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

        // Add or Replace Unique Item to Monster
        public void AddOrReplaceUniqueItem(string itemId)
        {
            UniqueItem = itemId;
        }

        // Gets the unique item (if any) from this monster when it dies...
        public Item GetUniqueItem()
        {
            return GetItem(UniqueItem);
        }

        // Drop all the items the monster has
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Drop unique Item
            Item myItem;
            if(!string.IsNullOrWhiteSpace(UniqueItem))
            {
                myItem = GetItem(UniqueItem);
                if (myItem != null)
                {
                    myReturn.Add(myItem);
                }
            }

            // Drop Item from each location
            foreach (string loc in ItemLocationList.GetListCharacter)
            {
                
                Enum.TryParse(loc, true, out ItemLocationEnum locEnum);
                Item _item = RemoveItem(locEnum);
                if(_item != null)
                {
                    myReturn.Add(_item);
                }
                
            }

            return myReturn;
        }

        // Remove Item from a set location
        // Does this by adding a new item of Null to the location
        // This will return the previous item, and put null in its place
        // Returns the item that was at the location
        // Nulls out the location
        public Item RemoveItem(ItemLocationEnum itemLocation)
        {
            return AddItem(itemLocation, null);
        }

        // Add Item
        // Looks up the Item
        // Puts the Item ID as a string in the location slot
        // If item is null, then puts null in the slot
        // Returns the item that was in the location
        public Item AddItem(ItemLocationEnum itemLocation, string itemId)
        {
            Item item = null;
            var _prevItem = "";
            if (itemId != null)
                item = GetItem(itemId);

            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    _prevItem = Head;
                    Head = item?.Id;
                    break;
                case ItemLocationEnum.Necklass:
                    _prevItem = Necklace;
                    Necklace = item?.Id;
                    break;
                case ItemLocationEnum.PrimaryHand:
                    _prevItem = PrimaryHand;
                    PrimaryHand = item?.Id;
                    break;
                case ItemLocationEnum.OffHand:
                    _prevItem = OffHand;
                    OffHand = item?.Id;
                    break;
                case ItemLocationEnum.RightFinger:
                    _prevItem = RightFinger;
                    RightFinger = item?.Id;
                    break;
                case ItemLocationEnum.LeftFinger:
                    _prevItem = LeftFinger;
                    LeftFinger = item?.Id;
                    break;
                case ItemLocationEnum.Feet:
                    _prevItem = Feet;
                    Feet = item?.Id;
                    break;
            }
            return GetItem(_prevItem);
        }

        // Get the Item at a known string location (head, foot etc.)
        public Item GetItem(string itemString)
        {
            return ItemsViewModel.Instance.GetItem(itemString);
        }

        #endregion Items
    }
}