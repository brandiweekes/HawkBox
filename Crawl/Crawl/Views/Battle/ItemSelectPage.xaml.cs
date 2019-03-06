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

		public ItemSelectPage ()
		{
			InitializeComponent ();

            //BindingContext = view
		}

        public ItemSelectPage(BattleViewModel vm)
        {
            InitializeComponent();

            BindingContext = viewModel = vm;

            CharPic.Source = BattleViewModel.Instance.pickedCharacter.ImageURI;
            AttackLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Attack);
            DefenseLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Defense);
            SpeedLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.Speed);
            HPLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.Attribute.CurrentHealth);
            XPLabel.Text = String.Format("{0}", BattleViewModel.Instance.pickedCharacter.ExperienceTotal);

            //ItemDescLabel.Text = "Select Item";
            //ItemAffectsLabel.Text = "Select Item";
        }

        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ItemLocationSelectPage());
        }

        public async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
                return;

            ItemDescLabel.Text = String.Format("{0}", item.Description);
            ItemAffectsLabel.Text = String.Format("This item affects {0} with value {1}", item.Attribute.ToString(), item.Value.ToString());

            ItemPic.Source = item.ImageURI;
        }
    }
}