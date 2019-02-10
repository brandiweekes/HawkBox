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

        private void AddMonster(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MonsterNewPage());
        }

        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Monster monster))
                return;

            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(monster)));

            // Manually deselect item.
            MonstersListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
                ToolbarItems.Clear();

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0 || _viewModel.NeedsRefresh())
                _viewModel.LoadMonstersCommand.Execute(null);

            BindingContext = _viewModel;
        }

    }
}