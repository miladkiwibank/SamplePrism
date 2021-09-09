using System.Collections.Generic;
using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Services.Implementations.PrinterModule.ValueChangers
{
    interface IValueChanger<in T>
    {
        string Replace(PrinterTemplate template, string content, IEnumerable<T> models);
        string GetValue(PrinterTemplate template, T model);
    }
}
