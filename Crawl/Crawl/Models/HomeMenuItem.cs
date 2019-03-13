using System;
using System.Collections.Generic;
using System.Text;

namespace Crawl.Models
{
    /// <summary>
    /// Menu items that are displayed in Master Page in MasterDetailsPage
    /// </summary>
    public enum MenuItemType
    {
        Home,
        Items,
        Aliens,
        Agents,
        Score,
        About
    }

    /// <summary>
    /// Wrapper class for Menu items.
    /// </summary>
    class HomeMenuItem
    {
        // Menu type
        public MenuItemType Id { get; set; }

        // Menu title
        public string Title { get; set; }
    }
}
