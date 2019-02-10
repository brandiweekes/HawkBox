using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterDetailPage : ContentPage
    {
        private MonsterDetailViewModel _viewModel;

        public MonsterDetailPage(MonsterDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public MonsterDetailPage()
        {
            InitializeComponent();

            var data = new Monster
            {
                Name = "Agent Name",
                Description = "This is an item description.",
                Level = 1
            };

            _viewModel = new MonsterDetailViewModel(data);
            BindingContext = _viewModel;
        }

        private async void EditMonster(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterEditPage(_viewModel));
        }

        private async void DeleteMonster(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterDeletePage(_viewModel));
        }

        private async void CancelMonster(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}