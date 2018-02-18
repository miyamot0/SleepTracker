using System;
using Xamarin.Forms;
using Xamarin.RangeSlider.Common;
using SleepTracker.Models;

namespace SleepTracker.Helpers
{
    public static class ViewTools
    {
        /// <summary>
        /// Gets the platform margins.
        /// </summary>
        /// <returns>The platform margins.</returns>
        public static Thickness GetPlatformMargins()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return new Thickness(10, 20, 10, 0);

                case Device.Android:
                    return new Thickness(10, 0, 10, 0);

                default:
                    return new Thickness(10, 0, 10, 0);
            }
        }

        /// <summary>
        /// Gets the padding.
        /// </summary>
        /// <returns>The padding.</returns>
        public static Thickness GetPadding()
        {
            return new Thickness(10);
        }

        /// <summary>
        /// Formats the date label.
        /// </summary>
        /// <returns>The date label.</returns>
        /// <param name="thumb">Thumb.</param>
        /// <param name="value">Value.</param>
        public static string FormatDateLabel(Thumb thumb, float value)
        {
            // The start window is 6pm to 6pm (yesterday to today)
            DateTime dateFromYesterdayAtSixPM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);
            dateFromYesterdayAtSixPM.AddDays(-1);

            // Set up label to build from the minutes supplied
            return dateFromYesterdayAtSixPM.AddMinutes(value).ToLocalTime().ToString("hh:mm tt");
        }

        /// <summary>
        /// Gets the target description.
        /// </summary>
        /// <returns>The target description.</returns>
        /// <param name="target">Target.</param>
        public static string GetTargetDescription(TargetType target)
        {
            switch (target)
            {
                case TargetType.Sleep:
                    return "Enter the time span when actually asleep";

                case TargetType.Bed:
                    return "Enter the time span when in bed/sleeping area";

                default:
                    return "";
            }
        }

        public static void SetStyle(bool isNightMode)
        {
            if (isNightMode)
            {
                App.Current.Resources["MainBarBackgroundColor"] = DarkGray;
                App.Current.Resources["MainBarTextColor"] = Color.White; 

                App.Current.Resources["MainBackgroundColor"] = LightGray;
                App.Current.Resources["MainTextColor"] = Color.White;


            }
            else
            {
                App.Current.Resources["MainBarBackgroundColor"] = Color.White;
                App.Current.Resources["MainBarTextColor"] = Color.Black;

                App.Current.Resources["MainBackgroundColor"] = Color.White;
                App.Current.Resources["MainTextColor"] = Color.Black;
            }
        }

        public static Color DarkGray = Color.FromHex("495867");
        public static Color LightGray = Color.FromHex("7B9EA8");
        public static Color LightGreen = Color.FromHex("6FD08C");
    }
}
