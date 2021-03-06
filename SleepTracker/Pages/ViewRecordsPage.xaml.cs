﻿/*

Copyright (c) 2018 Shawn Patrick Gilroy, www.smallnstats.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;
using SleepTracker.CustomViews;
using System.Globalization;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace SleepTracker.Pages
{
    public partial class ViewRecordsPage : ContentPage
    {
        SleepDiagramView[] sleepViews;
        string[] commentStrings;
        string[] dateStrings;

        /// <summary>
        /// 
        /// </summary>
        public Color StyledText
        {
            get
            {
                return (Color)Application.Current.Resources["MainTextColor"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ViewRecordsPage()
        {
            InitializeComponent();

            LoadData();
        }

        /// <summary>
        /// On disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            recordsStackLayout.Children.Clear();
        }

        /// <summary>
        /// On appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadData();
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        public async void LoadData(int count = 7)
        {
            ProgressDialogConfig config = new ProgressDialogConfig()
                .SetTitle("Analyzing results")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black);

            using (IProgressDialog progress = UserDialogs.Instance.Progress(config))
            {
                await Task.Delay(300);

                sleepViews = new SleepDiagramView[count];
                commentStrings = new string[count];
                dateStrings = new string[count];

                for (int i = 0; i < count; i++)
                {
                    dateStrings[i] = DateTime.Now.AddDays(-i).ToString("yyyyMMdd");

                    sleepViews[i] = await GenerateSleepView(dateStrings[i]);
                    commentStrings[i] = "";
                }

                DrawFigures();
            }
        }

        /// <summary>
        /// Draws the figures.
        /// </summary>
        public void DrawFigures()
        {
            recordsStackLayout.Children.Clear();

            for (int i = 0; i < sleepViews.Length; i++)
            {
                Label dateLabel = new Label()
                {
                    Text = DateTime.ParseExact(dateStrings[i],
                                   "yyyyMMdd",
                                   CultureInfo.InvariantCulture).ToString("d")
                };

                dateLabel.SetBinding(Label.TextColorProperty, new Binding("TextColor"));
                dateLabel.BindingContext = new
                {
                    TextColor = StyledText
                };

                recordsStackLayout.Children.Add(dateLabel);

                recordsStackLayout.Children.Add(sleepViews[i]);

                Label commentLabel = new Label()
                {
                    Text = "Comments:" + commentStrings[i],
                    Margin = new Thickness(0, 0, 0, 10)
                };
                commentLabel.SetBinding(Label.TextColorProperty, new Binding("TextColor"));
                commentLabel.BindingContext = new
                {
                    TextColor = StyledText
                };

                recordsStackLayout.Children.Add(commentLabel);                
            }
        }

        /// <summary>
        /// Calculate the specified dateString.
        /// </summary>
        /// <returns>The calculate.</returns>
        /// <param name="dateString">Date string.</param>
        private async Task<SleepDiagramView> GenerateSleepView(string dateString)
        {
            var existingData = await App.Database.GetSleepRecordsAsync(dateString);

            List<Tuple<double,double>> sleepingSpans = new List<Tuple<double, double>>();
            List<Tuple<double, double>> downSpans = new List<Tuple<double, double>>();

            if (existingData != null && existingData.Count > 0)
            {
                var sleepItems = existingData.Where(m => m.Type == Constants.Namings.SleepCode);

                foreach (var item in sleepItems)
                {
                    sleepingSpans.Add(new Tuple<double, double>(item.Lower, item.Upper));
                }

                var downItems = existingData.Where(m => m.Type == Constants.Namings.DownCode);

                foreach (var item in downItems)
                {
                    downSpans.Add(new Tuple<double, double>(item.Lower, item.Upper));
                }
            }

            return new SleepDiagramView(sleepingSpans, downSpans);
        }

        /// <summary>
        /// Clicks the change range.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async void ClickChangeRange(object sender, System.EventArgs e)
        {
            string result = await UserDialogs.Instance.ActionSheetAsync("How many days back do you wish to see?",
                                                            Constants.Namings.Cancel, Constants.Namings.Cancel, null,
                                                            new string[] { Constants.Namings.Week, Constants.Namings.TwoWeeks, Constants.Namings.Month });
            switch (result)
            {
                case Constants.Namings.Week:
                    LoadData();
                    return;

                case Constants.Namings.TwoWeeks:
                    LoadData(int.Parse(Constants.Namings.TwoWeeks));
                    break;

                case Constants.Namings.Month:
                    LoadData(int.Parse(Constants.Namings.Month));
                    break;

                default:
                    LoadData();
                    break;
            }
        }
    }
}
