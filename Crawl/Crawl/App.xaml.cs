using System;
using Crawl.Models;
using Crawl.Services;
using Crawl.Views;
using Xamarin.Forms;
using SQLite;

namespace Crawl
{
    public partial class App : Application
    {
        private static SQLiteAsyncConnection _database;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("HawkboxDB.db3"));
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            // Load The Mock Datastore by default
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
