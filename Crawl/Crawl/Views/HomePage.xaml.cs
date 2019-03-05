using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


		}

        async void OnAutoBattleClicked(object sender, EventArgs args)
        {


            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.1, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.2, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.3, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.4, .3, .3));

            AbsoluteLayout.SetLayoutBounds(Alien, new Rectangle(0, 0.5, .3, .3));


            
            await Navigation.PushAsync(new Battle.AutoBattlePage());
        }

        async void OnBattleClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Battle.BattlePage());
        }
        /*
        async void LocationClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Battle.ItemLocationSelectPage());
        }*/
    }
}