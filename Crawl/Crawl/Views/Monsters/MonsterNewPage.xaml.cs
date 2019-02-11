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

        //Stepper handlers

        //Attack stepper
        void OnAttackChanged(object sender, ValueChangedEventArgs e)
        {
            //update attack label
            AttackLabel.Text = String.Format("{0}", (int)e.NewValue);
        }

        //defense stepper
        void OnDefenseChanged(object sender, ValueChangedEventArgs e)
        {
            //update defense label
            DefenseLabel.Text = String.Format("{0}", (int)e.NewValue);
        }

        //speed stepper
        void OnSpeedChanged(object sender, ValueChangedEventArgs e)
        {
            //update speed label
            SpeedLabel.Text = String.Format("{0}", (int)e.NewValue);
        }

        //maximum health stepper
        void OnMaxHealthChanged(object sender, ValueChangedEventArgs e)
        {
            //update max health label
            MaxHealthLabel.Text = String.Format("{0}", (int)e.NewValue);
        }

        //experience points stepper
        void OnXPChanged(object sender, ValueChangedEventArgs e)
        {
            //update XP label
            XPLabel.Text = String.Format("{0}", (int)e.NewValue);
        }

        //level number stepper
        void OnLevelChanged(object sender, ValueChangedEventArgs e)
        {
            //update level label
            LevelLabel.Text = String.Format("{0}", (int)e.NewValue);
        }
    }
}