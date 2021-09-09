using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Services.Common
{
    public interface IWorkPeriodProcessor
    {
        void ProcessWorkPeriodStart(WorkPeriod workPeriod);
        void ProcessWorkPeriodEnd(WorkPeriod workPeriod);
    }
}