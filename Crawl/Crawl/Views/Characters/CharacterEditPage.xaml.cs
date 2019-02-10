using System;

using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private CharacterDetailViewModel _viewModel;

        //old stepper values in case of back or cancel button selected
        private int oldAttack;
        private int oldDefense;
        private int oldSpeed;
        private int oldMaxHealth;
        private int oldXP;

        //revert title on back or cancel button select
        private string oldTitle;

        public Character Data { get; set; }

        public CharacterEditPage(CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;

            //set title
            oldTitle = viewModel.Title;
            viewModel.Title = "Edit " + viewModel.Title;

            //set old attribute values          
            oldXP = Data.ExperienceTotal;
            oldAttack = Data.Attribute.Attack;
            oldDefense = Data.Attribute.Defense;
            oldSpeed = Data.Attribute.Speed;
            oldMaxHealth = Data.Attribute.MaxHealth;
            
            InitializeComponent();
            
            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

	    public async void Save_Clicked(object sender, EventArgs e)
        {
            //set attribute values
            Data.Attribute.Attack = Int32.Parse(AttackLabel.Text);
            Data.Attribute.Defense = Int32.Parse(DefenseLabel.Text);
            Data.Attribute.Speed = Int32.Parse(SpeedLabel.Text);
            Data.Attribute.MaxHealth = Int32.Parse(MaxHealthLabel.Text);
            Data.ExperienceTotal = Int32.Parse(XPLabel.Text);

            //set attribute string
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);

            //send message
            MessagingCenter.Send(this, "EditCharacter", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }
       
        //cancel button event handler
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //reset attribute values           
            Data.ExperienceTotal = oldXP;
            Data.Attribute.Attack = oldAttack;
            Data.Attribute.Defense = oldDefense;
            Data.Attribute.Speed = oldSpeed;
            Data.Attribute.MaxHealth = oldMaxHealth;
            
            //reset title
            _viewModel.Title = oldTitle;

            //pop page
            await Navigation.PopAsync();
        }

        //Stepper handlers

        //Attack stepper
        void OnAttackChanged(object sender, ValueChangedEventArgs e)
        {
            //set attack label
            AttackLabel.Text = String.Format("{0}", Data.Attribute.Attack);
        }

        //defense stepper
        void OnDefenseChanged(object sender, ValueChangedEventArgs e)
        {
            //update defense label
            DefenseLabel.Text = String.Format("{0}", Data.Attribute.Defense);
        }

        //speed stepper
        void OnSpeedChanged(object sender, ValueChangedEventArgs e)
        {
            //update speed label
            SpeedLabel.Text = String.Format("{0}", Data.Attribute.Speed);
        }

        //maximum health stepper
        void OnMaxHealthChanged(object sender, ValueChangedEventArgs e)
        {
            //update max health label
            MaxHealthLabel.Text = String.Format("{0}", Data.Attribute.MaxHealth);
        }

        //experience points stepper
        void OnXPChanged(object sender, ValueChangedEventArgs e)
        {
            //update XP label
            XPLabel.Text = String.Format("{0}", Data.ExperienceTotal);
        }
    }
}