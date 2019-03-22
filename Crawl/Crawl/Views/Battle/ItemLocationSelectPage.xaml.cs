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
        //battleviewmodel instance holding game information
        BattleViewModel viewModel;

        /// <summary>
        /// Constructor
        /// </summary>
		public ItemLocationSelectPage ()
		{
			InitializeComponent();

            //delete for final game, testing for items
            /* if (BattleViewModel.Instance.BattleEngine.ItemPool.Count < 1)
             {
                 BattleViewModel.Instance.AddItemsToPoolForTesting();
             }*/
            //var ct = BattleViewModel.Instance.BattleEngine.ItemPool.Count;

            if (BattleViewModel.Instance.RemainingCharacters.Count < 1)
            {
                //add remaining characters to remaining character observable collection in BattleViewModel
                foreach (var ch in BattleViewModel.Instance.BattleEngine.CharacterList)
                {
                    //add remaining character
                    BattleViewModel.Instance.RemainingCharacters.Add(ch);
                    Debug.WriteLine(ch.Name);
                }
            }
           // viewModel = BattleViewModel.Instance;
            //var ct = viewModel.BattleEngine.ItemPool.Count;
            //set binding context
            BindingContext = viewModel = BattleViewModel.Instance;
		}

        /// <summary>
        /// button handler, takes user to next round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NextButtonClicked(object sender, EventArgs e)
        {
            //pop back to battleroundspage
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Event handler to select item for selected location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LocationClicked(object sender, EventArgs e)
        {
            //get location of button clicked
            var button = (Button)sender;
            ItemLocationEnum buttonEnum = (ItemLocationEnum)Int32.Parse(button.ClassId);
            //clear viewmodel available items observablecollection
            viewModel.AvailableItems.Clear();
            //get available items at location
            var itemList = viewModel.BattleEngine.ItemPool.Where(a => a.Location == buttonEnum).OrderByDescending(a => a.Value).ToList();
            //var ct = viewModel.BattleEngine.ItemPool.Count;
            //add items to available items
            foreach(var item in itemList)
            {
                viewModel.AvailableItems.Add(item);
            }

            //if no character selected
            if(viewModel.pickedCharacter == null)
            {
                //give instructions
                InstructionLabel.Text = String.Format("Select a character");
            }
            //if no items available and no items equipped
            else if (viewModel.pickedCharacter.GetItemByLocation(buttonEnum) == null && itemList.Count < 1 )
            {
                //display message to user
                InstructionLabel.Text = String.Format("No items available for this location");
            }
            //available items or item currently equipped in location
            else
            {
                //deselect character
                RemainingListView.SelectedItem = null;
                //set labels to default
                AttackLabel.Text = String.Format("--");
                DefenseLabel.Text = String.Format("--");
                SpeedLabel.Text = String.Format("--");
                HPLabel.Text = String.Format("--");
                XPLabel.Text = String.Format("--");
                //push item select page
                await Navigation.PushModalAsync(new ItemSelectPage(buttonEnum));
            }

        }
        
        /// <summary>
        /// Event handler for remaining character listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //if null, return
            if (!(args.SelectedItem is Character character)) {
                return;
            }
            //set picked character in battleviewmodel
            viewModel.pickedCharacter = character;
            //set attribute labels
            AttackLabel.Text = String.Format("{0}", character.Attribute.Attack + character.GetItemBonus(AttributeEnum.Attack));
            DefenseLabel.Text = String.Format("{0}", character.Attribute.Defense + character.GetItemBonus(AttributeEnum.Defense));
            SpeedLabel.Text = String.Format("{0}", character.Attribute.Speed + character.GetItemBonus(AttributeEnum.Speed));
            HPLabel.Text = String.Format("{0}", character.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", character.ExperienceTotal);         

            //show items picked characters current items

            //get and display head item
            var ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.Head);
            if(ItemInLocation != null)
            {
                //if not null, display item image & name on the button
                HeadImage.Source = ItemInLocation.ImageURI;
                HeadButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                //if no item, default label & image
                HeadImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                HeadButton.Text = String.Format("None");
            }

            //get and display necklace item
            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.Necklass);
            if (ItemInLocation != null)
            {
                //if not null, set item image & name
                NecklaceImage.Source = ItemInLocation.ImageURI;
                NecklaceButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                //if no item, default img and label
                NecklaceImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                NecklaceButton.Text = String.Format("None");
            }

            //offhand
            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.OffHand);
            if (ItemInLocation != null)
            {
                //item img & name
                OHImage.Source = ItemInLocation.ImageURI;
                OHButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                //default img and label
                OHImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                OHButton.Text = String.Format("None");
            }

            //priamryhand
            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.PrimaryHand);
            if (ItemInLocation != null)
            {
                //item img and name
                PHImage.Source = ItemInLocation.ImageURI;
                PHButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                //default img and label
                PHImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                PHButton.Text = String.Format("None");
            }

            //left finger
            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.LeftFinger);
            if (ItemInLocation != null)
            {
                //item img and name
                LFImage.Source = ItemInLocation.ImageURI;
                LFButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                //default img and label
                LFImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                LFButton.Text = String.Format("None");
            }

            //right finger
            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.RightFinger);
            if (ItemInLocation != null)
            {
                //item image and name
                RFImage.Source = ItemInLocation.ImageURI;
                RFButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                //default image and label
                RFImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                RFButton.Text = String.Format("None");
            }

            //feet
            ItemInLocation = viewModel.pickedCharacter.GetItemByLocation(ItemLocationEnum.Feet);
            if (ItemInLocation != null)
            {
                //item img and name
                FeetImage.Source = ItemInLocation.ImageURI;
                FeetButton.Text = String.Format(ItemInLocation.Name);
            }
            else
            {
                //default img and label
                FeetImage.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
                FeetButton.Text = String.Format("None");
            }

        }
	}
}