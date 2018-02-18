using System;
using System.Collections.Generic;
using SleepTracker.Models;
using Xamarin.Forms;
using System.Diagnostics;
using SleepTracker.Helpers;

namespace SleepTracker.Views
{
    public partial class MenuPage : ContentPage
    {
        List<MenuItemModel> menuItems;

        public MenuPage()
        {
            InitializeComponent();

            Title = Constants.Namings.Menu;
            //Subtitle = Constants.Namings.Menu,
            Icon = "slideout.png";

            /*
            BindingContext = new BaseViewModel
            {

            };
            */

            Padding = ViewTools.GetPlatformMargins();

            ListViewMenu.ItemsSource = menuItems = new List<MenuItemModel>
            {
                new MenuItemModel { 
                    Title = "Home",  
                    MenuType = MenuType.Title, 
                    Icon = "hm.png" },
                
                new MenuItemModel { 
                    Title = "Record Sleep",   
                    MenuType = MenuType.Recording, 
                    Icon = "hm.png" },
                
                new MenuItemModel { 
                    Title = "View Records",   
                    MenuType = MenuType.View, 
                    Icon = "hm.png" },
                
                new MenuItemModel { 
                    Title = "Export",   
                    MenuType = MenuType.Export, 
                    Icon = "hm.png" },
                
                new MenuItemModel { 
                    Title = "Help",   
                    MenuType = MenuType.Help, 
                    Icon = "hm.png"},
                
                new MenuItemModel { 
                    Title = "Settings",   
                    MenuType = MenuType.Settings, 
                    Icon = "hm.png"},
                
                new MenuItemModel { 
                    Title = "About", 
                    MenuType = MenuType.About, 
                    Icon ="hm.png" },
            };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (ListViewMenu.SelectedItem == null)
                {
                    return;
                }

                var itemModel = e.SelectedItem as MenuItemModel;

                if (itemModel == null)
                {
                    return;                    
                }

                ListViewMenu.SelectedItem = null;

                await App.RootPage.NavigateAsync((int)((MenuItemModel)e.SelectedItem).MenuType);
            };
        }

        public void ResetToBaseItem()
        {
            ListViewMenu.SelectedItem = menuItems[0];
        }
    }
}