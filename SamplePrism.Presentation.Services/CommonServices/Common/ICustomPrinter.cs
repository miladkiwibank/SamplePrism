using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Services.Common
{
    public interface ICustomPrinter
    {
        string Name { get; }
        object GetSettingsObject(string customPrinterData);
        void Process(Printer printer, string document);
    }
}
