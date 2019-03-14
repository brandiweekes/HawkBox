using Crawl.GameEngine;
using Crawl.Models;
using Crawl.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {
        private PickCharactersViewModel _viewModel;

        private BattleEngine _battleEngine;

        public PickCharactersPage(BattleEngine battleEngine)
        {
            InitializeComponent();
            BindingContext = _viewModel = PickCharactersViewModel.Instance;
            if (_viewModel.DataSet.Count == 0)
                _viewModel.ForceDataRefresh();

            _battleEngine = battleEngine;
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
            var _list = _viewModel.GetSelectedCharacters();

            // add selected characters to battle's characters list
            _battleEngine.CharacterList.AddRange(_list);

            // start battle - set autobattle flag to false
            _battleEngine.StartBattle(false);

            // start a round
            _battleEngine.StartRound();

            await Navigation.PushModalAsync(new PickMonstersPage(_battleEngine));
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