﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawl.Controllers;
using Crawl.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();

            // load items from server and update in database
            Debug.WriteLine("Load items from server");
            ItemsController.Instance.GetItemsFromServer().GetAwaiter();

		}

        /// <summary>
        /// button directs to autobattle page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnAutoBattleClicked(object sender, EventArgs args)
        {


            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.1, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.2, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.3, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.4, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.5, .3, .3));


            
            await Navigation.PushAsync(new Battle.AutoBattlePage());
        }

        /// <summary>
        /// button directs to battle page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnBattleClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Battle.BattlePage());
        }
        /*for testing item pages
        async void LocationClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Battle.ItemLocationSelectPage());
        }*/
    }
}