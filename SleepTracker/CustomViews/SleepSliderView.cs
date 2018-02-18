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

using SleepTracker.Helpers;
using Xamarin.Forms;
using Xamarin.RangeSlider.Forms;
using SleepTracker.Models;

namespace SleepTracker.CustomViews
{
    public class SleepSliderView : ContentView
    {
        public RangeSlider RangeSlider { get; private set; }
        public TargetType TargetType { get; private set; }

        private float DefaultStart = 180;
        private float DefaultSpan = 720;

        public Color StyledBackground
        {
            get
            {
                return (Color)Application.Current.Resources["MainBackgroundColor"];
            }
        }

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
        /// <param name="targetType"></param>
        public SleepSliderView(TargetType targetType)
        {
            SetBinding(ContentView.BackgroundColorProperty, new Binding("BackgroundColor"));
            BindingContext = new
            {
                BackgroundColor = StyledBackground
            };

            TargetType = targetType;

            RangeSlider = new RangeSlider();

            RangeSlider.SetBinding(RangeSlider.BackgroundColorProperty, new Binding("BackgroundColor"));
            RangeSlider.SetBinding(RangeSlider.TextColorProperty, new Binding("TextColor"));
            RangeSlider.BindingContext = new
            {
                BackgroundColor = StyledBackground,
                TextColor = StyledText
            };

            RangeSlider.MinimumValue = 0;
            RangeSlider.LowerValue = DefaultStart;
            RangeSlider.MaximumValue = 1440;
            RangeSlider.UpperValue = DefaultStart + DefaultSpan;
            RangeSlider.StepValue = 1;
            RangeSlider.VerticalOptions = LayoutOptions.Center;
            RangeSlider.TextSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
            RangeSlider.FormatLabel = ViewTools.FormatDateLabel;
            RangeSlider.ShowTextAboveThumbs = true;

            Content = new StackLayout()
            {
                Spacing = 0,
                Padding = new Thickness(10, 0, 0, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    RangeSlider
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="startValue"></param>
        public SleepSliderView(TargetType targetType, float startValue)
        {
            SetBinding(ContentView.BackgroundColorProperty, new Binding("BackgroundColor"));
            BindingContext = new
            {
                BackgroundColor = StyledBackground
            };

            TargetType = targetType;

            RangeSlider = new RangeSlider();

            RangeSlider.SetBinding(RangeSlider.BackgroundColorProperty, new Binding("BackgroundColor"));
            RangeSlider.SetBinding(RangeSlider.TextColorProperty, new Binding("TextColor"));
            RangeSlider.BindingContext = new
            {
                BackgroundColor = StyledBackground,
                TextColor = StyledText
            };

            RangeSlider.MinimumValue = 0;
            RangeSlider.LowerValue = startValue;
            RangeSlider.MaximumValue = 1440;
            RangeSlider.UpperValue = (startValue + DefaultSpan > 1440) ? 1440 : startValue + DefaultSpan;
            RangeSlider.StepValue = 1;
            RangeSlider.VerticalOptions = LayoutOptions.Center;
            RangeSlider.TextSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
            RangeSlider.FormatLabel = ViewTools.FormatDateLabel;
            RangeSlider.ShowTextAboveThumbs = true;

            Content = new StackLayout()
            {
                Spacing = 0,
                Padding = new Thickness(10, 0, 0, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    RangeSlider
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        public SleepSliderView(TargetType targetType, float startValue, float endValue)
        {
            SetBinding(ContentView.BackgroundColorProperty, new Binding("BackgroundColor"));
            BindingContext = new
            {
                BackgroundColor = StyledBackground
            };

            TargetType = targetType;

            RangeSlider = new RangeSlider();

            RangeSlider.SetBinding(RangeSlider.BackgroundColorProperty, new Binding("BackgroundColor"));
            RangeSlider.SetBinding(RangeSlider.TextColorProperty, new Binding("TextColor"));
            RangeSlider.BindingContext = new
            {
                BackgroundColor = StyledBackground,
                TextColor = StyledText
            };

            RangeSlider.MinimumValue = 0;
            RangeSlider.LowerValue = startValue;
            RangeSlider.MaximumValue = 1440;
            RangeSlider.UpperValue = endValue;
            RangeSlider.StepValue = 1;
            RangeSlider.VerticalOptions = LayoutOptions.Center;
            RangeSlider.TextSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
            RangeSlider.FormatLabel = ViewTools.FormatDateLabel;
            RangeSlider.ShowTextAboveThumbs = true;

            Content = new StackLayout()
            {
                Spacing = 0,
                Padding = new Thickness(10, 0, 0, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    RangeSlider
                }
            };
        }
    }
}

