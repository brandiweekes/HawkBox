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
            Alive = true;

            Attribute = new AttributeBase();

            ScaleLevel(Level);
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

            ScaleLevel(Level);
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
        public bool ScaleLevel(int level)
        {
            if(level < 1)
            {
                return false;
            }

            if(level < Level)
            {
                return false;
            }

            if(level > LevelTable.MaxLevel)
            {
                return false;
            }

            Level = level;

            // Set Attributes
            Attribute.Attack = LevelTable.Instance.LevelDetailsList[Level].Attack;
            Attribute.Defense = LevelTable.Instance.LevelDetailsList[Level].Defense;
            Attribute.Speed = LevelTable.Instance.LevelDetailsList[Level].Speed;
            // Roll dice
            Attribute.MaxHealth = HelperEngine.RollDice(Level, HealthDice);
            Attribute.CurrentHealth = Attribute.MaxHealth;
            AttributeString = AttributeBase.GetAttributeString(Attribute);

            return true;
        }

        // Level Up based on XP
        public bool LevelUp()
        {

            // Walk the Level Table descending order
            // Stop when experience is >= experience in the table
            for (var i = LevelTable.Instance.LevelDetailsList.Count - 1; i > 0; i--)
            {
                // Check the Level
                // If the Level is > Experience for the Index, increment the Level.
                if (LevelTable.Instance.LevelDetailsList[i].Experience <= ExperienceTotal)
                {
                    var NewLevel = LevelTable.Instance.LevelDetailsList[i].Level;

                    // When leveling up, the current health is adjusted up by an offset of the MaxHealth, rather than full restore
                    var OldCurrentHealth = Attribute.CurrentHealth;
                    var OldMaxHealth = Attribute.MaxHealth;

                    // Set new Health
                    var NewHealthAddition = HelperEngine.RollDice(NewLevel - Level, HealthDice);

                    // Increment the Max health
                    Attribute.MaxHealth += NewHealthAddition;

                    // Calculate new current health
                    // old max was 10, current health 8, new max is 15 so (15-(10-8)) = current health
                    Attribute.CurrentHealth = (Attribute.MaxHealth - (OldMaxHealth - OldCurrentHealth));

                    // Refresh the Attriburte String
                    AttributeString = AttributeBase.GetAttributeString(Attribute);

                    // Set the new level
                    Level = NewLevel;

                    // Done, exit
                    return true;
                }
            }

            return false;
        }

        // Level up to a number, say Level 3
        public int LevelUpToValue(int value)
        {
            if(value < 1)
            {
                return Level;
            }

            if (value <= Level)
            {
                return Level;
            }

            if (value > LevelTable.MaxLevel)
            {
                value = LevelTable.MaxLevel;
            }

            AddExperience(LevelTable.Instance.LevelDetailsList[value].Experience + 1);

            return Level;
        }

        // Add experience
        public bool AddExperience(int newExperience)
        {
            if(newExperience < 0)
            {
                return false;
            }

            // If Level is Max level, then no need to add experience
            if(Level >= LevelTable.MaxLevel)
            {
                return false;
            }

            ExperienceTotal += newExperience;

            // If experience is higher than the experience at the next level, then level up.
            if (ExperienceTotal >= LevelTable.Instance.LevelDetailsList[Level + 1].Experience)
            {
                return LevelUp();
            }
            return false;
        }

        // Take Damage
        // If the damage received is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept experience from monsters
        public void TakeDamage(int damage)
        {
            if (damage < 1)
            {
                return;
            }

            Attribute.CurrentHealth -= damage;
            if (GetHealthCurrent() <= 0)
            {
                // Death...
                CauseDeath();
            }
        }

        #endregion Basics

        #region GetAttributes
        // Get Attributes

        // Get Attack
        public int GetAttack()
        {
            // Base Attack
            var myReturn = Attribute.Attack;

            // Get Attack bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Attack);

            return myReturn;
        }

        // Get Speed
        public int GetSpeed()
        {
            // Base value
            var myReturn = Attribute.Speed;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Speed);

            return myReturn;
        }

        // Get Defense
        public int GetDefense()
        {
            // Base value
            var myReturn = Attribute.Defense;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Defense);

            return myReturn;
        }

        // Get Max Health
        public int GetHealthMax()
        {
            // Base value
            var myReturn = Attribute.MaxHealth;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.MaxHealth);

            return myReturn;
        }

        // Get Current Health
        public int GetHealthCurrent()
        {
            // Base value
            var myReturn = Attribute.CurrentHealth;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.CurrentHealth);

            return myReturn;
        }

        // Returns the Dice for the item
        // Sword 10, is Sword Dice 10
        public int GetDamageDice()
        {
            var myReturn = 0;

            var myItem = ItemsViewModel.Instance.GetItem(PrimaryHand);
            if (myItem != null)
            {
                // Damage is base damage plus dice of the weapon.  So sword of Damage 10 is d10
                myReturn += myItem.Damage;
            }

            return myReturn;
        }

        // Get the Level based damage
        // Then add the damage for the primary hand item as a Dice Roll
        public int GetDamageRollValue()
        {
            var myReturn = GetLevelBasedDamage();
            var _damage = GetDamageDice();
            if(_damage != 0)
            {
                // Damage is base damage plus dice of the weapon.  So sword of Damage 10 is d10
                myReturn += HelperEngine.RollDice(1, _damage);
            }

            return myReturn;
        }

        #endregion GetAttributes

        #region Items
        // Drop All Items
        // Return a list of items for the pool of items
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Get Item Locations for Characters
            var _locationList = ItemLocationList.GetListCharacter;

            foreach (string loc in _locationList)
            {
                Enum.TryParse(loc, true, out ItemLocationEnum locEnum);
                Item _item = RemoveItem(locEnum);
                myReturn.Add(_item);
            }

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
            var _itemId = "";
            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    _itemId = this.Head;
                    break;
                case ItemLocationEnum.Necklass:
                    _itemId = this.Necklace;
                    break;
                case ItemLocationEnum.PrimaryHand:
                    _itemId = this.PrimaryHand;
                    break;
                case ItemLocationEnum.OffHand:
                    _itemId = this.OffHand;
                    break;
                case ItemLocationEnum.RightFinger:
                    _itemId = this.RightFinger;
                    break;
                case ItemLocationEnum.LeftFinger:
                    _itemId = this.LeftFinger;
                    break;
                case ItemLocationEnum.Feet:
                    _itemId = this.Feet;
                    break;
                default:
                    _itemId = null;
                    break;
            }
            return GetItem(_itemId);
        }

        // Add Item
        // Looks up the Item
        // Puts the Item Id as a string in the location slot
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

        // Walk all the Items on the Character.
        // Add together all Items that modify the Attribute Enum Passed in
        // Return the sum
        public int GetItemBonus(AttributeEnum attributeEnum)
        {
            var myReturn = 0;
            foreach (string loc in ItemLocationList.GetListCharacter)
            {
                Enum.TryParse(loc, true, out ItemLocationEnum locEnum);
                Item item = GetItemByLocation(locEnum);
                if (item.Attribute.Equals(attributeEnum))
                    myReturn += item.Value;
            }
            return myReturn;
        }

        #endregion Items
    }
}