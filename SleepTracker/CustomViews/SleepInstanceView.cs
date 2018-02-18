using System;

using Xamarin.Forms;
using SleepTracker.Models;

namespace SleepTracker.CustomViews
{
    public class SleepingInstanceView : ContentView
    {
        public SleepingInstanceView(TargetType targetType)
        {
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

            coreGrid.Children.Add(new SleepTargetView(targetType), 0, 1, 0, 2);
            coreGrid.Children.Add(new SleepSliderView(targetType), 1, 5, 0, 1);
            coreGrid.Children.Add(new SleepDescriptionView(targetType), 1, 5, 1, 2);

            Content = coreGrid;

        }
    }
}

