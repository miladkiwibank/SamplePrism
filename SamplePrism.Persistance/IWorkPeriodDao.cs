using System.Collections.Generic;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Persistance
{
    public interface IWorkPeriodDao
    {
        void StartWorkPeriod(string description,IWorkspace workspace);
        void StopWorkPeriod(string description, IWorkspace workspace);
        IEnumerable<WorkPeriod> GetLastWorkPeriods(int count);
    }
}
