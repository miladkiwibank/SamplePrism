using System.Collections.Generic;
using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Persistance
{
    public interface IPrinterDao
    {
        IEnumerable<Printer> GetPrinters();
        IEnumerable<PrinterTemplate> GetPrinterTemplates();
    }
}
