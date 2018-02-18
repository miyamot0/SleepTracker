using System;

using Xamarin.Forms;

namespace SleepTracker.CustomViews
{
    public class SleepTargetView : ContentPage
    {
        public SleepTargetView()
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

