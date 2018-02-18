using System;
using System.Collections.Generic;
using SleepTracker.Helpers;
using Xamarin.Forms;

namespace SleepTracker.Controls
{
    public partial class StyledNavigationPage : NavigationPage
    {
        public StyledNavigationPage(ContentPage mainPage, bool overrideLayout = false) : base(mainPage)
        {
            InitializeComponent();

            if (!overrideLayout)
            {
                Padding = ViewTools.GetPlatformMargins();
            }
        }
    }
}
