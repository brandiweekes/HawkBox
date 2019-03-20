using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Services
{
    public sealed class SQLDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static SQLDataStore _instance;

        public static SQLDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLDataStore();
                }
                return _instance;
            }
        }

        private SQLDataStore()
        {
            InitializeDatabaseNewTables();
        }

        public void InitializeDatabaseNewTables()
        {
            // Delete the tables
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitializeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();

        }

        // Delete the Database Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Character>().Wait();
            App.Database.DropTableAsync<Monster>().Wait();
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<Score>().Wait();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Character>().Wait();
            App.Database.CreateTableAsync<Monster>().Wait();
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        private void InitializeSeedData()
        {
            // Load Characters
            App.Database.InsertAsync(new Character(
                "3 Eyed", "Predicts future attacks with extra eye.", HawkboxResources.Aliens_Char_1,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            App.Database.InsertAsync(new Character(
                "Sea Alien", "Small and quick to attack.", HawkboxResources.Aliens_Char_2,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            App.Database.InsertAsync(new Character(
                "Happy Alien", "Smiling can be dangerous!!", HawkboxResources.Aliens_Char_3,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            App.Database.InsertAsync(new Character(
                "8 Arms", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_4,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            App.Database.InsertAsync(new Character(
                "Grass Hopper", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_5,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            App.Database.InsertAsync(new Character(
                "Pumpkin Ghost", "Aerial attacks are deadly!!!", HawkboxResources.Aliens_Char_6,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            App.Database.InsertAsync(new Character(
                "Mixed Horns", "Simple creature with most defense.", HawkboxResources.Aliens_Char_7,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            App.Database.InsertAsync(new Character(
                "Guitar Ghost", "Attacks with sound of red guitar.", HawkboxResources.Aliens_Char_8,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            // Load Monsters
            App.Database.InsertAsync(new Monster(
                "Agent A", "desc", HawkboxResources.Monsters_Female_Agent_A,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent B", "desc", HawkboxResources.Monsters_Female_Agent_B,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent C", "desc", HawkboxResources.Monsters_Female_Agent_C,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent D", "desc", HawkboxResources.Monsters_Female_Agent_D,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent E", "desc", HawkboxResources.Monsters_Female_Agent_E,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent F", "desc", HawkboxResources.Monsters_Male_Agent_A,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent G", "desc", HawkboxResources.Monsters_Male_Agent_B,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent H", "desc", HawkboxResources.Monsters_Male_Agent_C,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent I", "desc", HawkboxResources.Monsters_Male_Agent_D,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));
            App.Database.InsertAsync(new Monster(
                "Agent J", "desc", HawkboxResources.Monsters_Male_Agent_E,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            // Load Items
            App.Database.InsertAsync(new Item("Gold Sword", "Sword made of Gold, really expensive looking",
                "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png", 0, 10, 10, ItemLocationEnum.PrimaryHand, AttributeEnum.Defense, true));

            App.Database.InsertAsync(new Item("Strong Shield", "Enough to hide behind",
                "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png", 0, 10, 0, ItemLocationEnum.OffHand, AttributeEnum.Attack, true));

            App.Database.InsertAsync(new Item("Bunny Hat", "Pink hat with fluffy ears",
                "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png", 0, 10, -1, ItemLocationEnum.Head, AttributeEnum.Speed, true));

            // Load Scores
            App.Database.InsertAsync(new Score("Score Name 1", "Description", false));
            App.Database.InsertAsync(new Score("Score Name 2", "Description", false));
            App.Database.InsertAsync(new Score("Score Name 3", "Description", false));
            App.Database.InsertAsync(new Score("Score Name 4", "Description", false));

        }

        #region Item
        // Item

        // Add InsertUpdateAsync_Item Method

        // Check to see if the item exists
        // Add your code here.

        // If it does not exist, then Insert it into the DB
        // Add your code here.
        // return true;

        // If it does exist, Update it into the DB
        // Add your code here
        // return true;

        // If you got to here then return false;

        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {
            // Implement
            var _count = App.Database.Table<Item>().Where(i => i.Id == data.Id).CountAsync().Result;
            if(_count == 0)
            {
                return await AddAsync_Item(data);
            }
            else
            {
                return await UpdateAsync_Item(data);
            }

        }

        // Add new Item into database
        public async Task<bool> AddAsync_Item(Item data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Update Item details
        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Delete Item from database based on given Id
        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Load an Item based on given Id from database
        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(App.Database.GetAsync<Item>(id).Result);
        }

        // Load all Items from database
        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(App.Database.Table<Item>().ToListAsync().Result);
        }
        #endregion Item

        #region Character

        /// <summary>
        /// Add Character in DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync_Character(Character data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        /// <summary>
        /// Update Character in DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        /// <summary>
        /// Delete Character in DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        /// <summary>
        /// Get Character from DB based on given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(App.Database.GetAsync<Character>(id).Result);
        }

        /// <summary>
        /// Get all Characters from DB
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var charactersList = App.Database.Table<Character>().ToListAsync().Result;
            var list = new List<Character>();
            foreach (var character in charactersList)
            {
                list.Add(character);
            }
            return await Task.FromResult(list);
        }

        #endregion Character

        #region Monster
        // Add new monster to database
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
        // Update monster in database.
        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
        // Delete monster from database.
        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
        // Get monster from database based on given Id.
        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(App.Database.GetAsync<Monster>(id).Result);
        }
        // Get all monsters from database.
        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var monstersList = App.Database.Table<Monster>().ToListAsync().Result;
            var list = new List<Monster>();
            foreach (var monster in monstersList)
            {
                list.Add(monster);
            }
            return await Task.FromResult(list);
        }

        #endregion Monster

        #region Score
        // Add new Score to database
        public async Task<bool> AddAsync_Score(Score data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Update score details in database
        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Delete Score details in database
        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Fetch score from database based in given Id
        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(App.Database.GetAsync<Score>(id).Result);
        }

        // Fetch all scores from database
        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(App.Database.Table<Score>().ToListAsync().Result);

        }

        #endregion Score
    }
}