using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterNewPage : ContentPage
    {
        public Monster Data { get; set; }

        public MonsterNewPage()
        {
            InitializeComponent();

            //default monster
            Data = new Monster()
            {
                Name = "Agent name",
                Description = "This is a Agent description."
            };
            //set binding context
            BindingContext = this;
        }

        //create monster event handler
        private async void Create_Clicked(object sender, EventArgs e)
        {
            //get attribute values from labels and set monster attributes
            Data.Attribute.Attack = Int32.Parse(AttackLabel.Text);
            Data.Attribute.Defense = Int32.Parse(DefenseLabel.Text);
            Data.Attribute.Speed = Int32.Parse(SpeedLabel.Text);
            Data.Attribute.MaxHealth = Int32.Parse(MaxHealthLabel.Text);
            Data.ExperienceTotal = Int32.Parse(XPLabel.Text);
            Data.Level = Int32.Parse(LevelLabel.Text);
            //set attribute string
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);
            //set remaining experience
            Data.ExperienceRemaining = Data.ExperienceTotal;

            //send message and pop page
            MessagingCenter.Send(this, "AddMonster", Data);
            await Navigation.PopAsync();
        }

        //cancel add event handler
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //pop page
            await Navigation.PopAsync();
        }
   
    }
}