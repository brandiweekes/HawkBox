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
        private ScoreDetailViewModel _viewModel;

        public ScoreDetailPage(ScoreDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;

            ToggleLists();
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
            ToggleLists();
        }

        // Show and hide 'No Data' label based on list count
        private void ToggleLists()
        {
            
            if (_viewModel.Data.CharacterAtDeathList.Count > 0)
            {
                CharacterAtDeathListView.IsVisible = true;
                CharacterAtDeathLabel.IsVisible = false;
            }
            if (_viewModel.Data.MonstersKilledList.Count > 0)
            {
                MonstersKilledListView.IsVisible = true;
                MonstersKilledLabel.IsVisible = false;
            }
            if (_viewModel.Data.ItemsDroppedList.Count > 0)
            {
                ItemsDroppedListView.IsVisible = true;
                ItemsDroppedLabel.IsVisible = false;
            }
        }

        // Edit score event. navigates to edit score page. 
        private async void EditScore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreEditPage(_viewModel));
        }

        // Delete score event. navigates to delete score page.
        private async void DeleteScore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreDeletePage(_viewModel));
        }

        // Cancel score event. navigates to previous page by popping current page in navigation stack
        private async void CancelScore(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}