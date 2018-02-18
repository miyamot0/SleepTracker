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
using SleepTracker.Views;
using SleepTracker.Storage;
using SleepTracker.Interfaces;
using System;
using SleepTracker.Helpers;

namespace SleepTracker
{
    public partial class App : Application
    {
        /// <summary>
        /// Checking to determine if should be deleting
        /// </summary>
        public static bool IsDeleting = false;

        /// <summary>
        /// Check if minimize brightness
        /// </summary>
        public static bool IsInNightMode = false;

        /// <summary>
        /// Instance to access core view
        /// </summary>
        public static RootHomePage RootPage;

        /// <summary>
        /// Database singleton
        /// </summary>
        private static ApplicationDatabase database;
        public static ApplicationDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ApplicationDatabase(DependencyService.Get<InterfaceSaveLoad>().GetLocalFilePath("Database.db3"));
                }

                return database;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Force database to prepare
            Database.Init();

            // Night mode toggle
            if (DateTime.Now.Hour > 21 || DateTime.Now.Hour < 7)
            {
                App.IsInNightMode = true;
            }

            ViewTools.SetStyle(App.IsInNightMode);

            RootPage = new RootHomePage();

            MainPage = RootPage;
        }

        /// <summary>
        /// On start.
        /// </summary>
        protected override void OnStart() { }

        /// <summary>
        /// On sleep.
        /// </summary>
        protected override void OnSleep() { }

        /// <summary>
        /// On resume.
        /// </summary>
        protected override void OnResume() { }
    }
}
