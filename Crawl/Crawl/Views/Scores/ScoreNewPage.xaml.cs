using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreNewPage : ContentPage
    {
        public Score Data { get; set; }

        public ScoreNewPage()
        {
            InitializeComponent();
            //new default score
            Data = new Score();
            //default autobattle to true
            AutoBattlePicker.SelectedItem = "true";
            //set binding context
            BindingContext = this;
        }

        //create score event handler
        private async void Create_Clicked(object sender, EventArgs e)
        {
            //set autobattle with value from picker
            Data.AutoBattle = Boolean.Parse(AutoBattlePicker.SelectedItem.ToString());
            //send message
            MessagingCenter.Send(this, "AddScore", Data);
            //pop page
            await Navigation.PopAsync();
        }

        //cancel event handler
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //pop page
            await Navigation.PopAsync();
        }
    }
}