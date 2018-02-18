/*

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
using SleepTracker.CustomViews;
using SleepTracker.Models;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SleepTracker.Storage;

namespace SleepTracker.Pages
{
    public partial class RecordingPage : ContentPage
    {
        public List<SleepInstanceView> sleepingSpans;
        public List<SleepInstanceView> downSpans;

        private string DateStringIndex = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");

        SleepDiagramView canvasView;

        /// <summary>
        /// 
        /// </summary>
        public RecordingPage()
        {
            InitializeComponent();  
        }

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            sleepingSpans = new List<SleepInstanceView>();
            downSpans = new List<SleepInstanceView>();

            base.OnAppearing();

            SetUpViewAsync();
        }

        /// <summary>
        /// Ons the disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            sleepStackLayout.Children.Clear();
            bedStackLayout.Children.Clear();

            labelBed.IsVisible = false;
            labelAsleep.IsVisible = false;

            base.OnDisappearing();
        }

        /// <summary>
        /// Sets up view async.
        /// </summary>
        private async void SetUpViewAsync()
        {
            ProgressDialogConfig config = new ProgressDialogConfig()
                .SetTitle("Querying Database")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black);

            using (IProgressDialog progress = UserDialogs.Instance.Progress(config))
            {
                await Task.Delay(300);
                   
                var existingData = await App.Database.GetSleepRecordsAsync(DateStringIndex);

                if (existingData != null && existingData.Count > 0)
                {
                    var sleepItems = existingData.Where(m => m.Type == Constants.Namings.SleepCode);

                    SleepInstanceView slider;

                    labelAsleep.IsVisible = true;
                    foreach (var item in sleepItems)
                    {
                        slider = new SleepInstanceView(TargetType.Sleep, Convert.ToSingle(item.Lower), Convert.ToSingle(item.Upper), ReddrawCanvasView);

                        sleepStackLayout.Children.Add(slider);
                        sleepingSpans.Add(slider);
                    }

                    var downItems = existingData.Where(m => m.Type == Constants.Namings.DownCode);

                    labelBed.IsVisible = true;
                    foreach (var item in downItems)
                    {
                        slider = new SleepInstanceView(TargetType.Bed, Convert.ToSingle(item.Lower), Convert.ToSingle(item.Upper), ReddrawCanvasView);

                        bedStackLayout.Children.Add(slider);
                        downSpans.Add(slider);
                    }
                }

                canvasView = new SleepDiagramView(sleepingSpans, downSpans);

                recordingGrid.Children.Add(canvasView, 0, 1);
            }
        }

        /// <summary>
        /// Handles the deletions.
        /// </summary>
        private void HandleDeletions()
        {
            var itemsToDelete = sleepingSpans.Where(m => m.RangeTargetView.SelectedForDeletion).ToList();

            if (itemsToDelete != null && itemsToDelete.Count() > 0)
            {
                foreach (var item in itemsToDelete)
                {
                    sleepingSpans.Remove(item);
                }

                foreach (var item in itemsToDelete)
                {
                    sleepStackLayout.Children.Remove(item);
                }
            }

            itemsToDelete = downSpans.Where(m => m.RangeTargetView.SelectedForDeletion).ToList();

            if (itemsToDelete != null && itemsToDelete.Count() > 0)
            {
                foreach (var item in itemsToDelete)
                {
                    downSpans.Remove(item);
                }

                foreach (var item in itemsToDelete)
                {
                    bedStackLayout.Children.Remove(item);
                }
            }

            ResetLayout(null, null);

            foreach (var item in sleepingSpans)
            {
                item.RangeTargetView.UpdatedColor(true);
            }

            foreach (var item in downSpans)
            {
                item.RangeTargetView.UpdatedColor(true);
            }
        }

        /// <summary>
        /// Handles the additions.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async void HandleAdditionsAsync(object sender, System.EventArgs e)
        {
            string result = await UserDialogs.Instance.ActionSheetAsync("What type of data do you want to add?",
                                                                        Constants.Namings.Cancel, Constants.Namings.Cancel, null,
                                                                        new string[] { Constants.Namings.Sleep, Constants.Namings.Down });

            SleepInstanceView newSlider = null;

            switch (result)
            {
                case Constants.Namings.Cancel:

                    return;

                case Constants.Namings.Down:
                    if (downSpans.Count == 0)
                    {
                        newSlider = new SleepInstanceView(TargetType.Bed, ReddrawCanvasView);
                    }
                    else
                    {
                        SleepInstanceView maxHeight = downSpans.Aggregate((agg, next) => next.RangeSliderView.RangeSlider.UpperValue > agg.RangeSliderView.RangeSlider.UpperValue ? next : agg);

                        newSlider = new SleepInstanceView(TargetType.Bed, maxHeight.RangeSliderView.RangeSlider.UpperValue, ReddrawCanvasView);

                        maxHeight = null;
                    }

                    bedStackLayout.Children.Add(newSlider);
                    downSpans.Add(newSlider);

                    ReddrawCanvasView();

                    break;

                case Constants.Namings.Sleep:
                    if (sleepingSpans.Count == 0)
                    {
                        newSlider = new SleepInstanceView(TargetType.Sleep, ReddrawCanvasView);
                    }
                    else
                    {
                        SleepInstanceView maxHeight = sleepingSpans.Aggregate((agg, next) => next.RangeSliderView.RangeSlider.UpperValue > agg.RangeSliderView.RangeSlider.UpperValue ? next : agg);

                        newSlider = new SleepInstanceView(TargetType.Sleep, maxHeight.RangeSliderView.RangeSlider.UpperValue, ReddrawCanvasView);

                        maxHeight = null;
                    }

                    sleepStackLayout.Children.Add(newSlider);
                    sleepingSpans.Add(newSlider);

                    ReddrawCanvasView();

                    break;
            }
        }

        /// <summary>
        /// Removes the slider.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ResetLayout(object sender, System.EventArgs e)
        {
            App.IsDeleting = !App.IsDeleting;

            if (App.IsDeleting)
            {
                toolBarEdit.Text = "Cancel Edit";
                editSliderButton.Text = "Remove Selected";
                editSliderButton.BackgroundColor = Color.Red;
            }
            else
            {
                toolBarEdit.Text = "Edit";
                editSliderButton.Text = "Save Sleep Data";
                editSliderButton.BackgroundColor = Color.Green;
            }
        }

        /// <summary>
        /// Reddraws the canvas view.
        /// </summary>
        void ReddrawCanvasView()
        {
            recordingGrid.Children.Remove(canvasView);
            canvasView = new SleepDiagramView(sleepingSpans, downSpans);
            recordingGrid.Children.Add(canvasView, 0, 1);
        }

        /// <summary>
        /// Commits the current action.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void CommitCurrentAction(object sender, System.EventArgs e)
        {
            if (App.IsDeleting)
            {
                HandleDeletions();

                ReddrawCanvasView();
            }
            else
            {
                App.Database.DeleteRecordsAsync(DateStringIndex);

                for (int i = 0; i < sleepingSpans.Count; i ++)
                {
                    await App.Database.SaveItemAsync(new SleepRecordModel()
                    {
                        Date = DateStringIndex,
                        Type = Constants.Namings.SleepCode,
                        Order = i,
                        Lower = (int)sleepingSpans[i].RangeSliderView.RangeSlider.LowerValue,
                        Upper = (int)sleepingSpans[i].RangeSliderView.RangeSlider.UpperValue,
                        DateSaved = DateTime.Now.ToString("yyyyMMddHHmmss")
                    });
                }

                for (int i = 0; i < downSpans.Count; i++)
                {
                    await App.Database.SaveItemAsync(new SleepRecordModel()
                    {
                        Date = DateStringIndex,
                        Type = Constants.Namings.DownCode,
                        Order = i,
                        Lower = (int)downSpans[i].RangeSliderView.RangeSlider.LowerValue,
                        Upper = (int)downSpans[i].RangeSliderView.RangeSlider.UpperValue,
                        DateSaved = DateTime.Now.ToString("yyyyMMddHHmmss")
                    });
                }

                App.RootPage.HomeMenu.ResetToBaseItem();

                App.RootPage.RemovePage((int)MenuType.Recording);
            }
        }
    }
}
