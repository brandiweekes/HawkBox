using System;

using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private CharacterDetailViewModel _viewModel; 

        //revert title on back or cancel button select
        private string oldTitle;

        public Character Data { get; set; }

        public CharacterEditPage(CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;

            //set oldtitle in case cancel button selected
            oldTitle = viewModel.Title;
            //set page title
            viewModel.Title = "Edit " + viewModel.Title;
    
            InitializeComponent();
            
            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        //save character event handler
	    public async void Save_Clicked(object sender, EventArgs e)
        {
            //set attribute values
            Data.Attribute.Attack = Int32.Parse(AttackLabel.Text);
            Data.Attribute.Defense = Int32.Parse(DefenseLabel.Text);
            Data.Attribute.Speed = Int32.Parse(SpeedLabel.Text);
            Data.Attribute.MaxHealth = Int32.Parse(MaxHealthLabel.Text);
            Data.ExperienceTotal = Int32.Parse(XPLabel.Text);

            //set attribute string
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);

            //send message
            MessagingCenter.Send(this, "EditCharacter", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }
       
        //cancel button event handler
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //reset title
            _viewModel.Title = oldTitle;

            //pop page
            await Navigation.PopAsync();
        }
    }
}