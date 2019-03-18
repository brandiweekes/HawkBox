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

        private async void ManualBattleButton_Command(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PickCharactersPage());
        }
    }
}