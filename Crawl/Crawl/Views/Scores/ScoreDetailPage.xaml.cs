using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private ScoreDetailViewModel _viewModel;

        public ScoreDetailPage(ScoreDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;

            if(viewModel.Data.CharacterAtDeathList.Count > 0)
            {
                CharacterAtDeathListView.IsVisible = true;
                CharacterAtDeathLabel.IsVisible = false;
            }
            if (viewModel.Data.MonstersKilledList.Count > 0)
            {
                MonstersKilledListView.IsVisible = true;
                MonstersKilledLabel.IsVisible = false;
            }
            if (viewModel.Data.ItemsDroppedList.Count > 0)
            {
                ItemsDroppedListView.IsVisible = true;
                ItemsDroppedLabel.IsVisible = false;
            }
        }

        public ScoreDetailPage()
        {
            InitializeComponent();

            var data = new Score
            {
                Name = "Score name",
                Description = "Score Description",
                ScoreTotal = 0,
                GameDate = DateTime.Now
            };

            _viewModel = new ScoreDetailViewModel(data);
            BindingContext = _viewModel;
        }


        private async void EditScore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreEditPage(_viewModel));
        }

        private async void DeleteScore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreDeletePage(_viewModel));
        }

        private async void CancelScore(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}