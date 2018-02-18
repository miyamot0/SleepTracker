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

using Xamarin.Forms;
using SleepTracker.Models;

namespace SleepTracker.CustomViews
{
    public class SleepTargetView : ContentView
    {
        public Frame ContentFrame;
        private TargetType TargetType;

        public bool SelectedForDeletion { get; private set;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
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

