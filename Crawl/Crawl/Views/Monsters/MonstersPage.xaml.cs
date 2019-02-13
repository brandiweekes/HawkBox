using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonstersPage : ContentPage
    {
        // ReSharper disable once NotAccessedField.Local
        private MonstersViewModel _viewModel;

        public MonstersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = MonstersViewModel.Instance;
            _viewModel.Title = "Agents Page";
        }

        /// <summary>
        /// toolbar item
        /// user selects this and navigates to New Monster Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMonster(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MonsterNewPage());
        }

        /// <summary>
        /// command for selecting a monster and navigating 
        /// to that monster's detail page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Monster monster))
                return;

            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(monster)));

            // Manually deselect item.
            MonstersListView.SelectedItem = null;
        }

        /// <summary>
        /// sets the index page with appropriate toolbar and monsters 
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            //reset toolbar for index page
            if (ToolbarItems.Count > 0)
                ToolbarItems.Clear();

            //sets global binding
            InitializeComponent();

            // load or reload monsters on page
            if (_viewModel.Dataset.Count == 0 || _viewModel.NeedsRefresh())
                _viewModel.LoadMonstersCommand.Execute(null);

            BindingContext = _viewModel;
        }

    }
}