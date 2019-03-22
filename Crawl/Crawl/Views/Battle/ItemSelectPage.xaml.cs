using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.ViewModels;
using Crawl.Models;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemSelectPage : ContentPage
	{
        //battleviewmodel instance
        BattleViewModel viewModel;
        //location
        ItemLocationEnum location;
        //currently equipped item
        Item EquippedItem;
        //selected item in listview
        Item SelectedItem;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loc"></param>
		public ItemSelectPage (ItemLocationEnum loc)
		{
			InitializeComponent ();
            //set binding context
            BindingContext = viewModel = BattleViewModel.Instance;
            //set location
            location = loc;
            //set equipped item
            EquippedItem = viewModel.pickedCharacter.GetItemByLocation(location);
            //set selected item to null
            SelectedItem = null;

            //set image of equipped item to default if no item equipped
            if(EquippedItem != null)
            {
                ItemPic.Source = EquippedItem.ImageURI;
            }

            //set labels for character attributes and image
            CharPic.Source = viewModel.pickedCharacter.ImageURI;
            AttackLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.Attack + viewModel.pickedCharacter.GetItemBonus(AttributeEnum.Attack));
            DefenseLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.Defense + viewModel.pickedCharacter.GetItemBonus(AttributeEnum.Defense));
            SpeedLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.Speed + viewModel.pickedCharacter.GetItemBonus(AttributeEnum.Speed));
            HPLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", viewModel.pickedCharacter.ExperienceTotal);
            //give instructions
            ItemDescLabel.Text = "To add an item, press Equip then Save\nTo remove item, press Unequip and Save\n" +
                   "To discard changes, press Cancel\nSave must be pressed to finalize equipped or unequipped items.";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vm"></param>
        public ItemSelectPage(BattleViewModel vm)
        {
            InitializeComponent();

            //binding context
            BindingContext = viewModel = vm;

            //set labels
            CharPic.Source = BattleViewModel.Instance.pickedCharacter.ImageURI;
            AttackLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Attack 
                + BattleViewModel.Instance.pickedCharacter.GetItemBonus(AttributeEnum.Attack));
            DefenseLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Defense
                + BattleViewModel.Instance.pickedCharacter.GetItemBonus(AttributeEnum.Defense));
            SpeedLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Speed 
                + BattleViewModel.Instance.pickedCharacter.GetItemBonus(AttributeEnum.Speed));
            HPLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.ExperienceTotal);          
        }

        /// <summary>
        /// Save button handler, finalizes changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            //cases

            //new item null, current item null (character has no item in slot)

            //new item null, current slot occupied

            //Equipped item, current item null

            //Equipped item current slot occupied with another item
            var current = viewModel.pickedCharacter.GetItemByLocation(location);

            //equip character based on user actions
            if(EquippedItem == null && current == null)
            {
                //slot already null, no need to do anything
                //no item dropped from slot, item pool not updated
            }
            else if(EquippedItem == null && current != null)
            {
                //replace item in slot with null
                var dropped = viewModel.pickedCharacter.AddItem(location, null);
                //add dropped item to pool
                viewModel.BattleEngine.ItemPool.Add(dropped);

            }
            else if(EquippedItem != null && current == null)
            {
                //equip new item
                var dropped = viewModel.pickedCharacter.AddItem(location, EquippedItem.Id);
                //remove equipped item from itempool
                viewModel.BattleEngine.ItemPool.Remove(EquippedItem);
                //no current item dropped, no need to add another item to pool
            }
            else if(EquippedItem != null && current != null)
            {
                //equip new item
                var dropped = viewModel.pickedCharacter.AddItem(location, EquippedItem.Id);
                //remove equipped item from item pool
                viewModel.BattleEngine.ItemPool.Remove(EquippedItem);
                //add dropped item back into item pool
                viewModel.BattleEngine.ItemPool.Add(dropped);
            }


            //need to update character in character list to reflect changes in equipment
            viewModel.BattleEngine.CharacterList[viewModel.BattleEngine.CharacterList.FindIndex(ch => ch.Id.Equals(viewModel.pickedCharacter.Id))] = viewModel.pickedCharacter;
            //set picked character null
            viewModel.pickedCharacter = null;
            //clear available items
            viewModel.AvailableItems.Clear();
            //pop back to itemlocaionselectpage
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// cancel button handler, cancels changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CancelButtonClicked(object sender, EventArgs e)
        {
            //set pickedcharacter to null and clear available items list
            viewModel.pickedCharacter = null;
            viewModel.AvailableItems.Clear();
            //pop page
            await Navigation.PopModalAsync();
        }

        /// <summary>
        ///Equipped button handler, equips item, preparing it to be added to character upon save button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EquipButtonClicked(object sender, EventArgs e)
        {
            //if no selected item
            if(SelectedItem == null)
            {
                return;
            }

            //if no current item
            if (EquippedItem == null)
            {
                //set equipped item
                EquippedItem = SelectedItem;
                //remove selected from available items
                viewModel.AvailableItems.Remove(SelectedItem);
                //set image
                ItemPic.Source = EquippedItem.ImageURI;
            }
            //if current item in slot
            else
            {
                //add current item to available items
                viewModel.AvailableItems.Add(EquippedItem);
                //set equipped item
                EquippedItem = SelectedItem;
                //remove selected item from available items
                viewModel.AvailableItems.Remove(SelectedItem);
                //set image
                ItemPic.Source = EquippedItem.ImageURI;
            }
            //set instruction label
            ItemDescLabel.Text = String.Format("{0} equipped, press Save to finalize and return", EquippedItem.Name);
        }

        /// <summary>
        /// unequip event handler, removes an equipped item from character, preparing for save clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UnequipButtonClicked(object sender, EventArgs e)
        {
            //if no equipped item, do nothing, if eqipped item...
            if (EquippedItem != null)
            {
                //add currently equipped item to available items
                viewModel.AvailableItems.Add(EquippedItem);
                //set instruction label
                ItemDescLabel.Text = String.Format("{0} unequipped, press Save to finalize and return", EquippedItem.Name);
                //set equipped item to null
                EquippedItem = null;
                //set image to default image
                ItemPic.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
            }
        }

        /// <summary>
        /// item selected handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //if no selected item
            if (!(args.SelectedItem is Item item))
                return;
            //set labels describing selected item
            ItemDescLabel.Text = String.Format("{0}", item.Description);
            ItemAffectsLabel.Text = String.Format("This item affects {0} with value {1}", item.Attribute.ToString(), item.Value.ToString());
            //set item image
            ItemPic.Source = item.ImageURI;
            //set selected item
            SelectedItem = item;
        }
    }
}