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
	public partial class ItemLocationSelectPage : ContentPage
	{
		public ItemLocationSelectPage ()
		{
			InitializeComponent();
		}

        private async void NextButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattleRoundsPage());
        }

        private async void LocationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemSelectPage());
        }
	}
}