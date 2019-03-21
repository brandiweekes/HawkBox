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
    public class MonstersViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MonstersViewModel _instance;

        public static MonstersViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonstersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Monster> Dataset { get; set; }
        public Command LoadMonstersCommand { get; set; }

        private bool _needsRefresh;

        /// <summary>
        /// initialize properties and define Messages
        /// </summary>
        private MonstersViewModel()
        {
            Dataset = new ObservableCollection<Monster>();
            LoadMonstersCommand = new Command(async () => await ExecuteLoadDataCommand());

            #region Messages
            MessagingCenter.Subscribe<MonsterDeletePage, Monster>(this, "DeleteMonster", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<MonsterNewPage, Monster>(this, "AddMonster", async (obj, data) =>
            {
                var newMonster = data as Monster;
                await AddAsync(newMonster);
            });

            MessagingCenter.Subscribe<MonsterEditPage, Monster>(this, "EditMonster", async (obj, data) =>
            {
                var newMonster = data as Monster;
                await UpdateAsync(newMonster);
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
        /// reset dataset and load data from datastore
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
                var monsters = await DataStore.GetAllAsync_Monster(true);
                foreach (var monster in monsters)
                {
                    Dataset.Add(monster);
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
        /// Used  when DataSore is toggled between Mock and SQL. Check <see cref="Services.MasterDataStore"/>
        /// </summary>
        public void ForceDataRefresh()
        {
            LoadMonstersCommand.Execute(null);
        }
        #endregion Refresh

        #region DataOperations

        /// <summary>
        /// Add new monster data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Monster data)
        {
            Dataset.Add(data);
            return await DataStore.AddAsync_Monster(data);
        }

        /// <summary>
        /// check if monster exists in DataStore
        /// if true, delete the monster
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Monster data)
        {
            var myData = Dataset.FirstOrDefault(s => s.Id == data.Id);
            Dataset.Remove(myData);
            return await DataStore.DeleteAsync_Monster(data);
        }

        /// <summary>
        /// update monster
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Monster data)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
                return await Task.FromResult(false);

            myData.Update(data);
            SetNeedsRefresh(true);
            return await DataStore.UpdateAsync_Monster(data);
        }

        /// <summary>
        /// Call to database to ensure most recent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Monster> GetAsync(string id)
        {
            return await DataStore.GetAsync_Monster(id);
        }

        #endregion DataOperations

    }
}