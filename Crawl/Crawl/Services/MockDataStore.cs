using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Services
{
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>();
        private List<Character> _characterDataset = new List<Character>();
        private List<BaseMonster> _monsterDataset = new List<BaseMonster>();
        private List<Score> _scoreDataset = new List<Score>();

        private MockDataStore()
        {
            InitializeSeedData();
        }

        private void InitializeSeedData()
        {

            // Load Items
            _itemDataset.Add(new Item("Anti-Gravity Shoes", "These shoes allow the wearer to hover at any given height. When not in use, they revert to their casual form as an ordinary black leather office shoes.",
                "https://vignette.wikia.nocookie.net/finders-keepers-roblox/images/2/2b/Rocket_Boots.png/revision/latest?cb=20181213142618", 0, 10, 10, ItemLocationEnum.Feet, AttributeEnum.Speed, true));

            _itemDataset.Add(new Item("Icer", "A small freeze pistol having no barrel but has a slit where a bolt of green electricity-like energy shoots out that can freeze, or ice, enemies.",
                "http://www.pngmart.com/files/4/Green-Light-PNG-Photos.png", 0, 10, 0, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack, true));

            _itemDataset.Add(new Item("Spectral Trail Scanner", "Used to scan people and distinguish a human from an alien via X-ray and heat signatures. It can also list down that organism's components, ailments, and species.",
                "http://www.clker.com/cliparts/N/D/U/r/g/M/radar-md.png", 0, 10, -1, ItemLocationEnum.Necklass, AttributeEnum.Defense, true));

            // Load Characters
            _characterDataset.Add(new Character(
                "3 Eyed", "Predicts future attacks with extra eye.", HawkboxResources.Aliens_Char_1,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            _characterDataset.Add(new Character(
                "Sea Alien", "Small and quick to attack.", HawkboxResources.Aliens_Char_2,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            _characterDataset.Add(new Character(
                "Happy Alien", "Smiling can be dangerous!!", HawkboxResources.Aliens_Char_3,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            _characterDataset.Add(new Character(
                "8 Arms", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_4,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            _characterDataset.Add(new Character(
                "Grass Hopper", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_5,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            _characterDataset.Add(new Character(
                "Pumpkin Ghost", "Aerial attacks are deadly!!!", HawkboxResources.Aliens_Char_6,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            _characterDataset.Add(new Character(
                "Mixed Horns", "Simple creature with most defense.", HawkboxResources.Aliens_Char_7,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));

            _characterDataset.Add(new Character(
                "Guitar Ghost", "Attacks with sound of red guitar.", HawkboxResources.Aliens_Char_8,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger"));


            // Load Monsters

            _monsterDataset.Add(new BaseMonster(new Monster("Agent L",
                "Elle is the chief scientific officer and an assistant to Zed.",
                HawkboxResources.Monsters_Female_Agent_A,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent M",
                "After an emissary from a powerful alien government is killed Agent M must find the killer and the mole in the MiB organization.",
                HawkboxResources.Monsters_Female_Agent_B,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent O",
                "She is a veteran agent, becoming chief of the MiB after Zed's passing, who was a secretary back in the 1960s to Zed's predeceesor Chief X.",
                HawkboxResources.Monsters_Female_Agent_C,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent E",
                "A friend of Kay who works for The Agency, a special branch of MiB which operates in Hollywood, helping out the careers of alien actors who get to appear on the big screen in their real extraterrestrial forms while pretending to be disguised.",
                HawkboxResources.Monsters_Female_Agent_D,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent X",
                "Chief X was the head of MiB prior to the arrival of Zed, who apparently replaced X as Director of MiB at some point prior or during the incident related to the Light of Zartha which occurred in 1978",
                HawkboxResources.Monsters_Female_Agent_E,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent D",
                "A founding member of the MiB, and partner to Agent K. He is an old veteran agent, and has troubles keeping up.",
                HawkboxResources.Monsters_Male_Agent_A,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent T",
                "Tee was a marine for six years before joining the MiB. He was brought in to be another partner to Agent J after the neuralyzing of Agent K, among his other previous partners.",
                HawkboxResources.Monsters_Male_Agent_B,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent J",
                "An agent of the MiB, after being recruited by Agent K. Jay is energetic, and tries to bring life and emotion back to the bland organization.",
                HawkboxResources.Monsters_Male_Agent_C,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent K",
                "A top agent and a founder of MiB, Kay is a character who is extremely respected, and after working at MiB for nearly 40 years, he's very stoic and shows nearly no emotion",
                HawkboxResources.Monsters_Male_Agent_D,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));
            _monsterDataset.Add(new BaseMonster(new Monster("Agent Z",
                "Chief Zed was one of the founding members of the MiB, and the former Chief/Head of the MiB in all media forms.",
                HawkboxResources.Monsters_Male_Agent_E,
                1, 10, true, 10, 10, 10, 10, 10,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            // Load Scores
            _scoreDataset.Add(new Score("Score Name 1", "Description", false));
            _scoreDataset.Add(new Score("Score Name 2", "Description", false));
            _scoreDataset.Add(new Score("Score Name 3", "Description", false));
            _scoreDataset.Add(new Score("Score Name 4", "Description", false));
        }

        #region Item
        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                _itemDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                await AddAsync_Item(data);
                return true;
            }

            return false;
        }

        // Add new item to dataset.
        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        // Update item data in dataset.
        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        // Delete a item from dataset.
        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        // Get item data from dataset based in given Id.
        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        // Get all items from dataset.
        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }

        #endregion Item

        #region Character
        // Add new character to dataset.
        public async Task<bool> AddAsync_Character(Character data)
        {
            _characterDataset.Add(data);

            return await Task.FromResult(true);
        }

        // Update character data in dataset.
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            // Check if given character exists in dataset
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
                return await Task.FromResult(false);
            // Update character
            myData.Update(data);

            return await Task.FromResult(true);
        }

        // Delete a character from dataset.
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            // Check if given character exists in dataset
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        // Get character data from dataset based in given Id.
        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(_characterDataset.FirstOrDefault(s => s.Id == id));
        }

        // Get all characters from dataset.
        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var list = new List<Character>();
            foreach (var character in _characterDataset)
            {
                list.Add(character);
            }
            return await Task.FromResult(list);
        }

        #endregion Character

        #region Monster
        // Add new Monster to dataset
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            _monsterDataset.Add(new BaseMonster(data));
            return await Task.FromResult(true);
        }

        // Update existing monster in dataset
        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            // Check if given monster exists in dataset
            var myData = _monsterDataset.FirstOrDefault(args => args.Id == data.Id);
            if (myData == null)
                return await Task.FromResult(false);
            myData.Update(data);
            return await Task.FromResult(true);
        }

        // Delete Monster in dataset
        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            // Check if given monster exists in dataset
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _monsterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        // Get Monster from dataset based on given Id
        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(ConvertToMonster(_monsterDataset.FirstOrDefault(s => s.Id == id)));
        }

        // Get all Monsters from dataset
        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var list = new List<Monster>();
            foreach (var baseMonster in _monsterDataset)
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

        // Add new score to dataset.
        public async Task<bool> AddAsync_Score(Score data)
        {
            _scoreDataset.Add(data);
            return await Task.FromResult(true);
        }

        // Update a score from dataset.
        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(args => args.Id == data.Id);
            if (myData == null)
                return await Task.FromResult(false);
            myData.Update(data);
            return await Task.FromResult(true);
        }

        // Delete a score from dataset.
        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _scoreDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        // Get score data from dataset based in given Id.
        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(_scoreDataset.FirstOrDefault(s => s.Id == id));
        }

        // Get all scores from dataset.
        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(_scoreDataset);
        }
        #endregion Score
    }
}