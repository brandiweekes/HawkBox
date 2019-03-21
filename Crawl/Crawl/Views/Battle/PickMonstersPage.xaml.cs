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
        // view model for view monsters page
        private PickMonstersViewModel _viewModel;

        // battle view model instance - singleton
        public BattleViewModel battleViewModel;

        /// <summary>
        /// constructor
        /// </summary>
        public PickMonstersPage ()
		{
			InitializeComponent ();

            _viewModel = PickMonstersViewModel.Instance;
            battleViewModel = BattleViewModel.Instance;

            // clear dataset
            _viewModel.DataSet.Clear();

            foreach (Monster m in battleViewModel.BattleEngine.MonsterList)
            {
                _viewModel.DataSet.Add(m);
            }

            BindingContext = _viewModel;

		}

        /// <summary>
        /// close modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NextClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// disable back buttonon page appearing event
        /// </summary>
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