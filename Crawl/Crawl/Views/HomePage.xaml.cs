using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        async void OnAutoBattleClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Battle.AutoBattlePage());
        }

        async void OnBattleClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Battle.BattlePage());
        }
    }
}