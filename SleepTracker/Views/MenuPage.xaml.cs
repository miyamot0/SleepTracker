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

using System.Collections.Generic;
using SleepTracker.Models;
using Xamarin.Forms;
using SleepTracker.Helpers;

namespace SleepTracker.Views
{
    public partial class MenuPage : ContentPage
    {
        List<MenuItemModel> menuItems;

        /// <summary>
        /// 
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();

            Title = Constants.Namings.Menu;
            Icon = "slideout.png";

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

        /// <summary>
        /// 
        /// </summary>
        public void ResetToBaseItem()
        {
            ListViewMenu.SelectedItem = menuItems[0];
        }
    }
}