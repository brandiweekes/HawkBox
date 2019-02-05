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

        private int oldAttack;
        private int oldDefense;
        private int oldSpeed;
        private int oldMaxHealth;
        private int oldXP;

        public Character Data { get; set; }

        public CharacterEditPage(CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Edit " + viewModel.Title;

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
            //Data.Attribute.Attack = newAttack;
            //Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);

            MessagingCenter.Send(this, "EditCharacter", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

	    private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Data.ExperienceTotal = oldXP;
            Data.Attribute.Attack = oldAttack;
            Data.Attribute.Defense = oldDefense;
            Data.Attribute.Speed = oldSpeed;
            Data.Attribute.MaxHealth = oldMaxHealth;
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);

            await Navigation.PopAsync();
        }

        //Steppers
        void OnAttackChanged(object sender, ValueChangedEventArgs e)
        {
            Data.Attribute.Attack = (int)e.NewValue;
            AttackLabel.Text = String.Format("{0}", Data.Attribute.Attack);
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);
        }

        void OnDefenseChanged(object sender, ValueChangedEventArgs e)
        {
            Data.Attribute.Defense = (int)e.NewValue;
            DefenseLabel.Text = String.Format("{0}", Data.Attribute.Defense);
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);
        }

        void OnSpeedChanged(object sender, ValueChangedEventArgs e)
        {
            Data.Attribute.Speed = (int)e.NewValue;
            SpeedLabel.Text = String.Format("{0}", Data.Attribute.Speed);
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);
        }

        void OnMaxHealthChanged(object sender, ValueChangedEventArgs e)
        {
            Data.Attribute.MaxHealth = (int)e.NewValue;
            MaxHealthLabel.Text = String.Format("{0}", Data.Attribute.MaxHealth);
            Data.AttributeString = AttributeBase.GetAttributeString(Data.Attribute);
        }

        void OnXPChanged(object sender, ValueChangedEventArgs e)
        {
            Data.ExperienceTotal = (int)e.NewValue;
            XPLabel.Text = String.Format("{0}", Data.ExperienceTotal);
        }
    }
}