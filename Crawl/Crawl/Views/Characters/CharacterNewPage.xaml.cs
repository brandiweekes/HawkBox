using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterNewPage : ContentPage
    {
        public Character Data { get; set; }

        public CharacterNewPage()
        {
            InitializeComponent();

            //character with default values
            Data = new Character
            {
                Name = "Character Name",
                Description = "This is a Character description.",
                ImageURI = "http://gdurl.com/RxRK",
                Level =1,
                Id = Guid.NewGuid().ToString(),

                Attribute = new AttributeBase(1, 1, 1, 10 ,10),
                AttributeString = AttributeBase.GetAttributeString(new AttributeBase(1,1,1,10,10)),

                Head = "head",
                Feet = "feet",
                Necklace = "necklace",
                PrimaryHand = "primaryHand",
                OffHand = "offhand",
                RightFinger = "rightFinger",
                LeftFinger = "leftFinger"
            };

            //set BindingContext
            BindingContext = this;
        }

        //create new character event handler
        public async void Save_Clicked(object sender, EventArgs e)
        {
            //send message
            MessagingCenter.Send(this, "AddCharacter", Data);
            //pop page
            await Navigation.PopAsync();
        }

        //cancel button event handler
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //pop page
            await Navigation.PopAsync();
        }
    }
}