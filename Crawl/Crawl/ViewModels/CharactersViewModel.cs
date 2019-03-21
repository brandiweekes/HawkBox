using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Crawl.Models;
using Crawl.Views;
using System.Linq;

namespace Crawl.ViewModels
{
    public class CharactersViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static CharactersViewModel _instance;

        public static CharactersViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharactersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> Dataset { get; set; }

        public Command LoadCharactersCommand { get; set; }

        private bool _needsRefresh;

        /// <summary>
        /// initialize properties and define Messages
        /// </summary>
        private CharactersViewModel()
        {
            Dataset = new ObservableCollection<Character>();
            LoadCharactersCommand = new Command(async () => await ExecuteLoadDataCommand());

            // Implement 
            #region Messages
            MessagingCenter.Subscribe<CharacterDeletePage, Character>(this, "DeleteCharacter", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<CharacterNewPage, Character>(this, "AddCharacter", async (obj, data) =>
            {
                var newCharacter = data as Character;
                await AddAsync(newCharacter);
            });

            MessagingCenter.Subscribe<CharacterEditPage, Character>(this, "EditCharacter", async (obj, data) =>
            {
                var newCharacter = data as Character;
                await UpdateAsync(newCharacter);
            });

            #endregion Messages

        }

        #region Refresh

        /// <summary>
        /// Return True if a refresh is needed
        /// It sets the refresh flag to false
        /// </summary>
        /// <returns></returns>
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the need to refresh
        /// </summary>
        /// <param name="value"></param>
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        /// <summary>
        /// reset data and load data from datastore
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var characters = await DataStore.GetAllAsync_Character(true);
                foreach (var character in characters)
                {
                    Dataset.Add(character);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                SetNeedsRefresh(false);
            }
        }

        /// <summary>
        /// Force Data Refresh
        /// Used when DataSore is toggled between Mock and SQL.Check<see cref="Services.MasterDataStore"/>
        /// </summary>
        public void ForceDataRefresh()
        {
            LoadCharactersCommand.Execute(null);
        }

        #endregion

        #region DataOperations

        /// <summary>
        /// add new character
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Character data)
        {
            Dataset.Add(data);
            return await DataStore.AddAsync_Character(data);
        }

        /// <summary>
        /// Deletes a requested Character
        /// Checks if exists in list then removes if found
        /// </summary>
        /// <param name="data">Character to be deleted</param>
        /// <returns>true if success</returns>
        public async Task<bool> DeleteAsync(Character data)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            Dataset.Remove(myData);
            var myReturn = await DataStore.DeleteAsync_Character(data);
            return myReturn;
        }

        /// <summary>
        /// update character
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Character data)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
                return await Task.FromResult(false);

            myData.Update(data);
            SetNeedsRefresh(true);
            return await DataStore.UpdateAsync_Character(data);
        }

        /// <summary>
        /// Call to database to ensure most recent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Character> GetAsync(string id)
        {
            return await DataStore.GetAsync_Character(id);
        }

        #endregion DataOperations

    }
}