using System;

namespace SleepTracker.Models
{
    public enum MenuType
    {
        About,
        Title,
        Recording,
        View,
        Help,
        Settings,
        Export,
        Blank
    }

    public enum TargetType
    {
        Sleep,
        Bed,
        Blank
    }

    public class MenuItemModel : BaseModel
    {
        public MenuItemModel()
        {
            MenuType = MenuType.Title;
        }

        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
    }
}
