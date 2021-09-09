using System;
using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Services.Implementations.PrinterModule
{
    public interface IDocumentFormatter
    {
        Type ObjectType { get; }
        string[] GetFormattedDocument(object item, PrinterTemplate printerTemplate);
    }
}