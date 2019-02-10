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

        // Switches the datastore values.
        // Loads the databases...
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

        private static void ModifyDataStoreOnViewModels()
        {
            ItemsViewModel.Instance.SetDataStore(_dataStoreEnum);
            CharactersViewModel.Instance.SetDataStore(_dataStoreEnum);
            MonstersViewModel.Instance.SetDataStore(_dataStoreEnum);
            // Implement Score
        }

        private static void RefreshViewModels()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
        }

        // Force all modes to load data...
        public static void ForceDataRestoreAll()
        {
            ItemsViewModel.Instance.ForceDataRefresh();
            CharactersViewModel.Instance.ForceDataRefresh();
            MonstersViewModel.Instance.ForceDataRefresh();
            // Implement Score
        }
    }
}
