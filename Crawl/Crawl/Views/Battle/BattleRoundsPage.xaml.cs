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

            PositionPlayersOnScreen();
        }

        public void PositionPlayersOnScreen()
        {
            RelativeLayout myRelativeCharacters = this.FindByName<RelativeLayout>("CharacterBox");
            AddCharactersToBox(myRelativeCharacters);

            RelativeLayout myRelativeMonsters = this.FindByName<RelativeLayout>("MonsterBox");
            AddMonstersToBox(myRelativeMonsters);
        }

        private void AddMonstersToBox(RelativeLayout MonstersRelativeLayout)
        {
            MonstersRelativeLayout.FindByName<Image>("mons1").Source = new Uri(HawkboxResources.Monsters_Female_Agent_A);
            MonstersRelativeLayout.FindByName<Image>("mons2").Source = new Uri(HawkboxResources.Monsters_Male_Agent_B);
            MonstersRelativeLayout.FindByName<Image>("mons3").Source = new Uri(HawkboxResources.Monsters_Female_Agent_B);
            MonstersRelativeLayout.FindByName<Image>("mons4").Source = new Uri(HawkboxResources.Monsters_Male_Agent_B);
            MonstersRelativeLayout.FindByName<Image>("mons5").Source = null;
            MonstersRelativeLayout.FindByName<Image>("mons6").Source = new Uri(HawkboxResources.Monsters_Female_Agent_E);
        }

        private void AddCharactersToBox(RelativeLayout CharactersRelativeLayout)
        {
            
            CharactersRelativeLayout.FindByName<Image>("char1").Source = new Uri(HawkboxResources.Aliens_Char_7);
            CharactersRelativeLayout.FindByName<Image>("char2").Source = new Uri(HawkboxResources.Aliens_Char_5);
            CharactersRelativeLayout.FindByName<Image>("char3").Source = new Uri(HawkboxResources.Aliens_Char_3);
            CharactersRelativeLayout.FindByName<Image>("char4").Source = new Uri(HawkboxResources.Aliens_Char_1);
            CharactersRelativeLayout.FindByName<Image>("char5").Source = null;
            CharactersRelativeLayout.FindByName<Image>("char6").Source = new Uri(HawkboxResources.Aliens_Char_6);

        }

        public async void ItemPagesClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ItemLocationSelectPage());
        }
    }
}