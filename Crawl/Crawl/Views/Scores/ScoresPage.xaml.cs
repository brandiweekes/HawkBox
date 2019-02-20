using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoresPage : ContentPage
    {
        private ScoresViewModel _viewModel;

        public ScoresPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = ScoresViewModel.Instance;
            _viewModel.Title = "Scores Page";
        }

        // Add new score event. navigates to Add new Score Page
        private void AddScore(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScoreNewPage());
        }

        // On Score select event. navigates to score details page.
        private async void OnScoreSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is Score score))
                return;

            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(score)));

            // Manually deselect item.
            ScoresListView.SelectedItem = null;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            //reset toolbar for index page
            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.Clear();
            }

            //sets global binding
            InitializeComponent();

            // load or reload monsters on page
            if (_viewModel.Dataset.Count == 0 || _viewModel.NeedsRefresh())
            {
                _viewModel.LoadScoresCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }
    }
}