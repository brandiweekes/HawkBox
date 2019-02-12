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
            Data = new Monster
            {
                Name = "Monster name",
                Description = "This is a Monster description.",
                ImageURI = HawkboxResources.Monsters_Male_Agent_A,
                Id = Guid.NewGuid().ToString(),
                Level = 1,
                ExperienceTotal = 100,
                Alive = true,
                Attribute = new AttributeBase(1, 1, 1, 10, 10),
                AttributeString = AttributeBase.GetAttributeString(new AttributeBase(1,1,1,10,10)),
                Head = "head",
                Feet = "feet",
                Necklace = "necklace",
                PrimaryHand = "primaryHand",
                OffHand = "offhand",
                RightFinger = "rightFinger",
                LeftFinger = "leftFinger"
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