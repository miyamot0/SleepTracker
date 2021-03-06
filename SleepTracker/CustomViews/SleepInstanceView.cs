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
using Xamarin.Forms;
using SleepTracker.Models;
using SleepTracker.Helpers;
using System.Diagnostics;

namespace SleepTracker.CustomViews
{
    public class SleepInstanceView : ContentView
    {
        private Action ReddrawCanvasView;

        public SleepSliderView RangeSliderView { get; set; }
        public SleepTargetView RangeTargetView { get; set; }

        public Color StyledBackground
        {
            get
            {
                return (Color)Application.Current.Resources["MainBackgroundColor"];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SleepTracker.CustomViews.SleepInstanceView"/> class.
        /// </summary>
        /// <param name="targetType">Target type.</param>
        public SleepInstanceView(TargetType targetType, Action reddrawCanvasView)
        {
            ReddrawCanvasView = reddrawCanvasView;

            SetBinding(ContentView.BackgroundColorProperty, new Binding("BackgroundColor"));
            BindingContext = new
            {
                BackgroundColor = StyledBackground
            };

            Grid coreGrid = new Grid
            {
                Padding = new Thickness(0, 1, 1, 1),
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.FromHex("E3E3E3"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (100, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (4, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (50, GridUnitType.Absolute) }
                }
            };

            RangeTargetView = new SleepTargetView(targetType);

            coreGrid.Children.Add(RangeTargetView, 0, 1, 0, 2);

            RangeSliderView = new SleepSliderView(targetType);

            coreGrid.Children.Add(RangeSliderView, 1, 5, 0, 1);
            coreGrid.Children.Add(new SleepDescriptionView(targetType), 1, 5, 1, 2);

            Content = coreGrid;

            var tgr = CreateRateTapGestureRecognizer();

            GestureRecognizers.Add(tgr);
            RangeTargetView.GestureRecognizers.Add(tgr);

            RangeSliderView.RangeSlider.DragCompleted += RangeSlider_DragCompleted;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SleepTracker.CustomViews.SleepInstanceView"/> class.
        /// </summary>
        /// <param name="targetType">Target type.</param>
        /// <param name="startValue">Start value.</param>
        public SleepInstanceView(TargetType targetType, float startValue, Action reddrawCanvasView)
        {
            ReddrawCanvasView = reddrawCanvasView;

            Grid coreGrid = new Grid
            {
                Padding = new Thickness(0, 1, 1, 1),
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.FromHex("E3E3E3"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (100, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (4, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (50, GridUnitType.Absolute) }
                }
            };

            RangeTargetView = new SleepTargetView(targetType);

            coreGrid.Children.Add(RangeTargetView, 0, 1, 0, 2);

            RangeSliderView = new SleepSliderView(targetType, startValue);

            coreGrid.Children.Add(RangeSliderView, 1, 5, 0, 1);
            coreGrid.Children.Add(new SleepDescriptionView(targetType), 1, 5, 1, 2);

            Content = coreGrid;

            var tgr = CreateRateTapGestureRecognizer();

            GestureRecognizers.Add(tgr);
            RangeTargetView.GestureRecognizers.Add(tgr);

            RangeSliderView.RangeSlider.DragCompleted += RangeSlider_DragCompleted;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SleepTracker.CustomViews.SleepInstanceView"/> class.
        /// </summary>
        /// <param name="targetType">Target type.</param>
        /// <param name="startValue">Start value.</param>
        /// <param name="reddrawCanvasView">Reddraw canvas view.</param>
        public SleepInstanceView(TargetType targetType, float startValue, float endValue, Action reddrawCanvasView)
        {
            ReddrawCanvasView = reddrawCanvasView;

            Grid coreGrid = new Grid
            {
                Padding = new Thickness(0, 1, 1, 1),
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.FromHex("E3E3E3"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (100, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (4, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (50, GridUnitType.Absolute) }
                }
            };

            RangeTargetView = new SleepTargetView(targetType);

            coreGrid.Children.Add(RangeTargetView, 0, 1, 0, 2);

            RangeSliderView = new SleepSliderView(targetType, startValue, endValue);

            coreGrid.Children.Add(RangeSliderView, 1, 5, 0, 1);
            coreGrid.Children.Add(new SleepDescriptionView(targetType), 1, 5, 1, 2);

            Content = coreGrid;

            var tgr = CreateRateTapGestureRecognizer();

            GestureRecognizers.Add(tgr);
            RangeTargetView.GestureRecognizers.Add(tgr);

            RangeSliderView.RangeSlider.DragCompleted += RangeSlider_DragCompleted;
        }

        /// <summary>
        /// Ranges the slider drag completed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void RangeSlider_DragCompleted(object sender, EventArgs e)
        {
            ReddrawCanvasView();
        }

        /// <summary>
        /// Creates the rate tap gesture recognizer.
        /// </summary>
        /// <returns>The rate tap gesture recognizer.</returns>
        private TapGestureRecognizer CreateRateTapGestureRecognizer()
        {
            TapGestureRecognizer tgr = new TapGestureRecognizer();
            tgr.Tapped += async (s, e) =>
            {
                Debug.WriteLine("Tapped");

                if (!App.IsDeleting)
                {
                    return;
                }

                SleepInstanceView baseView = s as SleepInstanceView;

                if (baseView != null)
                {
                    var tempColorHolder = baseView.RangeTargetView.UpdatedColor();
                    await baseView.RangeTargetView.ContentFrame.ColorTo(tempColorHolder);
                }
            };

            return tgr;
        }
    }
}

