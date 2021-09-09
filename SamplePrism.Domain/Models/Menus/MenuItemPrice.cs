using System.ComponentModel.DataAnnotations;
using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Menus
{
    public class MenuItemPrice : ValueClass
    {
        public int MenuItemPortionId { get; set; }
        [StringLength(10)]
        public string PriceTag { get; set; }
        public decimal Price { get; set; }

        public string GetTrimmedPriceTag()
        {
            return string.IsNullOrWhiteSpace(PriceTag) ? null : PriceTag;
        }
    }
}
