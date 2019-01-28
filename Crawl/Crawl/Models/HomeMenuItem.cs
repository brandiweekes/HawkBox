using System;
using System.Collections.Generic;
using System.Text;

namespace Crawl.Models
{
    public enum MenuItemType
    {
        Home,
        Items,
        Aliens,
        Agents,
        Score,
        About
    }

    class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
