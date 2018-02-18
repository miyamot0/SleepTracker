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
using SkiaSharp;
using SleepTracker.Interfaces;
using System.Threading.Tasks;
using SleepTracker.CustomViews;
using System.Linq;
using System.Diagnostics;

namespace SleepTracker.Pages
{
    public partial class ExportPage : ContentPage
    {
        /// <summary>
        /// 
        /// </summary>
        public ExportPage()
        {
            InitializeComponent();
        }

        public static int Header = 125;
        public static int MarginLeft = 150;
        public int currentY = Header;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yDiff"></param>
        public void AddNewLine(int yDiff = 100)
        {
            currentY += yDiff;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var mBitMap = new SKBitmap(1600, 2200, isOpaque: false);
            var font = SKTypeface.FromFamilyName("Arial");

            var canvas = new SKCanvas(mBitMap);
            canvas.Clear(SKColors.White);

            var brush = new SKPaint
            {
                Typeface = font,
                TextSize = 16f,
                IsAntialias = true,
                Color = SKColors.Black
            };

            for (int i = 0; i < 7; i++)
            {
                canvas.DrawText(DateTime.Now.AddDays(-i).ToString("yyyyMMdd"), MarginLeft, currentY, brush);
                AddNewLine(22);

                var view = await GenerateSleepImage(DateTime.Now.AddDays(-i).ToString("yyyyMMdd"));

                if (view != null)
                {
                    Debug.WriteLine(view.ImgData == null);

                    canvas.DrawImage(view.ImgData, MarginLeft, currentY, null);

                    //35 diff

                    AddNewLine(135);
                }
                else
                {
                    Debug.WriteLine("Null image");
                }
            }

            canvas.Flush();

            var image = SKImage.FromBitmap(mBitMap);
            var data = image.Encode(SKEncodedImageFormat.Png, 90);

            DependencyService.Get<InterfaceSaveLoad>().SaveTempImage(data, "results.png");
        }

        /// <summary>
        /// Calculate the specified dateString.
        /// </summary>
        /// <returns>The calculate.</returns>
        /// <param name="dateString">Date string.</param>
        private async Task<SleepDiagramView> GenerateSleepView(string dateString)
        {
            var existingData = await App.Database.GetSleepRecordsAsync(dateString);

            List<Tuple<double, double>> sleepingSpans = new List<Tuple<double, double>>();
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
        /// 
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        private async Task<SleepDiagramView> GenerateSleepImage(string dateString)
        {
            var existingData = await App.Database.GetSleepRecordsAsync(dateString);

            List<Tuple<double, double>> sleepingSpans = new List<Tuple<double, double>>();
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

            return new SleepDiagramView(sleepingSpans, downSpans, dateString);
        }
    }
}
