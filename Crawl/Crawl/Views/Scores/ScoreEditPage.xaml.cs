using System;

using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScoreEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private ScoreDetailViewModel _viewModel;
        //save old title in case of cancel button
        private string oldTitle;

        public Score Data { get; set; }

        public ScoreEditPage(ScoreDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            //save oldtitle
            oldTitle = viewModel.Title;
            //set title
            viewModel.Title = "Edit " + viewModel.Data.Name;

            InitializeComponent();
            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
            //set autobattle picker selecteditem
            AutoBattlePicker.SelectedItem = Data.AutoBattle.ToString().ToLower() ;
        }

        //save score event handler
        private async void Save_Clicked(object sender, EventArgs e)
        {
            //set stepper values
            Data.ScoreTotal = Int32.Parse(ScoreLabel.Text);
            Data.ExperienceGainedTotal = Int32.Parse(XPLabel.Text);
            Data.RoundCount = Int32.Parse(RoundLabel.Text);
            Data.TurnCount = Int32.Parse(TurnLabel.Text);
            Data.MonsterSlainNumber = Int32.Parse(MonsterLabel.Text);
            //set autobattle picker
            Data.AutoBattle = Boolean.Parse(AutoBattlePicker.SelectedItem.ToString());
            //send message
            MessagingCenter.Send(this, "EditScore", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        //cancel event handler
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //revert to old title
            _viewModel.Title = oldTitle;
            //pop page
            await Navigation.PopAsync();
        }
    }
}