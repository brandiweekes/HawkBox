using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawl.Models
{
    /// <summary>
    /// Enum to specify the different attributes Allowed.
    /// Not specified is considered not initialize and returns unknown
    /// All other attributes have explicted values.
    /// </summary>
    public enum AttributeEnum
    {
        // Not specified
        Unknown = 0,    

        // The speed of the character, impacts movement, and initiative
        Speed = 10,

        // The defense score, to be used for defending against attacks
        Defense = 12,

        // The Attack score to be used when attacking
        Attack = 14,

        // Current Health which is always at or below MaxHealth
        CurrentHealth = 16,

        // The highest value health can go
        MaxHealth = 18,
    }

    /// <summary>
    ///  Helper functions for the AttribureEnum
    /// </summary>
    public static class AttributeList
    {

        /// <summary>
        /// Returns a list of strings of the enum for Attribute
        /// Removes the attributes that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetListItem
        {
            get
            {
                var _list = Enum.GetValues(typeof(AttributeEnum)).Cast<AttributeEnum>()
                    .Where(e => e != AttributeEnum.MaxHealth && e != AttributeEnum.Unknown)
                    .Select(v => v.ToString())
                    .ToList();
                return _list;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Attribute. Removes the unknown.
        /// </summary>
        public static List<string> GetListCharacter
        {
            get
            {
                var _list = Enum.GetValues(typeof(AttributeEnum)).Cast<AttributeEnum>()
                    .Where(e => e != AttributeEnum.Unknown)
                    .Select(v => v.ToString())
                    .ToList();
                return _list;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AttributeEnum ConvertStringToEnum(string value)
        {
            return (AttributeEnum)Enum.Parse(typeof(AttributeEnum), value);
        }
    }
}
