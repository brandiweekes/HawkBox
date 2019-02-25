using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crawl.GameEngine;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		public AutoBattlePage ()
		{
			InitializeComponent ();
		}

        private async void AutoBattleButton_Command(object sender, EventArgs e)
        {
            // Can create a new battle engine...
            var myAutoBattleEngine = new AutoBattleEngine();

            myAutoBattleEngine.RunAutoBattle();
            if (myAutoBattleEngine.BattleEngine.CharacterList.Count != 6)
            {
                var answer = await DisplayAlert("Error", "No Characters to battle with", "OK","Cancel");
                if (answer)
                {
                    var a = 1;
                    // Can't run auto battle, no characters...
                }
            }

            if (myAutoBattleEngine.GetFinalScoreObject().RoundCount < 1)
            {
                var answer = await DisplayAlert("Error", "No Rounds Fought", "OK", "Cancel");
                if (answer)
                {
                    var a = 1;
                    // Can't run auto battle, no characters...
                }
            }

            var display = await DisplayAlert("AutoBattle Details", myAutoBattleEngine.FormatOutput(), "OK", "Cancel");

            //var outputString = "Battle Over! Score " + myBattleEngine.BattleScore.ScoreTotal;
            var action = await DisplayActionSheet("Click View Score below to see your score details", 
                "Cancel", 
                null, 
                "View Score");
            if (action == "View Score")
            {
                await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(myAutoBattleEngine.BattleEngine.BattleScore)));
            }
        }
    }
}