using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Crawl.Models
{
    /// <summary>
    /// Common Attributes of Characters or Monsters. 
    /// </summary>
    public class AttributeBase
    {
        // The speed of the character, impacts movement, and initiative
        public int Speed { get; set; }

        // The defense score, to be used for defending against attacks
        public int Defense { get; set; }

        // The Attack score to be used when attacking
        public int Attack { get; set; }

        // Current Health which is always at or below MaxHealth
        public int CurrentHealth { get; set; }

        // The highest value health can go
        public int MaxHealth { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public AttributeBase()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Defaults are all value 1, and then adjusted by scaling up
        /// </summary>
        private void SetDefaultValues()
        {
            Speed = 1;
            Defense = 1;
            Attack = 1;
            CurrentHealth = 1;
            MaxHealth = 1;
        }

        /// <summary>
        /// Return AttributeBase based on a string as the constructor.
        /// </summary>
        /// <param name="data"></param>
        public AttributeBase(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                SetDefaultValues();
                return;
            }

            var myAttributes = JsonConvert.DeserializeObject<AttributeBase>(data);

            Speed = myAttributes.Speed;
            Defense = myAttributes.Defense;
            Attack = myAttributes.Attack;
            CurrentHealth = myAttributes.CurrentHealth;
            MaxHealth = myAttributes.MaxHealth;
        }

        /// <summary>
        /// Create Attributes based on given parameters.
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="maxHealth"></param>
        /// <param name="currentHealth"></param>
        public AttributeBase(int speed, int attack, int defense, int maxHealth, int currentHealth)
        {
            SetDefaultValues();
            Speed = speed;
            Attack = attack;
            Defense = defense;
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        /// <summary>
        /// Return a formatted string of the AttributeBase
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetAttributeString (AttributeBase data)
        {
            var myString = (JObject)JToken.FromObject(data);

            return myString.ToString();
        }

        /// <summary>
        /// Given a string of attributes, convert them to actual attributes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AttributeBase GetAttributeFromString(string data)
        {
            AttributeBase myResult;

            // Convert the string to json object
            // convert the json object to the class
            // return the class

            // make sure the object is properly formatted json for the object type
            try
            {
                myResult = JsonConvert.DeserializeObject<AttributeBase>(data);
                return myResult;
            }

            catch (Exception)
            {
                // Failed, so fall through to the return of new.
                return new AttributeBase();
            }
        }

        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string.
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var myReturn = $"Attack: {Attack} :: Defense: {Defense} :: Speed: {Speed} :: " +
                $"Current Health: {CurrentHealth} :: Max. Health: {MaxHealth}";
            return myReturn.Trim();
        }
    }
}