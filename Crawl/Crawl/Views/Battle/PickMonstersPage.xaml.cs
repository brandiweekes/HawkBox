using Crawl.GameEngine;
using Crawl.Models;
using Crawl.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickMonstersPage : ContentPage
	{
        private BattleEngine _battleEngine { set; get; }

        private PickMonstersViewModel _viewModel;

        public PickMonstersPage (BattleEngine battleEngine)
		{
			InitializeComponent ();
            _battleEngine = battleEngine;

            _viewModel = PickMonstersViewModel.Instance;

            foreach(Monster m in _battleEngine.MonsterList)
            {
                _viewModel.DataSet.Add(m);
            }

            BindingContext = _viewModel;
		}

        private async void NextClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BattleRoundsPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // disable back button
            if (NavigationPage.GetHasBackButton(this))
            {
                NavigationPage.SetHasBackButton(this, false);
            }
        }
    }
}