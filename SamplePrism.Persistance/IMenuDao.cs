using System.Collections.Generic;
using SamplePrism.Domain.Models.Menus;
using SamplePrism.Persistance.Common;

namespace SamplePrism.Persistance
{
    public interface IMenuDao
    {
        IEnumerable<ScreenMenu> GetScreenMenus();
        IEnumerable<string> GetMenuItemNames();
        IEnumerable<string> GetMenuItemGroupCodes();
        IEnumerable<string> GetMenuItemTags();
        IEnumerable<MenuItem> GetMenuItemsByGroupCode(string menuItemGroupCode);
        IEnumerable<MenuItem> GetMenuItems();
        IEnumerable<MenuItemData> GetMenuItemData();
        MenuItem GetMenuItemById(int id);
        IEnumerable<MenuItem> GetMenuItemsWithPortions();
    }
}
