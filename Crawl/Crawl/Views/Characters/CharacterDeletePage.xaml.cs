using Crawl.Models;
using Crawl.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterDeletePage : ContentPage
	{
	    private CharacterDetailViewModel _viewModel;

        public Character Data { get; set; }

        public CharacterDeletePage (CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Data.Name;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        /// <summary>
        /// user cancels a delete, 
        /// remove delete page from stack 
        /// return to details page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //remove Character from DataStore
            MessagingCenter.Send(this, "DeleteCharacter", Data);

            // Remove Character Details Page manually
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }
    }
}