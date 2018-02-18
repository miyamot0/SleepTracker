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

using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using SleepTracker.Controls;
using SleepTracker.Models;
using SleepTracker.Pages;

namespace SleepTracker.Views
{
    public class RootHomePage : MasterDetailPage
    {
        /// <summary>
        /// Accessible instance of Core Menu
        /// </summary>
        /// <value>The home menu.</value>
        public MenuPage HomeMenu { get; set; }

        /// <summary>
        /// Pages in memory
        /// </summary>
        /// <value>The pages.</value>
        Dictionary<int, NavigationPage> Pages { get; set;}

        /// <summary>
        /// Constructor
        /// </summary>
        public RootHomePage()
        {
            Pages = new Dictionary<int, NavigationPage>();

            HomeMenu = new MenuPage();

            Master = new StyledNavigationPage(HomeMenu, overrideLayout: true)
            {
                Title = Constants.Namings.Menu,
            };

            Title = Constants.Namings.Home;
            Icon = "slideout.png";

            Pages.Add((int)MenuType.Title, new StyledNavigationPage(new TitlePage()));

            Detail = Pages[(int)MenuType.Title];

            InvalidateMeasure();
        }

        /// <summary>
        /// Removes the page.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void RemovePage(int id)
        {
            Pages.Remove(id);
        }

        /// <summary>
        /// Navigates the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task NavigateAsync(int id)
        {
            if (Detail != null)
            {
                if (Device.Idiom != TargetIdiom.Tablet)
                {
                    IsPresented = false;
                }

                if (Device.RuntimePlatform == Device.Android)
                {
                    await Task.Delay(300);
                }
            }

            Page newPage;

            if (!Pages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int) MenuType.About:
                        Pages.Add(id, new StyledNavigationPage(new AboutPage()));

                        break;

                    case (int) MenuType.Title:
                        Pages.Add(id, new StyledNavigationPage(new TitlePage()));

                        break;

                    case (int)MenuType.Recording:
                        Pages.Add(id, new StyledNavigationPage(new RecordingPage()));

                        break;

                    case (int)MenuType.View:
                        Pages.Add(id, new StyledNavigationPage(new ViewRecordsPage()));
                        break;

                    case (int)MenuType.Help:
                        Pages.Add(id, new StyledNavigationPage(new HelpPage()));

                        break;

                    case (int)MenuType.Settings:
                        Pages.Add(id, new StyledNavigationPage(new SettingsPage()));

                        break;

                    case (int)MenuType.Export:
                        Pages.Add(id, new StyledNavigationPage(new ExportPage()));

                        break;

                    case (int) MenuType.Blank:
                        Pages.Add(id, new StyledNavigationPage(new BlankPage()));

                        break;

                    default:
                        newPage = null;

                        break;
                }
            }

            newPage = Pages[id];

            if (newPage == null)
            {
                return;
            }

            Detail = newPage;
        }
    }
}

