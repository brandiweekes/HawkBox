using System;

using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonsterEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private MonsterDetailViewModel _viewModel;

        //revert title on cancel button select
        private string oldTitle;

        public Monster Data { get; set; }

        public MonsterEditPage(MonsterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            //set old title for if cancel button selected
            oldTitle = viewModel.Title;
            //set page title
            viewModel.Title = "Edit " + viewModel.Data.Name;

            InitializeComponent(); 

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        //save monster event handler
	    private async void Save_Clicked(object sender, EventArgs e)
        {
            //set attributes, xp, and level # based on stepper labels
            Data.Attribute.Attack = Int32.Parse(AttackLabel.Text);
            Data.Attribute.Defense = Int32.Parse(DefenseLabel.Text);
            Data.Attribute.Speed = Int32.Parse(SpeedLabel.Text);
            Data.Attribute.MaxHealth = Int32.Parse(MaxHealthLabel.Text);
            Data.ExperienceTotal = Int32.Parse(XPLabel.Text);
            Data.Level = Int32.Parse(LevelLabel.Text);
            //set attribute string
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);
            //set remaining experience
            Data.ExperienceRemaining = Data.ExperienceTotal;

            //send message
            MessagingCenter.Send(this, "EditMonster", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        //cancel edit event handker
	    private async void Cancel_Clicked(object sender, EventArgs e)
        {
             //reset title
            _viewModel.Title = oldTitle;
            //pop page
            await Navigation.PopAsync();
        }
    }
}