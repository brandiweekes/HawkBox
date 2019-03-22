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

        #region ServerCalls

        /// <summary>
        /// Get items from web server. input must be either 1, 2, 3 or 100. default is 100.
        /// </summary>
        /// <param name="parameter"></param>
        public async Task<List<Item>> GetItemsFromServer(int parameter = 100)
        {
            // parameter is the item group to request.  1, 2, 3, 100

            // Needs to get items from the server
            // Parse them
            // Then update the database
            // Only update fields on existing items
            // Insert new items
            // Then notify the viewmodel of the change

            // Needs to get items from the server

            var URLComponent = "GetItemList/";

            var DataResult = await HttpClientService.Instance.GetJsonGetAsync(WebGlobals.WebSiteAPIURL + URLComponent + parameter);

            // Parse them
            var myList = ParseJson(DataResult);
            if (myList == null)
            {
                // Error, no results
                return new List<Item>();
            }

            // Then update the database

            // Use a foreach on myList
            foreach (var item in myList)
            {
                // Call to the View Model (that is where the datasource is set, and have it then save
                await ItemsViewModel.Instance.InsertUpdateAsync(item);
            }

            // When foreach is done, call to the items view model to set needs refresh to true, so it can refetch the list...
            ItemsViewModel.Instance.SetNeedsRefresh(true);

            return await Task.FromResult(myList);
        }

        /// <summary>
        /// Asks the server for items based on paramaters.
        /// Number is the number of items to return
        /// Level is the Value max for the items
        /// Random is to have the value random between 1 and the Level
        /// Attribute is a filter to return only items for that attribute, else unknown is used for any
        /// Location is a filter to return only items for that location, else unknown is used for any
        /// </summary>
        /// <param name="number"></param>
        /// <param name="level"></param>
        /// <param name="attribute"></param>
        /// <param name="location"></param>
        /// <param name="random"></param>
        /// <param name="updateDataBase"></param>
        /// <returns></returns>
        public async Task<List<Item>> GetItemsFromGame(int number, int level, AttributeEnum attribute, ItemLocationEnum location, bool random, bool updateDataBase)
        {
            // Needs to get items from the server
            // Parse them
            // Then update the database
            // Only update fields on existing items
            // Insert new items
            // Then notify the viewmodel of the change

            // Needs to get items from the server

            var URLComponent = "GetItemListPost/";


            var dict = new Dictionary<string, string>
            {
                { "Number", number.ToString()},
                { "Level", level.ToString()},
                { "Attribute", ((int)attribute).ToString()},
                { "Location", ((int)location).ToString()},
                { "Random", random.ToString()}

            };

            // Convert parameters to a key value pairs to a json object
            JObject finalContentJson = (JObject)JToken.FromObject(dict);

            // Make a call to the helper.  URL and Parameters
            var DataResult = await HttpClientService.Instance.GetJsonPostAsync(WebGlobals.WebSiteAPIURL + URLComponent, finalContentJson);

            // Parse them
            var myList = ParseJson(DataResult);
            if (myList == null)
            {
                // Error, no results, return empty list.
                return new List<Item>();
            }


            // Then update the database

            // Use a foreach on myList
            if (updateDataBase)
            {
                foreach (var item in myList)
                {
                    // Call to the View Model (that is where the datasource is set, and have it then save
                    await ItemsViewModel.Instance.InsertUpdateAsync(item);
                }

                // When foreach is done, call to the items view model to set needs refresh to true, so it can refetch the list...
                ItemsViewModel.Instance.SetNeedsRefresh(true);
            }

            return await Task.FromResult(myList);
        }


        /// <summary>
        ///  The returned data will be a list of items.  Need to pull that list out
        /// </summary>
        /// <param name="myJsonData"></param>
        /// <returns></returns>
        private List<Item> ParseJson(string myJsonData)
        {
            var myData = new List<Item>();

            try
            {
                JObject json;
                json = JObject.Parse(myJsonData);

                // Data is a List of Items, so need to pull them out one by one...

                var myTempList = json["ItemList"].ToObject<List<JObject>>();

                foreach (var myItem in myTempList)
                {
                    var myTempObject = ConvertFromJson(myItem);
                    if (myTempObject != null)
                    {
                        myData.Add(myTempObject);
                    }
                }

                return myData;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

        }

        /// <summary>
        /// Convert json to Item object. Parse each key value data in json and assign it to Item properties.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private Item ConvertFromJson(JObject json)
        {
            var myData = new Item();

            try
            {
                myData.Name = JsonHelper.GetJsonString(json, "Name");
                myData.Guid = JsonHelper.GetJsonString(json, "Guid");
                myData.Id = myData.Guid;    // Set to be the same as Guid, does not come down from server, but needed for DB

                myData.Description = JsonHelper.GetJsonString(json, "Description");
                myData.ImageURI = JsonHelper.GetJsonString(json, "ImageURI");

                myData.Value = JsonHelper.GetJsonInteger(json, "Value");
                myData.Range = JsonHelper.GetJsonInteger(json, "Range");
                myData.Damage = JsonHelper.GetJsonInteger(json, "Damage");

                myData.Location = (ItemLocationEnum)JsonHelper.GetJsonInteger(json, "Location");
                myData.Attribute = (AttributeEnum)JsonHelper.GetJsonInteger(json, "Attribute");

                myData.Category = JsonHelper.GetJsonInteger(json, "Category");

                if(myData.Category == 3)
                {
                    myData.IsUnique = true;
                }

            }

            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

            return myData;
        }
        #endregion ServerCalls
    }
}
