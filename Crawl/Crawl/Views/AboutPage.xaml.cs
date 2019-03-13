
using System;
using Crawl.Services;
using Crawl.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.ViewModels;
using Crawl.Models;
using System.Collections.Generic;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            // Set the flag for Mock on or off...
            UseMockDataSource.IsToggled = (MasterDataStore.GetDataStoreMockFlag() == DataStoreEnum.Mock);
            SetDataSource(UseMockDataSource.IsToggled);

            // Example of how to add an view to an existing set of XAML. 
            // Give the Xaml layout you want to add the data to a good x:Name, so you can access it.  Here "DateRoot" is what I am using.
            var dateLabel = new Label
            {
                Text = String.Format("{0:MMMM d, yyyy HH:mm}", DateTime.Now),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "Helvetica Neue",
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                TextColor = Color.Black,
            };

            DateRoot.Children.Add(dateLabel);

            // Set debug settings
            EnableCriticalMissProblems.IsToggled = GameGlobals.EnableCriticalMissProblems;
            EnableCriticalHitDamage.IsToggled = GameGlobals.EnableCriticalHitDamage;

        }

        #region Database Settings

        /// <summary>
        /// Set datastore based on switch.
        /// </summary>
        /// <param name="isMock"></param>
        private void SetDataSource(bool isMock)
        {
            var set = DataStoreEnum.Sql;

            if (isMock)
            {
                set = DataStoreEnum.Mock;
            }

            MasterDataStore.ToggleDataStore(set);
        }

        /// <summary>
        /// Swich to toggle between Mock and SQL Datastore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseMockDataSourceSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.
            SetDataSource(UseMockDataSource.IsToggled);
        }

        /// <summary>
        /// clear SQL Datastore and re-initializes data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ClearDatabase_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Sure you want to Delete All Data, and start over?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables.
                SQLDataStore.Instance.InitializeDatabaseNewTables();
            }
        }

        #endregion Database Settings

        #region Debug Settings

        /// <summary>
        /// Show Debug options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableDebugSettings_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.

            DebugSettingsFrame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Turn on Critical Misses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableCriticalMissProblems_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.
            GameGlobals.EnableCriticalMissProblems = e.Value;
        }

        /// <summary>
        /// Turn on Critical Hit Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableCriticalHitDamage_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.
            GameGlobals.EnableCriticalHitDamage = e.Value;
        }

        #endregion Debug Settings

        #region Force Random value

        /// <summary>
        /// Enable Ranom Force Flag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableFoceRandomSettings_OnToggled(object sender, ToggledEventArgs e)
        {

            if (e.Value)
            {
                ForceRandomSettingsFrame.IsVisible = true;
                GameGlobals.EnableRandomValues();

                GameGlobals.SetForcedRandomNumbersValue(Convert.ToInt16(ForceRandomValue.Text));
            }
            else
            {
                GameGlobals.DisableRandomValues();
                ForceRandomSettingsFrame.IsVisible = false;
            }
        }

        /// <summary>
        /// Enable Force Random value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForcedRandomValue_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ForceRandomValue.Text = string.Format("{0}", e.NewValue);
            GameGlobals.SetForcedRandomNumbersValue(Convert.ToInt16(ForceRandomValue.Text));
        }

        /// <summary>
        /// Enable force to hit value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForceRandomValueToHit_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ForceRandomValueToHit.Text = string.Format("{0}", e.NewValue);
            GameGlobals.SetForceToHitValue(Convert.ToInt16(ForceRandomValueToHit.Text));
        }

        #endregion Force Random value

        #region Web-Server calls

        /// <summary>
        /// Get Items from Server and refresh Items in Datastore.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetItems_Command(object sender, EventArgs e)
        {
            var myOutput = "No Items";
            var answer = await DisplayAlert("Get", "Sure you want to Get Items from the Server?", "Yes", "No");
            if (answer)
            {
                var _count = Convert.ToInt32(ServerItemValue.Text);
                // Call to the Item Service and have it Get the Items. Update them to datastore.
                var myItemList = await ItemsController.Instance.GetItemsFromServer(_count);

                if (myItemList != null && myItemList.Count > 0)
                {
                    // Reset the output
                    myOutput = "";

                    foreach (var item in myItemList)
                    {
                        myOutput += item.FormatOutput() + "\n******\n";
                    }
                }
            }
            await DisplayAlert("Items from Server", myOutput, "OK");
        }

        /// <summary>
        /// Get specific items from Server and refresh items in Datastore.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetItemsPost_Command(object sender, EventArgs e)
        {
            var number = Convert.ToInt32(ServerItemValue.Text);
            var level = 6;  // Max Value of 6
            var attribute = AttributeEnum.Unknown;  // Any Attribute
            var location = ItemLocationEnum.Unknown;    // Any Location
            var random = true;  // Random between 1 and Level
            var updateDataBase = true;  // Add them to the DB

            var myOutput = "No Items";
            var myItemList = await ItemsController.Instance.GetItemsFromGame(number, level, attribute, location, random, updateDataBase);

            if (myItemList != null && myItemList.Count > 0)
            {
                // Reset the output
                myOutput = "";

                foreach (var item in myItemList)
                {
                    myOutput += item.FormatOutput() + "\n******\n";
                }
            }

            await DisplayAlert("Items from Server", myOutput, "OK");
        }

        #endregion Web-Server calls

    }
}