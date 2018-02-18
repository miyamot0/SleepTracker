using System;
using SleepTracker.Models;
using Xamarin.Forms;
using SleepTracker.Helpers;

namespace SleepTracker.CustomViews
{
    public class SleepDescriptionView : ContentView
    {
        public Color StyledBackground
        {
            get
            {
                return (Color) Application.Current.Resources["MainBackgroundColor"];
            }
        }

        public Color StyledText
        {
            get
            {
                return (Color)Application.Current.Resources["MainTextColor"];
            }
        }

        public SleepDescriptionView(TargetType targetType)
        {
            SetBinding(ContentView.BackgroundColorProperty, new Binding("BackgroundColor"));
            BindingContext = new
            {
                BackgroundColor = StyledBackground
            };

            Label contentLabel = new Label()
            {
                Text = ViewTools.GetTargetDescription(targetType),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("383838")
            };

            contentLabel.SetBinding(Label.TextColorProperty, new Binding("TextColor"));
            contentLabel.BindingContext = new
            {
                TextColor = StyledText
            };

            Content = new StackLayout()
            {
                Padding = new Thickness(5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    contentLabel
                }
            };
        }
    }
}

