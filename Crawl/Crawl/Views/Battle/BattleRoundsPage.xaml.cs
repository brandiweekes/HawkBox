using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;
using Crawl.GameEngine;
using System.Diagnostics;
using System.Threading;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleRoundsPage : ContentPage
	{
        // Hold the Selected Characters
        PickCharactersPage _myModalCharacterSelectPage;

        // Hold the Monsters
        PickMonstersPage _myModalBattleMonsterListPage;

        // Hold the Items
        private ItemLocationSelectPage _myModalItemSelectionPage;

        // Holds the view model
        private BattleViewModel _viewModel;
        

        public BattleRoundsPage ()
		{
            // this is a singleton & should NOT be null
            // this should have the updates from prior pages
            _viewModel = BattleViewModel.Instance;

            InitializeComponent();

            // Show the Next button, hide the Game Over button
            GameAttackButton.IsVisible = true;
            GameOverButton.IsVisible = false;

            // start battle
            _viewModel.StartBattle();
            
            // Load the Characters into the Battle Engine
            _viewModel.LoadCharacters();
            Debug.WriteLine("Battle Start" + " Characters :" + _viewModel.BattleEngine.CharacterList.Count);

            // start round
            _viewModel.StartRound();
            Debug.WriteLine("Round Start" + " Monsters:" + _viewModel.BattleEngine.MonsterList.Count);

            BindingContext = _viewModel;

            ResetBattleScreen();

            ShowModalPageMonsterList();

            PositionPlayersOnScreen();

            
        }

        private void ResetBattleScreen()
        {
            RelativeLayout myRelativeCharacterStats = this.FindByName<RelativeLayout>("BattleCharacterStats");
            RelativeLayout myRelativeMonsterStats = this.FindByName<RelativeLayout>("BattleMonsterStats");

            ResetStatsBox(myRelativeCharacterStats, "CharName", "c", "Character");
            ResetStatsBox(myRelativeMonsterStats, "MonsName", "m", "Monster");

        }

        /// <summary>
        /// handles player stat box setting values to * at start 
        /// </summary>
        /// <param name="playerStats">data where stat box found</param>
        /// <param name="xName">bound data Label name</param>
        /// <param name="p">prefix for stat name; c=character, m=monster</param>
        /// <param name="playerType">character or monster</param>
        private void ResetStatsBox(RelativeLayout playerStats, string xName, string p, string playerType)
        {
            string playerName = "Battling " + playerType;
            playerStats.FindByName<Label>(xName).Text = playerName;

            playerStats.FindByName<Label>(p+"XPStat").Text = "***";
            playerStats.FindByName<Label>(p+"LevelStat").Text = "***";
            playerStats.FindByName<Label>(p+"HPStat").Text = "***";
            playerStats.FindByName<Label>(p+"HPMaxStat").Text = "***";
            playerStats.FindByName<Label>(p+"ATKStat").Text = "***";
            playerStats.FindByName<Label>(p+"DEFStat").Text = "***";
            playerStats.FindByName<Label>(p+"SPDStat").Text = "***";
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
                monsterImages[index].Source = new Uri(monster.ImageURI);
                
                index++;
            }
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
                if (character.Alive == false)
                {
                    characterImages[index].Opacity = .5;
                }
            }
            
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnGameOverClicked(object sender, EventArgs args)
        {
            var myScore = _viewModel.BattleEngine.BattleScore.ScoreTotal;
            var outputString = "Battle Over! Score " + myScore.ToString();
            Debug.WriteLine(outputString);


            ShowModalItemSelectionPage();

            // Back up to the Start of Battle
            await Navigation.PushModalAsync(new PickCharactersPage());
        }

        private async void ShowModalItemSelectionPage()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            Crawl.App.Current.ModalPopping += HandleModalPopping;
            _myModalItemSelectionPage = new ItemLocationSelectPage();
            await Navigation.PushModalAsync(_myModalItemSelectionPage);
        }

   

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _myModalBattleMonsterListPage)
            {
                _myModalBattleMonsterListPage = null;

                // remember to remove the event handler:
                App.Current.ModalPopping -= HandleModalPopping;
            }

            if (e.Modal == _myModalCharacterSelectPage)
            {
                _myModalCharacterSelectPage = null;

                // remember to remove the event handler:
                App.Current.ModalPopping -= HandleModalPopping;
            }

            if (e.Modal == _myModalItemSelectionPage)
            {
                _myModalItemSelectionPage = null;

                // remember to remove the event handler:
                App.Current.ModalPopping -= HandleModalPopping;
            }
        }

        private async void ShowModalPageMonsterList()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            Crawl.App.Current.ModalPopping += HandleModalPopping;
            _myModalBattleMonsterListPage = new PickMonstersPage();
            await Navigation.PushModalAsync(_myModalBattleMonsterListPage);
        }

        /// <summary>
        /// Next Turn Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnAttackClicked(object sender, EventArgs args)
        {
            //Thread.Sleep(2000);
            //AbsoluteLayout.SetLayoutBounds(AttackerImage, new Rectangle(0, 0.6, .3, .3));

            // Do the turn...
            _viewModel.RoundNextTurn();
            //MessagingCenter.Send(this, "RoundNextTurn");

            // Hold the current state
            var CurrentRoundState = _viewModel.BattleEngine.RoundStateEnum;

            // If the round is over start a new one...
            if (CurrentRoundState == RoundEnum.NewRound)
            {
                ShowModalItemSelectionPage();

                _viewModel.NewRound();
                // MessagingCenter.Send(this, "NewRound");

                Debug.WriteLine("New Round :" + _viewModel.BattleEngine.BattleScore.RoundCount);

                ShowModalPageMonsterList();
            }

            // Check for Game Over
            if (CurrentRoundState == RoundEnum.GameOver)
            {
                
                ////MessagingCenter.Send(this, "EndBattle");

                // Output Formatted Results 
                //var myResult = _viewModel.BattleEngine.GetResultsOutput();
                //Debug.Write(myResult);

                // Let the user know the game is over
                //ClearMessages();    // Clear message
                //AppendMessage("Game Over\n"); // Show Game Over

                // Clear the players from the center of the board
                DrawGameBoardClear();

                // Change to the Game Over Button
                GameAttackButton.IsVisible = false;
                GameOverButton.IsVisible = true;

                // Save the Score to the Score View Model, by sending a message to it.
                var myScore = _viewModel.BattleEngine.BattleScore;
                MessagingCenter.Send(this, "AddData", myScore);

                _viewModel.EndRound();

                _viewModel.EndBattle();

                Debug.WriteLine("Round count :" + _viewModel.BattleEngine.BattleScore.RoundCount);
                Debug.WriteLine("Turn count :" + _viewModel.BattleEngine.BattleScore.TurnCount);
                Debug.WriteLine("End Battle");

                return;
            }

            // Output the Game Board
            DrawGameBoardAttackerDefender();
            


            // Move attacker on battle field
            //Crawl.App.Current.BindingContextChanged += AnimateAttacker;
            

            // Output The Message that happened.
            // GameMessage();
        }

        private void AnimateAttacker(object sender, EventArgs e)
        {
            //Thread.Sleep(1000);
            AbsoluteLayout battleArena = this.FindByName<AbsoluteLayout>("BattleArena");
            var AttackingAnimation = battleArena.FindByName<Image>("AttackerImage");
            var DefendingAnimation = battleArena.FindByName<Image>("DefenderImage");

            AbsoluteLayout.SetLayoutBounds(AttackerImage, new Rectangle(0.1, 0.1, .3, .3));
            //Thread.Sleep(1000);
            AbsoluteLayout.SetLayoutBounds(AttackerImage, new Rectangle(0.2, 0.2, .3, .3));
            //Thread.Sleep(1000);
            AbsoluteLayout.SetLayoutBounds(AttackerImage, new Rectangle(0.3, 0.3, .3, .3));
            //Thread.Sleep(1000);
            AbsoluteLayout.SetLayoutBounds(AttackerImage, new Rectangle(0.4, 0.4, .3, .3));
            //Thread.Sleep(1000);
            AbsoluteLayout.SetLayoutBounds(AttackerImage, new Rectangle(0.5, 0.5, .3, .3));

            DrawGameBoardAttackerDefender();
            Crawl.App.Current.PropertyChanging -= AnimateAttacker;
            
        }

        private void DrawGameBoardAttackerDefender()
        {
            AbsoluteLayout battleArena = this.FindByName<AbsoluteLayout>("BattleArena");
            battleArena.FindByName<Image>("AttackerImage").Source = _viewModel.BattleEngine.CurrentAttacker.ImageURI;
            battleArena.FindByName<Image>("DefenderImage").Source = _viewModel.BattleEngine.CurrentDefender.ImageURI;

            DrawStatsBoxAttackerDefender();

            
        }

        private void DrawStatsBoxAttackerDefender()
        {
            var currentAttacker = _viewModel.BattleEngine.CurrentAttacker;
            var currentDefender = _viewModel.BattleEngine.CurrentDefender;
            RelativeLayout battleCharStatBox = this.FindByName<RelativeLayout>("BattleCharacterStats");
            RelativeLayout battleMonsStatBox = this.FindByName<RelativeLayout>("BattleMonsterStats");

            if (currentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                
                battleCharStatBox.FindByName<Label>("CharName").Text = currentAttacker.Name;

                battleCharStatBox.FindByName<Label>("cXPStat").Text = currentAttacker.ExperiencePoints.ToString();
                battleCharStatBox.FindByName<Label>("cLevelStat").Text = currentAttacker.Level.ToString();
                battleCharStatBox.FindByName<Label>("cHPStat").Text = currentAttacker.CurrentHealth.ToString();
                battleCharStatBox.FindByName<Label>("cHPMaxStat").Text = currentAttacker.MaxHealth.ToString();
                battleCharStatBox.FindByName<Label>("cATKStat").Text = currentAttacker.Attack.ToString();
                battleCharStatBox.FindByName<Label>("cDEFStat").Text = currentAttacker.Defense.ToString();
                battleCharStatBox.FindByName<Label>("cSPDStat").Text = currentAttacker.Speed.ToString();

                battleMonsStatBox.FindByName<Label>("MonsName").Text = currentDefender.Name;

                battleMonsStatBox.FindByName<Label>("mXPStat").Text = currentDefender.ExperiencePoints.ToString();
                battleMonsStatBox.FindByName<Label>("mLevelStat").Text = currentDefender.Level.ToString();
                battleMonsStatBox.FindByName<Label>("mHPStat").Text = currentDefender.CurrentHealth.ToString();
                battleMonsStatBox.FindByName<Label>("mHPMaxStat").Text = currentDefender.MaxHealth.ToString();
                battleMonsStatBox.FindByName<Label>("mATKStat").Text = currentDefender.Attack.ToString();
                battleMonsStatBox.FindByName<Label>("mDEFStat").Text = currentDefender.Defense.ToString();
                battleMonsStatBox.FindByName<Label>("mSPDStat").Text = currentDefender.Speed.ToString();
            }
            else if(currentAttacker.PlayerType == PlayerTypeEnum.Monster)
            {
                battleCharStatBox.FindByName<Label>("CharName").Text = currentDefender.Name;

                battleCharStatBox.FindByName<Label>("cXPStat").Text = currentDefender.ExperiencePoints.ToString();
                battleCharStatBox.FindByName<Label>("cLevelStat").Text = currentDefender.Level.ToString();
                battleCharStatBox.FindByName<Label>("cHPStat").Text = currentDefender.CurrentHealth.ToString();
                battleCharStatBox.FindByName<Label>("cHPMaxStat").Text = currentDefender.MaxHealth.ToString();
                battleCharStatBox.FindByName<Label>("cATKStat").Text = currentDefender.Attack.ToString();
                battleCharStatBox.FindByName<Label>("cDEFStat").Text = currentDefender.Defense.ToString();
                battleCharStatBox.FindByName<Label>("cSPDStat").Text = currentDefender.Speed.ToString();

                battleMonsStatBox.FindByName<Label>("MonsName").Text = currentAttacker.Name;

                battleMonsStatBox.FindByName<Label>("mXPStat").Text = currentAttacker.ExperiencePoints.ToString();
                battleMonsStatBox.FindByName<Label>("mLevelStat").Text = currentAttacker.Level.ToString();
                battleMonsStatBox.FindByName<Label>("mHPStat").Text = currentAttacker.CurrentHealth.ToString();
                battleMonsStatBox.FindByName<Label>("mHPMaxStat").Text = currentAttacker.MaxHealth.ToString();
                battleMonsStatBox.FindByName<Label>("mATKStat").Text = currentAttacker.Attack.ToString();
                battleMonsStatBox.FindByName<Label>("mDEFStat").Text = currentAttacker.Defense.ToString();
                battleMonsStatBox.FindByName<Label>("mSPDStat").Text = currentAttacker.Speed.ToString();
            }
            else
            {
                ResetStatsBox(battleCharStatBox, "CharName", "c", "Character");
                ResetStatsBox(battleMonsStatBox, "MonsName", "m", "Monster");
            }
        }

        private void DrawGameBoardClear()
        {
            AbsoluteLayout battleArena = this.FindByName<AbsoluteLayout>("BattleArena");
            battleArena.FindByName<Image>("AttackerImage").Source = null;
            battleArena.FindByName<Image>("DefenderImage").Source = null;

            RelativeLayout battleCharStatBox = this.FindByName<RelativeLayout>("BattleCharacterStats");
            RelativeLayout battleMonsStatBox = this.FindByName<RelativeLayout>("BattleMonsterStats");
            ResetStatsBox(battleCharStatBox, "CharName", "c", "Character");
            ResetStatsBox(battleMonsStatBox, "MonsName", "m", "Monster");
            
        }
    }
}