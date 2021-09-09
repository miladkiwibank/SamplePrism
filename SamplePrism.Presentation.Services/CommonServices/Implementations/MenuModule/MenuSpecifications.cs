using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamplePrism.Domain.Models.Inventory;
using SamplePrism.Domain.Models.Menus;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Persistance.Specification;

namespace SamplePrism.Services.Implementations.MenuModule
{
    public static class MenuSpecifications
    {


        public static Specification<ScreenMenuItem> ScreenMenuItemsByMenuItemId(int menuItemId)
        {
            return new DirectSpecification<ScreenMenuItem>(x => x.MenuItemId == menuItemId);
        }

        public static Specification<Recipe> RecipesByMenuItemId(int menuItemId)
        {
            return new DirectSpecification<Recipe>(x => x.Portion.MenuItemId == menuItemId);
        }

        public static Specification<OrderTag> OrderTagsByMenuItemId(int menuItemId)
        {
            return new DirectSpecification<OrderTag>(x=>x.MenuItemId == menuItemId);
        }
    }
}
