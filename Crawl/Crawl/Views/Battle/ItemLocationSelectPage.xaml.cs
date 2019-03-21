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

            //delete for final game, testing for items
            if (BattleViewModel.Instance.BattleEngine.ItemPool.Count < 1)
            {
                BattleViewModel.Instance.AddItemsToPoolForTesting();
            }

            if (BattleViewModel.Instance.RemainingCharacters.Count < 1)
            {
                //add remaining characters to remaining character observable collection in BattleViewModel
                foreach (var ch in BattleViewModel.Instance.BattleEngine.CharacterList)
                {
                    BattleViewModel.Instance.RemainingCharacters.Add(ch);
                    Debug.WriteLine(ch.Name);
                }
            }
            
            //set binding context
            BindingContext = viewModel = BattleViewModel.Instance;
		}

        private async void NextButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void LocationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            ItemLocationEnum buttonEnum = (ItemLocationEnum)Int32.Parse(button.ClassId);
            viewModel.AvailableItems.Clear();

            var itemList = viewModel.BattleEngine.ItemPool.Where(a => a?.Location == buttonEnum).OrderByDescending(a => a.Value).ToList();

            foreach(var item in itemList)
            {
                viewModel.AvailableItems.Add(item);
            }


            if(viewModel.pickedCharacter == null)
            {
                InstructionLabel.Text = String.Format("Select a character");
            }
            else if (viewModel.pickedCharacter.GetItemByLocation(buttonEnum) == null && itemList.Count < 1 )
            {
                InstructionLabel.Text = String.Format("No items available for this location");
            }
            else
            {
                await Navigation.PushModalAsync(new ItemSelectPage(buttonEnum));
            }

            /*
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
            }*/

           // if (!(RemainingListView.SelectedItem is Character character))
             //   return;

           // viewModel.pickedCharacter = character;

           // await Navigation.PushModalAsync(new ItemSelectPage(viewModel));
        }
        
        private void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Character character)) {
                return;
            }

            viewModel.pickedCharacter = character;

            AttackLabel.Text = String.Format("{0}", character.Attribute.Attack);
            DefenseLabel.Text = String.Format("{0}", character.Attribute.Defense);
            SpeedLabel.Text = String.Format("{0}", character.Attribute.Speed);
            HPLabel.Text = String.Format("{0}", character.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", character.ExperienceTotal);

            var ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.Head);
            if(ItemInLocation != null)
            {
                HeadImage.Source = ItemInLocation.ImageURI;
                HeadButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                HeadImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                HeadButton.Text = String.Format("None");
            }

            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.Necklass);
            if (ItemInLocation != null)
            {
                NecklaceImage.Source = ItemInLocation.ImageURI;
                NecklaceButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                NecklaceImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                NecklaceButton.Text = String.Format("None");
            }

            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.OffHand);
            if (ItemInLocation != null)
            {
                OHImage.Source = ItemInLocation.ImageURI;
                OHButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                OHImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                OHButton.Text = String.Format("None");
            }

            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.PrimaryHand);
            if (ItemInLocation != null)
            {
                PHImage.Source = ItemInLocation.ImageURI;
                PHButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                PHImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                PHButton.Text = String.Format("None");
            }

            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.LeftFinger);
            if (ItemInLocation != null)
            {
                LFImage.Source = ItemInLocation.ImageURI;
                LFButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                LFImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                LFButton.Text = String.Format("None");
            }

            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.RightFinger);
            if (ItemInLocation != null)
            {
                RFImage.Source = ItemInLocation.ImageURI;
                RFButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                RFImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                RFButton.Text = String.Format("None");
            }

            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.Feet);
            if (ItemInLocation != null)
            {
                FeetImage.Source = ItemInLocation.ImageURI;
                FeetButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                FeetImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                FeetButton.Text = String.Format("None");
            }

        }
	}
}