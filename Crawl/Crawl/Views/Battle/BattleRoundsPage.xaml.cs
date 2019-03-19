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

            _viewModel.StartBattle();


            // Load the Characters into the Battle Engine
            _viewModel.LoadCharacters();


            _viewModel.StartRound();


            BindingContext = _viewModel;

            //GoToModalPageMonsterList();

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
            
            int index = 0;

            Image monster1 = MonstersRelativeLayout.FindByName<Image>("mons1");
            Image monster2 = MonstersRelativeLayout.FindByName<Image>("mons2");
            Image monster3 = MonstersRelativeLayout.FindByName<Image>("mons3");
            Image monster4 = MonstersRelativeLayout.FindByName<Image>("mons4");
            Image monster5 = MonstersRelativeLayout.FindByName<Image>("mons5");
            Image monster6 = MonstersRelativeLayout.FindByName<Image>("mons6");

            List<Image> monsterImages = new List<Image>()
            {
                monster1, monster2, monster3, monster4, monster5, monster6
            };

            foreach (Image image in monsterImages)
            {
                image.Source = null;
            }

            List<Uri> imageURIs = new List<Uri>();

            foreach (var monster in this._viewModel.BattleEngine.MonsterList)
            {
                monsterImages[index++].Source = new Uri(monster.ImageURI);
            }

            //TODO: remove this when have monster list 
            MonstersRelativeLayout.FindByName<Image>("mons1").Source = new Uri(HawkboxResources.Monsters_Female_Agent_A);
            MonstersRelativeLayout.FindByName<Image>("mons2").Source = new Uri(HawkboxResources.Monsters_Male_Agent_B);
            MonstersRelativeLayout.FindByName<Image>("mons3").Source = new Uri(HawkboxResources.Monsters_Female_Agent_B);
            MonstersRelativeLayout.FindByName<Image>("mons4").Source = new Uri(HawkboxResources.Monsters_Male_Agent_B);
            MonstersRelativeLayout.FindByName<Image>("mons5").Source = null;
            MonstersRelativeLayout.FindByName<Image>("mons6").Source = new Uri(HawkboxResources.Monsters_Female_Agent_E);

        }

        private void AddCharactersToBox(RelativeLayout CharactersRelativeLayout)
        {
            int index = 0;

            Image character1 = CharactersRelativeLayout.FindByName<Image>("char1");
            Image character2 = CharactersRelativeLayout.FindByName<Image>("char2");
            Image character3 = CharactersRelativeLayout.FindByName<Image>("char3");
            Image character4 = CharactersRelativeLayout.FindByName<Image>("char4");
            Image character5 = CharactersRelativeLayout.FindByName<Image>("char5");
            Image character6 = CharactersRelativeLayout.FindByName<Image>("char6");

            List<Image> characterImages = new List<Image>()
            {
                character1, character2, character3, character4, character5, character6
            };

            foreach(Image image in characterImages)
            {
                image.Source = null;
            }

            List<Uri> imageURIs = new List<Uri>();

            foreach(var character in this._viewModel.BattleEngine.CharacterList)
            {
                characterImages[index++].Source = new Uri(character.ImageURI);
            }
            
        }

        public async void ItemPagesClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ItemLocationSelectPage());
        }
    }
}