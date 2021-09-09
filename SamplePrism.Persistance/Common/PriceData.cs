using SamplePrism.Domain.Models.Menus;

namespace SamplePrism.Persistance.Common
{
    public class PriceData
    {
        public MenuItemPortion Portion { get; set; }
        public string Name { get; set; }

        public PriceData(MenuItemPortion portion, string name)
        {
            Portion = portion;
            Name = name;
        }
    }
}