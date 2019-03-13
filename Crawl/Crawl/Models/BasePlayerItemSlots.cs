using Crawl.ViewModels;
using System.Collections.Generic;
using System.Reflection;

namespace Crawl.Models
{
    // Folding ItemSlots into the overall class inheritance, to show approach.  
    // C# does not support multiple inheritance
    // Could use simulated by using a pattern of interfaces, but for this, just doing it the simple way...
    /// <summary>
    /// Item slots class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePlayerItemSlots<T> : Entity<T>
    {
        // Item is a string referencing the database table
        public string Head { get; set; }

        // Feet is a string referencing the database table
        public string Feet { get; set; }

        // Necklace is a string referencing the database table
        public string Necklace { get; set; }

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; }

        // Offhand is a string referencing the database table
        public string OffHand { get; set; }

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; }

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; }

        /// <summary>
        /// This uses reflection, to get the property from a string
        /// Then based on the property, it gets the value which will be the string pointing to the item
        /// Then it calls to the view model who has the list of items, and asks for it
        /// then it returns the formatted string for the Item, and Value.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        private string FormatOutputSlot(string slot)
        {
            var myReturn = slot + ": ";
            var _value = GetType().GetProperty(slot).GetValue(this);
            if (_value == null)
            {
                myReturn += "None";
                return myReturn;
            }

            var myData = ItemsViewModel.Instance.GetItem(_value.ToString());
            if (myData == null)
            {
                myReturn += "None";
            }
            else
            {
                myReturn += myData.Value.ToString();
            }
            return myReturn;
        }

        /// <summary>
        /// Get item slots in as a string. easier to display.
        /// </summary>
        /// <returns></returns>
        public string ItemSlotsFormatOutput()
        {
            var myReturn = $"{FormatOutputSlot("Head")} :: " +
                $"{FormatOutputSlot("Necklass")} :: " +
                $"{FormatOutputSlot("PrimaryHand")} :: " +
                $"{FormatOutputSlot("OffHand")} :: " +
                $"{FormatOutputSlot("RightFinger")} :: " +
                $"{FormatOutputSlot("LeftFinger")} :: " +
                $"{FormatOutputSlot("Feet")}";

            return myReturn.Trim();
        }
    }
}
