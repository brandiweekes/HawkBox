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
        BattleViewModel viewModel;
        ItemLocationEnum location;
        Item EquippedItem;
        Item SelectedItem;

		public ItemSelectPage (ItemLocationEnum loc)
		{
			InitializeComponent ();

            BindingContext = viewModel = BattleViewModel.Instance;

            location = loc;
            EquippedItem = viewModel.pickedCharacter.GetItemByLocation(location);
            SelectedItem = null;

            if(EquippedItem != null)
            {
                ItemPic.Source = EquippedItem.ImageURI;
            }


            CharPic.Source = viewModel.pickedCharacter.ImageURI;
            AttackLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.Attack + viewModel.pickedCharacter.GetItemBonus(AttributeEnum.Attack));
            DefenseLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.Defense + viewModel.pickedCharacter.GetItemBonus(AttributeEnum.Defense));
            SpeedLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.Speed + viewModel.pickedCharacter.GetItemBonus(AttributeEnum.Speed));
            HPLabel.Text = String.Format("{0}", viewModel.pickedCharacter.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", viewModel.pickedCharacter.ExperienceTotal);

            ItemDescLabel.Text = "To add an item, press Equip then Save\nTo remove item, press Unequip and Save\n" +
                   "To discard changes, press Cancel\nSave must be pressed to finalize equipped or unequipped items.";
        }

        public ItemSelectPage(BattleViewModel vm)
        {
            InitializeComponent();

            BindingContext = viewModel = vm;


            CharPic.Source = BattleViewModel.Instance.pickedCharacter.ImageURI;
            AttackLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Attack 
                + BattleViewModel.Instance.pickedCharacter.GetItemBonus(AttributeEnum.Attack));
            DefenseLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Defense
                + BattleViewModel.Instance.pickedCharacter.GetItemBonus(AttributeEnum.Defense));
            SpeedLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Speed 
                + BattleViewModel.Instance.pickedCharacter.GetItemBonus(AttributeEnum.Speed));
            HPLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.ExperienceTotal);

            //ItemDescLabel.Text = "Select Item";
            //ItemAffectsLabel.Text = "Select Item";
        }

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
            //viewModel.BattleEngine.CharacterList.Remove(viewModel.pickedCharacter);
            viewModel.BattleEngine.CharacterList[viewModel.BattleEngine.CharacterList.FindIndex(ch => ch.Id.Equals(viewModel.pickedCharacter.Id))] = viewModel.pickedCharacter;

            viewModel.pickedCharacter = null;
            viewModel.AvailableItems.Clear();

            await Navigation.PopModalAsync();
            //await Navigation.PushModalAsync(new ItemLocationSelectPage());
        }

        public async void CancelButtonClicked(object sender, EventArgs e)
        {
            viewModel.pickedCharacter = null;
            viewModel.AvailableItems.Clear();
           
            await Navigation.PopModalAsync();
        }

        public void EquipButtonClicked(object sender, EventArgs e)
        {
            if(SelectedItem == null)
            {
                return;
            }

            if (EquippedItem == null)
            {
                EquippedItem = SelectedItem;
                viewModel.AvailableItems.Remove(SelectedItem);
                ItemPic.Source = EquippedItem.ImageURI;
            }
            else
            {
                viewModel.AvailableItems.Add(EquippedItem);
                EquippedItem = SelectedItem;
                viewModel.AvailableItems.Remove(SelectedItem);
                ItemPic.Source = EquippedItem.ImageURI;
            }

            ItemDescLabel.Text = String.Format("{0} equipped, press Save to finalize and return", EquippedItem.Name);
        }

        public void UnequipButtonClicked(object sender, EventArgs e)
        {
            if (EquippedItem != null)
            {
                viewModel.AvailableItems.Add(EquippedItem);
                ItemDescLabel.Text = String.Format("{0} unequipped, press Save to finalize and return", EquippedItem.Name);
                EquippedItem = null;
                ItemPic.Source = "https://screenshotlayer.com/images/assets/placeholder.png";
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
                return;

            ItemDescLabel.Text = String.Format("{0}", item.Description);
            ItemAffectsLabel.Text = String.Format("This item affects {0} with value {1}", item.Attribute.ToString(), item.Value.ToString());

            ItemPic.Source = item.ImageURI;

            SelectedItem = item;
        }
    }
}