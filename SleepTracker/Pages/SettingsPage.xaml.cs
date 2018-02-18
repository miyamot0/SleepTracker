using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using SleepTracker.Helpers;

namespace SleepTracker.Pages
{
    public partial class SettingsPage : ContentPage
    {
        private bool preparingView = true;

        public SettingsPage()
        {
            InitializeComponent();

            nightMode.IsToggled = App.IsInNightMode;
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (preparingView)
            {
                preparingView = false;

                return;
            }

            App.IsInNightMode = !App.IsInNightMode;

            ViewTools.SetStyle(App.IsInNightMode);

            Debug.WriteLine("toggled");

//            App.Current.Resources["MainBarBackgroundColor"] = Color.White;
//            App.Current.Resources["MainBarTextColor"] = Color.Black;


/*
            <Color x:Key="MainBarBackgroundColor">#33302E</Color>
            <Color x:Key="MainBarTextColor">White</Color>
*/
            //throw new NotImplementedException();
        }
    }
}
