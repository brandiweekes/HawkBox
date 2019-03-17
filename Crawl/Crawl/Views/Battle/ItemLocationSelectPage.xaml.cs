using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.ViewModels;
using Crawl.Models;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemLocationSelectPage : ContentPage
	{
        BattleViewModel viewModel;

		public ItemLocationSelectPage ()
		{
			InitializeComponent();

            BindingContext = viewModel = BattleViewModel.Instance;
		}

        private async void NextButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BattleRoundsPage(BattleViewModel.Instance));
        }

        private async void LocationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            ItemLocationEnum buttonEnum = (ItemLocationEnum)Int32.Parse(button.ClassId);

            var itemList = viewModel.BattleEngine.ItemPool.Where(a => a.Location == buttonEnum).OrderByDescending(a => a.Value).ToList();

            try
            {
                BattleViewModel.Instance.AvailableItems.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            foreach (var item in itemList)
            {
                try
                {
                    BattleViewModel.Instance.AvailableItems.Add(item);
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

           // if (!(RemainingListView.SelectedItem is Character character))
             //   return;

           // viewModel.pickedCharacter = character;

            await Navigation.PushModalAsync(new ItemSelectPage(viewModel));
        }
        
        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Character character))
                return;

            try
            {
                BattleViewModel.Instance.pickedCharacter = character;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            AttackLabel.Text = String.Format("{0}", character.Attribute.Attack);
            DefenseLabel.Text = String.Format("{0}", character.Attribute.Defense);
            SpeedLabel.Text = String.Format("{0}", character.Attribute.Speed);
            HPLabel.Text = String.Format("{0}", character.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", character.ExperienceTotal);


        }
	}
}