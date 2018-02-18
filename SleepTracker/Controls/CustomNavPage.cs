using System;
using Xamarin.Forms;
using SleepTracker.Helpers;

namespace SleepTracker.Controls
{
    public class CustomNavPage : NavigationPage
    {
        public CustomNavPage(Page root, bool overrideLayout = false) : base(root)
        {
            Init(overrideLayout);
        }

        public CustomNavPage(bool overrideLayout = false)
        {
            Init(overrideLayout);
        }

        void Init(bool overrideLayout)
        {
            if (!overrideLayout)
            {
                Padding = ViewTools.GetPlatformMargins();   
            }
        }
    }
}
