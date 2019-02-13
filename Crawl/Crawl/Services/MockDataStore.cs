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
        private List<BaseCharacter> _characterDataset = new List<BaseCharacter>();
        private List<BaseMonster> _monsterDataset = new List<BaseMonster>();
        private List<Score> _scoreDataset = new List<Score>();

        private MockDataStore()
        {
            InitializeSeedData();
        }

        private void InitializeSeedData()
        {

            // Load Items
            _itemDataset.Add(new Item("Gold Sword", "Sword made of Gold, really expensive looking",
                "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png", 0, 10, 10, ItemLocationEnum.PrimaryHand, AttributeEnum.Defense));

            _itemDataset.Add(new Item("Strong Shield", "Enough to hide behind",
                "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png", 0, 10, 0, ItemLocationEnum.OffHand, AttributeEnum.Attack));

            _itemDataset.Add(new Item("Bunny Hat", "Pink hat with fluffy ears",
                "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png", 0, 10, -1, ItemLocationEnum.Head, AttributeEnum.Speed));

            // Load Characters
            _characterDataset.Add(new BaseCharacter(new Character(
                "3 Eyed", "Predicts future attacks with extra eye.", HawkboxResources.Aliens_Char_1,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            _characterDataset.Add(new BaseCharacter(new Character(
                "Sea Alien", "Small and quick to attack.", HawkboxResources.Aliens_Char_2,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            _characterDataset.Add(new BaseCharacter(new Character(
                "Happy Alien", "Smiling can be dangerous!!", HawkboxResources.Aliens_Char_3,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            _characterDataset.Add(new BaseCharacter(new Character(
                "8 Arms", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_4,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            _characterDataset.Add(new BaseCharacter(new Character(
                "Grass Hopper", "Multiple arms makes it hard to attack.", HawkboxResources.Aliens_Char_5,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            _characterDataset.Add(new BaseCharacter(new Character(
                "Pumpkin Ghost", "Ariel attacks are deadly!!!", HawkboxResources.Aliens_Char_6,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            _characterDataset.Add(new BaseCharacter(new Character(
                "Mixed Horns", "Simple creature with most defense.", HawkboxResources.Aliens_Char_7,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));

            _characterDataset.Add(new BaseCharacter(new Character(
                "Guitar Ghost", "Attacks with sound of red guitar.", HawkboxResources.Aliens_Char_8,
                1, 10, true, 10, 10, 10, 20, 20,
                "head", "feet", "necklace", "primaryHand", "offHand", "rightFinger", "leftFinger")));


            // Implement Monsters

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

            // Implement Scores
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

        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

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

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }

        #endregion Item

        #region Character
        // Add new character to dataset.
        public async Task<bool> AddAsync_Character(Character data)
        {
            _characterDataset.Add(new BaseCharacter(data));

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
            return await Task.FromResult(ConvertToCharacter(_characterDataset.FirstOrDefault(s => s.Id == id)));
        }

        // Get all characters from dataset.
        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var list = new List<Character>();
            foreach (var baseCharacter in _characterDataset)
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