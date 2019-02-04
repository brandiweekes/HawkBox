using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable RedundantExtendsListEntry

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterDetailPage : ContentPage
    {
        private CharacterDetailViewModel _viewModel;

        public CharacterDetailPage(CharacterDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
            SetCharacterStatus();
        }

        public CharacterDetailPage()
        {
            InitializeComponent();

            var data = new Character
            {
                Name = "Item 1",
                Description = "This is an item description.",
                Level = 1
            };

            _viewModel = new CharacterDetailViewModel(data);
            BindingContext = _viewModel;
            SetCharacterStatus();
        }

        private void SetCharacterStatus()
        {
            CharacterStatus.Text = _viewModel.Data.Alive ? "Alive" : "Dead";
        }

        public async void EditCharacter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterEditPage(_viewModel));
        }

        private async void DeleteCharacter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterDeletePage(_viewModel));
        }

        private async void CancelCharacter(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}