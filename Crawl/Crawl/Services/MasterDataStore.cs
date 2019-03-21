using Crawl.ViewModels;
using Crawl.Models;

namespace Crawl.Services
{
    public static class MasterDataStore
    {
        // Holds which datastore to use.

        private static DataStoreEnum _dataStoreEnum = DataStoreEnum.Mock;

        // Returns which dtatstore to use
        public static DataStoreEnum GetDataStoreMockFlag()
        {
            return _dataStoreEnum;
        }

        /// <summary>
        /// Switches the datastore values.
        /// Loads the databases. notifies viewmodels.
        /// </summary>
        /// <param name="dataStoreEnum"></param>
        public static void ToggleDataStore(DataStoreEnum dataStoreEnum)
        {
            switch (dataStoreEnum)
            {

                case DataStoreEnum.Mock:
                    _dataStoreEnum = DataStoreEnum.Mock;
                    break;
                case DataStoreEnum.Sql:
                case DataStoreEnum.Unknown:
                default:
                    _dataStoreEnum = DataStoreEnum.Sql;
                    break;
            }

            // Change DataStore
            ModifyDataStoreOnViewModels();

            // Load the Data
            RefreshViewModels();

        }

        /// <summary>
        /// toggle datastore type - Mock or SQL
        /// </summary>
        private static void ModifyDataStoreOnViewModels()
        {
            ItemsViewModel.Instance.SetDataStore(_dataStoreEnum);
            CharactersViewModel.Instance.SetDataStore(_dataStoreEnum);
            MonstersViewModel.Instance.SetDataStore(_dataStoreEnum);
            ScoresViewModel.Instance.SetDataStore(_dataStoreEnum);
        }

        /// <summary>
        /// Notify viewmodels of change.
        /// </summary>
        private static void RefreshViewModels()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        /// <summary>
        ///  Force all modes to load data...
        /// </summary>
        public static void ForceDataRestoreAll()
        {
            ItemsViewModel.Instance.ForceDataRefresh();
            CharactersViewModel.Instance.ForceDataRefresh();
            MonstersViewModel.Instance.ForceDataRefresh();
            ScoresViewModel.Instance.ForceDataRefresh();
        }
    }
}
