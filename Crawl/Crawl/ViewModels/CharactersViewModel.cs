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

        public CharactersViewModel()
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
                await AddAsync(data);
            });

            MessagingCenter.Subscribe<CharacterEditPage, Character>(this, "EditCharacter", async (obj, data) =>
            {
                await UpdateAsync(data);
            });

            #endregion Messages

        }

        #region Refresh

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

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

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

        /**
         * Force Data Refresh
         *  -- Used  when DataSore is toggled between Mock and SQL. Check <see cref="Services.MasterDataStore"/>
         */
        public void ForceDataRefresh()
        {
            LoadCharactersCommand.Execute(null);
        }

        #endregion

        #region DataOperations

        public async Task<bool> AddAsync(Character data)
        {
            Dataset.Add(data);
            return await DataStore.AddAsync_Character(data);
        }

        public async Task<bool> DeleteAsync(Character data)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            Dataset.Remove(myData);
            return await DataStore.DeleteAsync_Character(data);
        }

        public async Task<bool> UpdateAsync(Character data)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
                return await Task.FromResult(false);

            myData.Update(data);
            SetNeedsRefresh(true);
            return await DataStore.UpdateAsync_Character(data);
        }

        // Call to database to ensure most recent
        public async Task<Character> GetAsync(string id)
        {
            return await DataStore.GetAsync_Character(id);
        }

        #endregion DataOperations

    }
}