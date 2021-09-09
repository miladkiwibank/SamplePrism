using System;
using System.Collections.Generic;
using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Presentation.Services
{
    public interface IWorkPeriodService 
    {
        bool StartWorkPeriod(string description);
        bool StopWorkPeriod(string description);
        IEnumerable<WorkPeriod> GetLastWorkPeriods(int count);
        DateTime GetWorkPeriodStartDate();
    }
}
