using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleRoundsPage : ContentPage
	{
        // Holds the view model
        private BattleViewModel _viewModel;

		public BattleRoundsPage ()
		{
            // this is a singleton & should NOT be null
            // this should have the updates from prior pages
            _viewModel = BattleViewModel.Instance;

            InitializeComponent();

            // Load the Characters into the Battle Engine
            _viewModel.LoadCharacters();
        }

        public async void ItemPagesClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ItemLocationSelectPage());
        }
    }
}