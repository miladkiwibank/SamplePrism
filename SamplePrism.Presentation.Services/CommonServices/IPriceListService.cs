using System.Collections.Generic;
using SamplePrism.Domain.Models.Menus;
using SamplePrism.Persistance;
using SamplePrism.Persistance.Common;

namespace SamplePrism.Services
{
    public interface IPriceListService
    {
        void DeleteMenuItemPricesByPriceTag(string priceTag);
        void UpdatePriceTags(MenuItemPriceDefinition model);
        IEnumerable<string> GetTags();
        IEnumerable<PriceData> CreatePrices();
        void UpdatePrices(IList<PriceData> prices);
    }
}
