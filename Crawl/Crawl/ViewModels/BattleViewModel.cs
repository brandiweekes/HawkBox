using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Crawl.Models;
using Crawl.Views;
using System.Linq;
using Crawl.Controllers;
using Crawl.GameEngine;
using Crawl.Views.Battle;

namespace Crawl.ViewModels
{
    public class BattleViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static BattleViewModel _instance;

        public static BattleViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleViewModel();
                }
                return _instance;
            }
        }

        // Hold a copy of the Battle Engine
        public BattleEngine BattleEngine;

        // The Character List ot interact with
        // Class for the SelectedCharacters
        public ObservableCollection<Character> SelectedCharacters { get; set; }

        // Class for the AvailableCharacters
        public ObservableCollection<Character> AvailableCharacters { get; set; }

        public ObservableCollection<Character> RemainingCharacters { get; set; }

        public ObservableCollection<Item> AvailableItems { get; set; }

        public Character pickedCharacter; 

        // Load the Data command
        public Command LoadDataCommand { get; set; }

        public Command LoadItemsCommand { get; set; }

        // Flag to check if the data needs refreshing
        private bool _needsRefresh;

        private BattleViewModel()
        {
            Title = "Battle";

            SelectedCharacters = new ObservableCollection<Character>();
            AvailableCharacters = new ObservableCollection<Character>();
            RemainingCharacters = new ObservableCollection<Character>();
            AvailableItems = new ObservableCollection<Item>();

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            BattleEngine = new BattleEngine();
            pickedCharacter = new Character();

            // Load Data
            ExecuteLoadDataCommand().GetAwaiter().GetResult();

            /*MessagingCenter.Subscribe<BattleCharacterSelectPage, Character>(this, "AddSelectedCharacter", async (obj, data) =>
            {
                SelectedListAdd(data);
            });

            MessagingCenter.Subscribe<BattleCharacterSelectPage, Character>(this, "RemoveSelectedCharacter", async (obj, data) =>
            {
                SelectedListRemove(data);
            });*/
        }

        /// <summary>
        /// Call to the Engine to Start the Battle
        /// </summary>
        public void StartBattle()
        {
            BattleEngine.StartBattle(false);
        }

        /// <summary>
        /// Call to the Engine to End the Battle
        /// </summary>
        public void EndBattle()
        {
            BattleEngine.EndBattle();
        }

        /// <summary>
        /// Call to the Engine to Start the First Round
        /// </summary>
        public void StartRound()
        {
            BattleEngine.StartRound();
        }

        /// <summary>
        /// End Round.
        /// </summary>
        public void EndRound()
        {
            BattleEngine.EndRound();
        }

        /// <summary>
        /// Load the Characters from the Selected List into the Battle Engine
        /// Making a copy of the character.
        /// </summary>
        public void LoadCharacters()
        {
            foreach (var data in SelectedCharacters)
            {
                BattleEngine.CharacterList.Add(new Character(data));
            }

        }

        /// <summary>
        /// Call to the engine for the NextRound to Happen
        /// </summary>
        public void RoundNextTurn()
        {
            BattleEngine.RoundNextTurn();
        }

        /// <summary>
        /// Call to the Engine for a New Round to Happen
        /// </summary>
        public void NewRound()
        {
            BattleEngine.NewRound();
        }

        #region DataOperations
        // Call to database operation for delete
        public bool SelectedListRemove(Character data)
        {
            SelectedCharacters.Remove(data);
            return true;
        }

        // Call to database operation for add
        public bool SelectedListAdd(Character data)
        {
            SelectedCharacters.Add(data);
            return true;
        }

        // Call to database to ensure most recent
        public Character Get(string id)
        {
            var myData = SelectedCharacters.FirstOrDefault(arg => arg.Id == id);
            if (myData == null)
            {
                return null;
            }

            return myData;

        }
        #endregion DataOperations


        // Clear current lists so they can get rebuilt
        public async void ClearCharacterLists()
        {
            AvailableCharacters.Clear();
            SelectedCharacters.Clear();

            await ExecuteLoadDataCommand();
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        public async void AddItemsToPoolForTesting()
        {
            var items = await DataStore.GetAllAsync_Item(false);

            foreach (var i in items)
            {
                BattleEngine.ItemPool.Add(i);
            }

        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        // Command that Loads the Data
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                // SelectedCharacters, no need to change them.

                // Reload the Character List from the Character View Moel
                /*AvailableCharacters.Clear();
                var availableCharacters = CharactersViewModel.Instance.Dataset;
                foreach (var data in availableCharacters)
                {
                    AvailableCharacters.Add(data);
                }*/

                RemainingCharacters.Clear();
                var remaining = BattleEngine.CharacterList;
                foreach(var ch in remaining)
                {
                    RemainingCharacters.Add(ch);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }

        // Command that Loads the Data
        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                // SelectedCharacters, no need to change them.

                // Reload the Character List from the Character View Moel
                /*AvailableCharacters.Clear();
                var availableCharacters = CharactersViewModel.Instance.Dataset;
                foreach (var data in availableCharacters)
                {
                    AvailableCharacters.Add(data);
                }*/
                
                
                AvailableItems.Clear();
                var available = BattleEngine.ItemPool;
                foreach (var ch in available)
                {
                    AvailableItems.Add(ch);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }

        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }
    }
}
