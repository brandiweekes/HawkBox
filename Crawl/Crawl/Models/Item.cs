using Crawl.Controllers;
using Crawl.GameEngine;

namespace Crawl.Models
{
    // The Items that a character can use, a Monster may drop, or may be randomly available.
    // The items are stored in the DB, and during game time a random item is selected.
    // The system supports CRUDi operatoins on the items
    // When in test mode, a test set of items is loaded
    // When in run mode the items from from the database
    // When in online mode, the items come from an api call to a webservice

    // When characters or monsters die, they drop items into the Items Pool for the Battle

    /// <summary>
    /// Item class which holds details of each item. 
    /// Characters uses Items to attack monsters and monsters may drop Items when they are killed.
    /// </summary>
    public class Item : Entity<Item>
    {
        // Range of the item, swords are 1, hats/rings are 0, bows are >1
        public int Range { get; set; }

        // The Damage the Item can do if it is used as a weapon in the primary hand
        public int Damage { get; set; }

        // Enum of the different attributes that the item modifies, Items can only modify one item
        public AttributeEnum Attribute { get; set; }

        // Where the Item goes on the character.  Head, Foot etc.
        public ItemLocationEnum Location { get; set; }

        // The Value item modifies.  So a ring of Health +3, has a Value of 3
        public int Value { get; set; }

        // Flag use to determine it item is unique or not...
        public bool IsUnique { get; set; } = false;

        // Category to identify our group items from server. - our Group no. is 3
        public int Category { get; set; }

        // Inheritated properties
        // Id comes from BaseEntity class
        // Name comes from the Entity class... 
        // Description comes from the Entity class
        // ImageURI comes from the Entity class

        /// <summary>
        /// Default Item contructor
        /// </summary>
        public Item()
        {
            CreateDefaultItem();
        }

        /// <summary>
        /// Create a default item for the instantiation
        /// </summary>
        private void CreateDefaultItem()
        {
            Name = "Item Name";
            Description = "This is Item Description";
            ImageURI = ItemsController.DefaultImageURI;

            Range = 0;
            Value = 1;
            Damage = 1;

            Location = ItemLocationEnum.Unknown;
            Attribute = AttributeEnum.Unknown;
        }

        /// <summary>
        /// Create new Item from Existing Item
        /// </summary>
        /// <param name="data"></param>
        public Item(Item data)
        {
            Update(data);
        }

        /// <summary>
        /// Create Item with set values.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="imageuri"></param>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <param name="damage"></param>
        /// <param name="location"></param>
        /// <param name="attribute"></param>
        /// <param name="isUnique"></param>
        public Item(string name, string description, string imageuri,
            int range, int value, int damage,
            ItemLocationEnum location, AttributeEnum attribute, bool isUnique)
        {
            // Create default, and then override...
            CreateDefaultItem();

            Name = name;
            Description = description;
            ImageURI = imageuri;

            Range = range;
            Value = value;
            Damage = damage;

            Location = location;
            Attribute = attribute;

            IsUnique = isUnique;
        }

        /// <summary>
        /// Update for Item, that will update the fields one by one.
        /// </summary>
        /// <param name="newData"></param>
        public void Update(Item newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Description = newData.Description;
            Value = newData.Value;
            Attribute = newData.Attribute;
            Location = newData.Location;
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            Range = newData.Range;
            Damage = newData.Damage;
            IsUnique = newData.IsUnique;
        }

        /// <summary>
        /// Will update the Item to be stronger...
        /// </summary>
        /// <param name="level"></param>
        public void ScaleLevel(int level)
        {
            var newValue = 1;

            // given level cannot be zero or negative number
            if (level < 1)
            {
                return;
            }

            // if no random mode is turned On, then set value to be level value. 
            // else roll dice and get randome value.
            if (GameGlobals.ForceRollsToNotRandom)
            {
                newValue = level;
            }
            else
            {
                // Add value 1 to level passed in...
                newValue = HelperEngine.RollDice(1, level);
            }

            Value = newValue;
        }

        /// <summary>
        /// used to make item unique on fly.
        /// </summary>
        public void MakeItemUnique()
        {
            if (!IsUnique)
            {
                IsUnique = true;
            }
        }

        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var myReturn = $"Name: {Name} \n" +
                $"Desciption: {Description} \n" +
                $"Image: {ImageURI} \n" +
                $"Attribute: {Attribute.ToString()}\tLocation: {Location.ToString()}\n" +
                $"Range: {Range}\t Damage: {Damage}\tValue: {Value}\tUnique: {IsUnique}";
            return myReturn.Trim();
        }
    }
}