using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Crawl.Models;
using Crawl.Views;
using System.Linq;
using Crawl.Controllers;

namespace Crawl.ViewModels
{
    public class ScoresViewModel : BaseViewModel
    {
        #region Singleton
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ScoresViewModel _instance;

        public static ScoresViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoresViewModel();
                }
                return _instance;
            }
        }

        #endregion Singleton

        public ObservableCollection<Score> Dataset { get; set; }
        public Command LoadScoresCommand { get; set; }

        private bool _needsRefresh;

        /// <summary>
        /// initialize properties and define Messages
        /// </summary>
        private ScoresViewModel()
        {
            Title = "Score List";
            Dataset = new ObservableCollection<Score>();
            LoadScoresCommand = new Command(async () => await ExecuteLoadDataCommand());

            // Implement 
            #region Messages
            MessagingCenter.Subscribe<ScoreDeletePage, Score>(this, "DeleteScore", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<ScoreNewPage, Score>(this, "AddScore", async (obj, data) =>
            {
                var newScore = data as Score;
                await AddAsync(newScore);
            });

            MessagingCenter.Subscribe<ScoreEditPage, Score>(this, "EditScore", async (obj, data) =>
            {
                var newScore = data as Score;
                await UpdateAsync(newScore);
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
            if(_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;

        }

        /// <summary>
        /// Sets the need to refresh. used to refresh data from datastore.
        /// </summary>
        /// <param name="value"></param>
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        /// <summary>
        /// Command function which is executed to refresh dataset from data store.
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
                var scores = await DataStore.GetAllAsync_Score(true);
                foreach (var score in scores)
                {
                    Dataset.Add(score);
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
        /// -- Used when DataSore is toggled between Mock and SQL.Check<see cref="Services.MasterDataStore"/>
        /// </summary>
        public void ForceDataRefresh()
        {
            LoadScoresCommand.Execute(null);
        }

        #endregion Refresh

        #region Data Operations

        /// <summary>
        /// Call to database operation for delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Score data)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            Dataset.Remove(myData);
            var myReturn = await DataStore.DeleteAsync_Score(data);
            return myReturn;
        }

        /// <summary>
        /// Call to database operation for add
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Score data)
        {
            Dataset.Add(data);
            return await DataStore.AddAsync_Score(data);
        }

        /// <summary>
        /// Call to database operation for update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Score data)
        {
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
                return await Task.FromResult(false);

            myData.Update(data);
            SetNeedsRefresh(true);
            return await DataStore.UpdateAsync_Score(data);
        }

        /// <summary>
        /// Call to database to ensure most recent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Score> GetAsync(string id)
        {
            return await DataStore.GetAsync_Score(id);
        }

        #endregion Data Operations
    }
}