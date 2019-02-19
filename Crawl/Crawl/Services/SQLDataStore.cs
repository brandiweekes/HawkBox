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
            App.Database.DropTableAsync<BaseMonster>().Wait();
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<Score>().Wait();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<BaseCharacter>().Wait();
            App.Database.CreateTableAsync<BaseMonster>().Wait();
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
            App.Database.InsertAsync(new BaseCharacter(new Character(
                "3 Eyed", "Predicts future attacks with extra eye.", HawkboxResources.Aliens_Char_1,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Sea Alien", "Small and quick to attack.", HawkboxResources.Aliens_Char_2,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Happy Alien", "Smiling can be dangerous!!", HawkboxResources.Aliens_Char_3,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "8 Arms", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_4,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Grass Hopper", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_5,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Pumpkin Ghost", "Ariel attacks are deadly!!!", HawkboxResources.Aliens_Char_6,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Mixed Horns", "Simple creature with most defense.", HawkboxResources.Aliens_Char_7,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            App.Database.InsertAsync(new BaseCharacter(new Character(
                "Guitar Ghost", "Attacks with sound of red guitar.", HawkboxResources.Aliens_Char_8,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            // Load Monsters
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent A", "desc", HawkboxResources.Monsters_Female_Agent_A,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent B", "desc", HawkboxResources.Monsters_Female_Agent_B,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent C", "desc", HawkboxResources.Monsters_Female_Agent_C,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent D", "desc", HawkboxResources.Monsters_Female_Agent_D,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent E", "desc", HawkboxResources.Monsters_Female_Agent_E,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent F", "desc", HawkboxResources.Monsters_Male_Agent_A,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent G", "desc", HawkboxResources.Monsters_Male_Agent_B,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent H", "desc", HawkboxResources.Monsters_Male_Agent_C,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent I", "desc", HawkboxResources.Monsters_Male_Agent_D,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            App.Database.InsertAsync(new BaseMonster(new Monster(
                "Agent J", "desc", HawkboxResources.Monsters_Male_Agent_E,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            // Load Items
            App.Database.InsertAsync(new Item("Gold Sword", "Sword made of Gold, really expensive looking",
                "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png", 0, 10, 10, ItemLocationEnum.PrimaryHand, AttributeEnum.Defense));

            App.Database.InsertAsync(new Item("Strong Shield", "Enough to hide behind",
                "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png", 0, 10, 0, ItemLocationEnum.OffHand, AttributeEnum.Attack));

            App.Database.InsertAsync(new Item("Bunny Hat", "Pink hat with fluffy ears",
                "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png", 0, 10, -1, ItemLocationEnum.Head, AttributeEnum.Speed));

            // Load Scores
            App.Database.InsertAsync(new Score("Score Name 1", "Description", "Image", false));
            App.Database.InsertAsync(new Score("Score Name 2", "Description", "Image", false));
            App.Database.InsertAsync(new Score("Score Name 3", "Description", "Image", false));
            App.Database.InsertAsync(new Score("Score Name 4", "Description", "Image", false));

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
            var baseCharactersList = App.Database.Table<BaseCharacter>().ToListAsync().Result;
            var list = new List<Character>();
            foreach (var baseCharacter in baseCharactersList)
            {
                list.Add(ConvertToCharacter(baseCharacter));
            }
            return await Task.FromResult(list);
        }
        // Convert BaseCharacter to Character.
        // we store Character data as BaseCharacter.cs in dataset
        // we use Character data as Character.cs in system for displaying data in UI.
        private static Character ConvertToCharacter(BaseCharacter data)
        {
            return new Character(data);
        }

        #endregion Character

        #region Monster
        // Add new monster to database
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            var result = await App.Database.InsertAsync(new BaseMonster(data));
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
        // Update monster in database.
        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var result = await App.Database.UpdateAsync(new BaseMonster(data));
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
        // Delete monster from database.
        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var result = await App.Database.DeleteAsync(new BaseMonster(data));
            if (result == 1)
                return await Task.FromResult(true);
            return await Task.FromResult(false);
        }
        // Get monster from database based on given Id.
        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(ConvertToMonster(App.Database.GetAsync<BaseMonster>(id).Result));
        }
        // Get all monsters from database.
        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var baseMonstersList = App.Database.Table<BaseMonster>().ToListAsync().Result;
            var list = new List<Monster>();
            foreach (var baseMonster in baseMonstersList)
            {
                list.Add(ConvertToMonster(baseMonster));
            }
            return await Task.FromResult(list);
        }
        // Convert BaseMonster to Monster.
        // we store Monster data as BaseMonster.cs in dataset
        // we use Monster data as Monster.cs in system for displaying data in UI.
        private static Monster ConvertToMonster(BaseMonster data)
        {
            return new Monster(data);
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