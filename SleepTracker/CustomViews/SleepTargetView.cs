using System;

using Xamarin.Forms;
using SleepTracker.Models;

namespace SleepTracker.CustomViews
{
    public class SleepTargetView : ContentView
    {
        public Frame ContentFrame;
        private TargetType TargetType;

        public bool SelectedForDeletion { get; private set;}

        public SleepTargetView(TargetType targetType)
        {
            TargetType = targetType;

            ContentFrame = new Frame
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = GetStatusColor()
            };

            SelectedForDeletion = false;

            Content = ContentFrame;
        }

        /// <summary>
        /// Gets the color of the status.
        /// </summary>
        /// <returns>The status color.</returns>
        public Color GetStatusColor()
        {
            Color mColor;

            switch (TargetType)
            {
                case TargetType.Bed:
                    mColor = Color.FromHex("C5C5C5");
                    break;

                case TargetType.Sleep:
                    mColor = Color.FromHex("00A2D3");
                    break;

                default:
                    mColor = Color.FromHex("E3E3E3");
                    break;
            }

            return mColor;
        }

        /// <summary>
        /// Updates the color.
        /// </summary>
        public Color UpdatedColor(bool reset = false)
        {
            if (reset)
            {
                SelectedForDeletion = false;

                return GetStatusColor();
            }
            else if (App.IsDeleting && SelectedForDeletion)
            {
                SelectedForDeletion = false;

                return GetStatusColor();
            }
            else if (App.IsDeleting && !SelectedForDeletion)
            {
                SelectedForDeletion = true;

                return Color.FromHex("E74C3C");
            }
            else
            {
                SelectedForDeletion = false;

                return GetStatusColor();
            }
        }
    }
}

