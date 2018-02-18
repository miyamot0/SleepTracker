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

            // HACK
            App.IsInNightMode = true;

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
