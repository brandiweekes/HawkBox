using Crawl.Models;
using Crawl.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDeletePage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private ItemDetailViewModel _viewModel;

        public Item Data { get; set; }

        public ItemDeletePage (ItemDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

	    private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        /// <summary>
        /// user clicks "Confirm Delete" button
        /// calls Async call to remove character
        /// remove delete page and detail page
        /// return to index page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteConfirmButton_Command(object sender, EventArgs e)
        {
            //removing monster from DataStore
            MessagingCenter.Send(this, "DeleteItem", Data);

            // Remove Monster Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }
    }
}