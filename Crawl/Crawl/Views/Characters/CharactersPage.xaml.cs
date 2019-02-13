using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharactersPage : ContentPage
    {
        private CharactersViewModel _viewModel;

        public CharactersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = CharactersViewModel.Instance;
            _viewModel.Title = "Aliens Page";
        }

        /// <summary>
        /// Toolbar item
        /// user selects this and navigates to New Character Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCharacter(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CharacterNewPage());
        }

        /// <summary>
        /// command for selecting a character and navigating 
        /// to that character's detail page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Character character))
                return;

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(character)));

            // Manually deselect item.
            CharactersListView.SelectedItem = null;
        }

        /// <summary>
        /// sets the index page with appropriate toolbar and characters 
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // reset toolbar for index page
            if (ToolbarItems.Count > 0)
                ToolbarItems.Clear();
            
            // set globals for binding
            InitializeComponent();

            // load or reload characters on page
            if (_viewModel.Dataset.Count == 0 || _viewModel.NeedsRefresh())
                _viewModel.LoadCharactersCommand.Execute(null);

            BindingContext = _viewModel;
        }
    }
}