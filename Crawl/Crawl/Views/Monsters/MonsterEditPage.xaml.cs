using System;

using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonsterEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private MonsterDetailViewModel _viewModel;

        //old stepper values in case of back or cancel button selected
        private int oldAttack;
        private int oldDefense;
        private int oldSpeed;
        private int oldMaxHealth;
        private int oldXP;
        private int oldLevel;

        //revert title on back or cancel button select
        private string oldTitle;

        public Monster Data { get; set; }

        public MonsterEditPage(MonsterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;

            oldTitle = viewModel.Title;
            viewModel.Title = "Edit " + viewModel.Title;

            //set old attribute values          
            oldXP = Data.ExperienceTotal;
            oldAttack = Data.Attribute.Attack;
            oldDefense = Data.Attribute.Defense;
            oldSpeed = Data.Attribute.Speed;
            oldMaxHealth = Data.Attribute.MaxHealth;
            oldLevel = Data.Level;

            InitializeComponent();
            

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

	    private async void Save_Clicked(object sender, EventArgs e)
        {
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

            MessagingCenter.Send(this, "EditMonster", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

	    private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //reset attribute values           
            Data.ExperienceTotal = oldXP;
            Data.Attribute.Attack = oldAttack;
            Data.Attribute.Defense = oldDefense;
            Data.Attribute.Speed = oldSpeed;
            Data.Attribute.MaxHealth = oldMaxHealth;
            Data.Level = oldLevel;

            //reset title
            _viewModel.Title = oldTitle;

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