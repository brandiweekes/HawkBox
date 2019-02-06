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
            App.Database.DropTableAsync<BaseCharacter>().Wait();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<BaseCharacter>().Wait();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            CharactersViewModel.Instance.SetNeedsRefresh(true);
        }

        private void InitializeSeedData()
        {
            // Load Characters
            App.Database.InsertAsync(new BaseCharacter(new Character(
                "3 Eyed", "Predicts future attacks with extra eye.", "http://gdurl.com/RxRK",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Sea Alien", "Small and quick to attack.", "http://gdurl.com/dgT5",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Happy Alien", "Smiling can be dangerous!!", "http://gdurl.com/NvcO",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "8 Arms", "Multiple arms makes it hard to attack.", "http://gdurl.com/fxM0",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Grass Hopper", "Multiple arms makes it hard to attack.", "http://gdurl.com/c2iZ",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Pumpkin Ghost", "Ariel attacks are deadly!!!", "http://gdurl.com/HSHv",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Mixed Horns", "Simple creature with most defense.", "http://gdurl.com/IGNK",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Guitar Ghost", "Attacks with sound of red guitar.", "http://gdurl.com/O6wJ",
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

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

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            // Implement

            return false;
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            // Implement

            return false;
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            // Implement

            return false;
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            // Implement
            return null;
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            // Implement
            return null;
        }
        #endregion Item

        #region Character

        // Convert to BaseCharacter and then add it
        public async Task<bool> AddAsync_Character(Character data)
        {
            var result = await App.Database.InsertAsync(new BaseCharacter(data));
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Convert to BaseCharacter and then update it
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var result = await App.Database.UpdateAsync(new BaseCharacter(data));
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Pass in the character and convert to Character to then delete it
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var result = await App.Database.DeleteAsync(new BaseCharacter(data));
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }

        // Get the Character Base, and Load it back as Character
        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(ConvertToCharacter(App.Database.GetAsync<BaseCharacter>(id).Result));
        }

        // Load each character as the base character, 
        // Then then convert the list to characters to push up to the view model
        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var baseCharactersList = App.Database.Table<Character>().ToListAsync().Result;
            var list = new List<Character>();
            foreach (var baseCharacter in baseCharactersList)
            {
                list.Add(ConvertToCharacter(baseCharacter));
            }
            return await Task.FromResult(list);
        }

        private static Character ConvertToCharacter(BaseCharacter data)
        {
            return new Character(data);
        }

        #endregion Character

        #region Monster
        //Monster
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            // Implement
            return false;
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            // Implement
            return false;
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            // Implement
            return false;
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            // Implement
            return null;
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            // Implement
            return null;
        }

        #endregion Monster

        #region Score
        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            // Implement
            return null;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            // Implement
            return null;

        }

        #endregion Score
    }
}