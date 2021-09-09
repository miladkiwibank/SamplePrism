using System.Windows.Documents;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Services.Implementations.PrinterModule.Formatters;
using SamplePrism.Services.Implementations.PrinterModule.Tools;

namespace SamplePrism.Services.Implementations.PrinterModule.PrintJobs
{
    public class CustomPrinterJob : AbstractPrintJob
    {
        private readonly IPrinterService _printerService;

        public CustomPrinterJob(Printer printer, IPrinterService printerService)
            : base(printer)
        {
            _printerService = printerService;
        }

        public override void DoPrint(string[] lines)
        {
            var q = PrinterInfo.GetPrinter(Printer.ShareName);
            var text = new FormattedDocument(lines, Printer.CharsPerLine).GetFormattedText();
            var customPrinter = _printerService.GetCustomPrinter(Printer.CustomPrinterName);
            customPrinter.Process(Printer, text);
        }

        public override void DoPrint(FlowDocument document)
        {
            throw new System.NotImplementedException();
        }
    }
}