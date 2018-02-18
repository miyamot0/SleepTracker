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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
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

