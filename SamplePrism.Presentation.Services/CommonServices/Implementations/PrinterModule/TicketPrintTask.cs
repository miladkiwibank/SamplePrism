using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Services.Implementations.PrinterModule
{
    public class TicketPrintTask
    {
        public Printer Printer { get; set; }
        public string[] Lines { get; set; }
    }
}