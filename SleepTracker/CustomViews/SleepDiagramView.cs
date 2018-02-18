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
using SkiaSharp.Views.Forms;
using System.Linq;
using SkiaSharp;
using Xamarin.Forms;
using SleepTracker.Helpers;

namespace SleepTracker.CustomViews
{
    public class SleepDiagramView : SKCanvasView
    {
        private List<SleepInstanceView> SleepingSpans;
        private List<SleepInstanceView> DownSpans;

        private List<Tuple<double, double>> sleepingSpansTuple;
        private List<Tuple<double, double>> downSpansTuple;

        public event EventHandler DrawCompleted;

        public SKImage ImgData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDrawCompleted(EventArgs e)
        {
            if (DrawCompleted != null)
                DrawCompleted(this, e);
        }

        /// <summary>
        /// Gets the styled background.
        /// </summary>
        /// <value>The styled background.</value>
        public Color StyledBackground
        {
            get
            {
                return (Color)Application.Current.Resources["MainBackgroundColor"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sleepingSpans"></param>
        /// <param name="downSpans"></param>
        public SleepDiagramView(List<SleepInstanceView> sleepingSpans, List<SleepInstanceView> downSpans)
        {
            SleepingSpans = sleepingSpans.ToList();
            DownSpans = downSpans.ToList();

            Margin = new Xamarin.Forms.Thickness(0, 0);

            PaintSurface += CanvasView_ReDraw;

            SetBinding(ContentView.BackgroundColorProperty, new Binding("BackgroundColor"));
            BindingContext = new
            {
                BackgroundColor = StyledBackground
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void CanvasView_ReDraw(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            float hourlyWidth = Convert.ToSingle(info.Width) / 24f;

            float min, max, minInHours, maxInHours;

            canvas.Clear(SKColors.White);

            SKPaint sleepPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = ViewTools.DarkGray.ToSKColor(),
                StrokeWidth = 3,
                IsAntialias = true
            };

            SKPaint skPaint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColors.DarkBlue,
                StrokeWidth = 3,
                IsAntialias = true,
            };

            SKPaint gridPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Black.ToSKColor(),
                StrokeWidth = 3,
                IsAntialias = true,
            };

            foreach (var sleepingSpan in SleepingSpans)
            {
                min = Convert.ToSingle(sleepingSpan.RangeSliderView.RangeSlider.LowerValue);
                max = Convert.ToSingle(sleepingSpan.RangeSliderView.RangeSlider.UpperValue);

                minInHours = min / 60f;
                maxInHours = max / 60f;

                canvas.DrawRect(new SKRect(minInHours * hourlyWidth,
                                           info.Height,
                                           maxInHours * hourlyWidth,
                                           0), sleepPaint);
            }

            foreach (var downSpan in DownSpans)
            {
                min = Convert.ToSingle(downSpan.RangeSliderView.RangeSlider.LowerValue);
                max = Convert.ToSingle(downSpan.RangeSliderView.RangeSlider.UpperValue);

                minInHours = min / 60f;
                maxInHours = max / 60f;

                SKPoint[] skPointsList = new SKPoint[]
                {
                    //Point Bottom
                    new SKPoint(minInHours * hourlyWidth, info.Height),
                    //Point Right
                    new SKPoint(minInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(minInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),

                    //Point Center
                    new SKPoint(minInHours * hourlyWidth, info.Height / 2),

                    //Point Top
                    new SKPoint(minInHours * hourlyWidth, 0),
                    new SKPoint(minInHours * hourlyWidth, 0),

                    //Point Center
                    new SKPoint(minInHours * hourlyWidth, info.Height / 2),

                    //Point Left
                    new SKPoint(minInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(minInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),

                    //Point Bottom
                    new SKPoint(minInHours * hourlyWidth, info.Height),
                };

                canvas.DrawPoints(SKPointMode.Lines, skPointsList, skPaint);

                skPointsList = new SKPoint[]
                {
                    //Point Bottom
                    new SKPoint(maxInHours * hourlyWidth, 0),
                    //Point Right
                    new SKPoint(maxInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(maxInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),

                    //Point Center
                    new SKPoint(maxInHours * hourlyWidth, info.Height / 2),

                    //Point Top
                    new SKPoint(maxInHours * hourlyWidth, info.Height),
                    new SKPoint(maxInHours * hourlyWidth, info.Height),

                    //Point Center
                    new SKPoint(maxInHours * hourlyWidth, info.Height / 2),

                    //Point Left
                    new SKPoint(maxInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(maxInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),

                    //Point Bottom
                    new SKPoint(maxInHours * hourlyWidth, 0),
                };

                canvas.DrawPoints(SKPointMode.Lines, skPointsList, skPaint);
            }

            for (int i = 1; i <= 24; i++)
            {
                canvas.DrawRect(new SKRect(0, info.Height, hourlyWidth * i, 0), gridPaint);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sleepingSpans"></param>
        /// <param name="downSpans"></param>
        public SleepDiagramView(List<Tuple<double, double>> sleepingSpans, List<Tuple<double, double>> downSpans)
        {
            sleepingSpansTuple = sleepingSpans;
            downSpansTuple = downSpans;

            Margin = new Xamarin.Forms.Thickness(0, 0);

            PaintSurface += SleepDiagramView_PaintSurface;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void SleepDiagramView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            float hourlyWidth = Convert.ToSingle(info.Width) / 24f;

            float min, max, minInHours, maxInHours;

            canvas.Clear(SKColors.White);

            SKPaint sleepPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = ViewTools.DarkGray.ToSKColor(),
                StrokeWidth = 3,
                IsAntialias = true
            };

            SKPaint skPaint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColors.DarkBlue,
                StrokeWidth = 3,
                IsAntialias = true,
            };

            SKPaint gridPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Black.ToSKColor(),
                StrokeWidth = 3,
                IsAntialias = true,
            };

            foreach (var sleepingSpan in sleepingSpansTuple)
            {
                min = Convert.ToSingle(sleepingSpan.Item1);
                max = Convert.ToSingle(sleepingSpan.Item2);

                minInHours = min / 60f;
                maxInHours = max / 60f;

                canvas.DrawRect(new SKRect(minInHours * hourlyWidth,
                                           info.Height,
                                           maxInHours * hourlyWidth,
                                           0), sleepPaint);
            }

            foreach (var downSpan in downSpansTuple)
            {
                min = Convert.ToSingle(downSpan.Item1);
                max = Convert.ToSingle(downSpan.Item2);

                minInHours = min / 60f;
                maxInHours = max / 60f;

                SKPoint[] skPointsList = new SKPoint[]
                {
                    //Point Bottom
                    new SKPoint(minInHours * hourlyWidth, info.Height),
                    //Point Right
                    new SKPoint(minInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(minInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),

                    //Point Center
                    new SKPoint(minInHours * hourlyWidth, info.Height / 2),

                    //Point Top
                    new SKPoint(minInHours * hourlyWidth, 0),
                    new SKPoint(minInHours * hourlyWidth, 0),

                    //Point Center
                    new SKPoint(minInHours * hourlyWidth, info.Height / 2),

                    //Point Left
                    new SKPoint(minInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(minInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),

                    //Point Bottom
                    new SKPoint(minInHours * hourlyWidth, info.Height),
                };

                canvas.DrawPoints(SKPointMode.Lines, skPointsList, skPaint);

                skPointsList = new SKPoint[]
                {
                    //Point Bottom
                    new SKPoint(maxInHours * hourlyWidth, 0),
                    //Point Right
                    new SKPoint(maxInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(maxInHours * hourlyWidth + (hourlyWidth / 2), info.Height / 2),

                    //Point Center
                    new SKPoint(maxInHours * hourlyWidth, info.Height / 2),

                    //Point Top
                    new SKPoint(maxInHours * hourlyWidth, info.Height),
                    new SKPoint(maxInHours * hourlyWidth, info.Height),

                    //Point Center
                    new SKPoint(maxInHours * hourlyWidth, info.Height / 2),

                    //Point Left
                    new SKPoint(maxInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),
                    new SKPoint(maxInHours * hourlyWidth - (hourlyWidth / 2), info.Height / 2),

                    //Point Bottom
                    new SKPoint(maxInHours * hourlyWidth, 0),
                };

                canvas.DrawPoints(SKPointMode.Lines, skPointsList, skPaint);
            }

            for (int i = 1; i <= 24; i++)
            {
                canvas.DrawRect(new SKRect(0, info.Height, hourlyWidth * i, 0), gridPaint);
            }

            canvas.Flush();

            OnDrawCompleted(new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sleepingSpans"></param>
        /// <param name="downSpans"></param>
        /// <param name="holder"></param>
        public SleepDiagramView(List<Tuple<double, double>> sleepingSpans, List<Tuple<double, double>> downSpans, string holder)
        {
            sleepingSpansTuple = sleepingSpans;
            downSpansTuple = downSpans;

            int docHeight = 100;
            int docWidth = 1600;

            var surface = SKSurface.Create(docWidth, docHeight, SKImageInfo.PlatformColorType,  SKAlphaType.Premul);

            var surfWidth = surface.Snapshot().Width;
            var surfHeight = surface.Snapshot().Height;

            var barHeight = 75;

            SKCanvas canvas = surface.Canvas;

            float hourlyWidth = Convert.ToSingle(surfWidth - 300) / 24f;

            float min, max, minInHours, maxInHours;

            canvas.Clear(SKColors.White);

            SKPaint sleepPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = ViewTools.DarkGray.ToSKColor(),
                StrokeWidth = 3,
                IsAntialias = true
            };

            SKPaint skPaint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColors.DarkBlue,
                StrokeWidth = 3,
                IsAntialias = true,
            };

            SKPaint gridPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Black.ToSKColor(),
                StrokeWidth = 3,
                IsAntialias = true,
            };

            SKPaint writer = new SKPaint
            {
                TextSize = 16.0f,
                IsAntialias = true,
                Color = SKColors.Black,
                TextAlign = SKTextAlign.Center
            };

            foreach (var sleepingSpan in sleepingSpansTuple)
            {
                min = Convert.ToSingle(sleepingSpan.Item1);
                max = Convert.ToSingle(sleepingSpan.Item2);

                minInHours = min / 60f;
                maxInHours = max / 60f;

                canvas.DrawRect(new SKRect(minInHours * hourlyWidth,
                                           barHeight,
                                           maxInHours * hourlyWidth,
                                           0), sleepPaint);
            }

            foreach (var downSpan in downSpansTuple)
            {
                min = Convert.ToSingle(downSpan.Item1);
                max = Convert.ToSingle(downSpan.Item2);

                minInHours = min / 60f;
                maxInHours = max / 60f;

                SKPoint[] skPointsList = new SKPoint[]
                {
                    //Point Bottom
                    new SKPoint(minInHours * hourlyWidth, barHeight),
                    //Point Right
                    new SKPoint(minInHours * hourlyWidth + (hourlyWidth / 2), barHeight / 2),
                    new SKPoint(minInHours * hourlyWidth + (hourlyWidth / 2), barHeight / 2),

                    //Point Center
                    new SKPoint(minInHours * hourlyWidth, barHeight / 2),

                    //Point Top
                    new SKPoint(minInHours * hourlyWidth, 0),
                    new SKPoint(minInHours * hourlyWidth, 0),

                    //Point Center
                    new SKPoint(minInHours * hourlyWidth, barHeight / 2),

                    //Point Left
                    new SKPoint(minInHours * hourlyWidth - (hourlyWidth / 2), barHeight / 2),
                    new SKPoint(minInHours * hourlyWidth - (hourlyWidth / 2), barHeight / 2),

                    //Point Bottom
                    new SKPoint(minInHours * hourlyWidth, barHeight),
                };

                canvas.DrawPoints(SKPointMode.Lines, skPointsList, skPaint);

                skPointsList = new SKPoint[]
                {
                    //Point Bottom
                    new SKPoint(maxInHours * hourlyWidth, 0),
                    //Point Right
                    new SKPoint(maxInHours * hourlyWidth + (hourlyWidth / 2), barHeight / 2),
                    new SKPoint(maxInHours * hourlyWidth + (hourlyWidth / 2), barHeight / 2),

                    //Point Center
                    new SKPoint(maxInHours * hourlyWidth, barHeight / 2),

                    //Point Top
                    new SKPoint(maxInHours * hourlyWidth, barHeight),
                    new SKPoint(maxInHours * hourlyWidth, barHeight),

                    //Point Center
                    new SKPoint(maxInHours * hourlyWidth, barHeight / 2),

                    //Point Left
                    new SKPoint(maxInHours * hourlyWidth - (hourlyWidth / 2), barHeight / 2),
                    new SKPoint(maxInHours * hourlyWidth - (hourlyWidth / 2), barHeight / 2),

                    //Point Bottom
                    new SKPoint(maxInHours * hourlyWidth, 0),
                };

                canvas.DrawPoints(SKPointMode.Lines, skPointsList, skPaint);
            }

            for (int i = 1; i <= 24; i++)
            {
                canvas.DrawRect(new SKRect(0, barHeight, hourlyWidth * i, 0), gridPaint);

                canvas.DrawText(GetTranslatedTime(i-1), (hourlyWidth * i) - hourlyWidth / 2f, 100, writer);
            }

            ImgData = surface.Snapshot();

            OnDrawCompleted(new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetTranslatedTime(int value)
        {
            int hourActual = 18 + value;
            string timeDesc;

            if (value < 6)
            {
                hourActual = (hourActual > 23) ? hourActual - 24 : hourActual - 12;
                timeDesc = "PM";
            }
            else if (value == 6)
            {
                hourActual = 12;
                timeDesc = "AM";
            }
            else if (value >= 7 && value < 18)
            {
                hourActual = (hourActual > 23) ? hourActual - 24 : hourActual;
                timeDesc = "AM";
            }
            else
            {
                hourActual = (hourActual > 23) ? hourActual - 24 : hourActual;

                hourActual = (hourActual > 12) ? hourActual - 12 : hourActual;

                timeDesc = "PM";
            }

            return hourActual.ToString() + timeDesc;
        }
    }
}
