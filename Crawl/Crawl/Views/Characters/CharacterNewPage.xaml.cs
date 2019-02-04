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

            Data = new Character
            {
                Name = "Character Name",
                Description = "This is a Character description.",
                ImageURI = "http://gdurl.com/RxRK",
                Level =1,
                Id = Guid.NewGuid().ToString(),

                Attribute = new AttributeBase(1, 1, 1, 10 ,10),
                //AttributeString = AttributeBase.GetAttributeString(Attribute),

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

        public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddCharacter", Data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}