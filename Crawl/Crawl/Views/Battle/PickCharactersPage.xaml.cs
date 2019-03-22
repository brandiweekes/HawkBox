using Crawl.GameEngine;
using Crawl.Models;
using Crawl.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {
        private PickCharactersViewModel _viewModel;

        public BattleViewModel _battleViewModel;

        public PickCharactersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = PickCharactersViewModel.Instance;
            //if (_viewModel.DataSet.Count == 0)
                _viewModel.ForceDataRefresh();

            _battleViewModel = BattleViewModel.Instance;

        }

        private void OnCharacterTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.Item is MultiSelectData multiSelectData))
                return;
            var index = _viewModel.DataSet.IndexOf(multiSelectData);
            _viewModel.DataSet[index].IsSelected = _viewModel.DataSet[index].IsSelected ? false : true;
            _viewModel.DataSet[index].Image = _viewModel.DataSet[index].Image.Equals("checkbox_unchecked_icon.png")
                ? "checkbox_checked_icon.png" : "checkbox_unchecked_icon.png";

            CharactersListView.SelectedItem = null;
            ValidateSelectedData();
        }

        private async void NextClicked(object sender, EventArgs e)
        {
            List<Character> _list = _viewModel.GetSelectedCharacters();

            // Add Selected characters to BattleViewModel
            _battleViewModel.SelectedCharacters = new ObservableCollection<Character>(_list);

            // clear selcted characters from viewmodel
            _viewModel.ForceDataRefresh();

            await Navigation.PushModalAsync(new BattleRoundsPage());
        }

        private void ValidateSelectedData()
        {
            var dataset = _viewModel.DataSet.Where(s => s.IsSelected == true).ToList();
            if (dataset.Count > 0 && dataset.Count < 7)
                PickCharacters_NextButton.IsEnabled = true;
            else
                PickCharacters_NextButton.IsEnabled = false;
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