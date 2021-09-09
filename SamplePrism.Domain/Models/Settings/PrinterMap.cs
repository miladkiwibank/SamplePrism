using SamplePrism.Domain.Models.Menus;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Settings
{
    public class PrinterMap : ValueClass
    {
        public int PrintJobId { get; set; }
        public string MenuItemGroupCode { get; set; }
        public int MenuItemId { get; set; }
        public int PrinterId { get; set; }
        public int PrinterTemplateId { get; set; }
    }
}
