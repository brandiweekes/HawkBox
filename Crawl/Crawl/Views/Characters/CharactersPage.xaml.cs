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

        private void AddCharacter(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CharacterNewPage());
        }

        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Character character))
                return;

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(character)));

            // Manually deselect item.
            CharactersListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0 || _viewModel.NeedsRefresh())
                _viewModel.LoadCharactersCommand.Execute(null);

            BindingContext = _viewModel;
        }
    }
}