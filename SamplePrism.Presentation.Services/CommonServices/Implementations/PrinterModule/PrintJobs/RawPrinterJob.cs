using System;
using System.Windows.Documents;
using System.Windows.Media;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Services.Implementations.PrinterModule.Formatters;
using SamplePrism.Services.Implementations.PrinterModule.Tools;

namespace SamplePrism.Services.Implementations.PrinterModule.PrintJobs
{
    public class RawPrinterJob : AbstractPrintJob
    {
        public RawPrinterJob(Printer printer)
            : base(printer)
        {

        }

        public override void DoPrint(string[] lines)
        {
            var text = string.Join(Environment.NewLine, lines);
            RawPrinterHelper.SendStringToPrinter(Printer.ShareName, text+Environment.NewLine);
        }

        public override void DoPrint(FlowDocument document)
        {
            return;
        }
    }
}