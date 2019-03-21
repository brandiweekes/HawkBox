using Crawl.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage : ContentPage
	{
		public BattlePage ()
		{
			InitializeComponent ();
		}

        // redirects to start of game to select characters
        private async void ManualBattleButton_Command(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PickCharactersPage());
        }

        // Cancel battle event. navigates to previous page by popping current page in navigation stack
        private async void CancelBattle(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}