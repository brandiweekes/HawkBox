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
	public partial class ItemSelectPage : ContentPage
	{
		public ItemSelectPage ()
		{
			InitializeComponent ();
		}

        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemLocationSelectPage());
        }

        public async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
	}
}