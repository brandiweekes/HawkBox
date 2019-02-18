using System;
using Crawl.Models;
using Crawl.Controllers;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private ItemDetailViewModel _viewModel;

        //in case cancel button hit
        private string oldTitle;
        private ItemLocationEnum oldLocation;
        private AttributeEnum oldAttribute;
        // The data returned from the edit.
        public Item Data { get; set; }

        // The constructor takes a View Model
        // It needs to set the Picker values after doing the bindings.
        public ItemEditPage(ItemDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            //save title, location, attribute
            oldLocation = Data.Location;
            oldAttribute = Data.Attribute;
            oldTitle = viewModel.Title;
            //set title
            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = Data.Location.ToString();
            AttributePicker.SelectedItem = Data.Attribute.ToString();

        }

        // Save on the Tool bar
        private async void Save_Clicked(object sender, EventArgs e)
        {
            //set value 
            Data.Value = Int32.Parse(ValueLabel.Text);

            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = ItemsController.DefaultImageURI;
            }

            //send message
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //reset until I figure out another way
            Data.Attribute = oldAttribute;
            Data.Location = oldLocation;

            //reset title
            _viewModel.Title = oldTitle;
            //pop page
            await Navigation.PopAsync();
        }
    
    }
}