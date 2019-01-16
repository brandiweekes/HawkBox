using System;
using System.Collections.Generic;
using Crawl.Services;
using Crawl.Models;
using Crawl.ViewModels;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Crawl.Controllers
{

    public class ItemsController
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ItemsController _instance;

        public static ItemsController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemsController();
                }
                return _instance;
            }
        }

        // Return the Default Image URI for the Local Image for an Item.
        public static string DefaultImageURI = "Item.png";

    }
}
