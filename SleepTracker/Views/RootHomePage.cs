using System;

using Xamarin.Forms;

namespace SleepTracker.Views
{
    public class RootHomePage : ContentPage
    {
        public RootHomePage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

