using System;
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

