using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.Controllers;
using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemNewPage : ContentPage
    {
        public Item Data { get; set; }

        // Constructor for the page, will create a new black item that can tehn get updated
        public ItemNewPage()
        {
            InitializeComponent();

            //default item
            Data = new Item()
            {
                Name = "Item Name",
                Description = "Item Description"
            };

            BindingContext = this;
            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = Data.Location.ToString();
            AttributePicker.SelectedItem = Data.Attribute.ToString();
        }

        // Respond to the Save click
        // Send the add message to so it gets added...
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
            MessagingCenter.Send(this, "AddData", Data);
            //pop page
            await Navigation.PopAsync();
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //pop page
            await Navigation.PopAsync();
        }

    }
}