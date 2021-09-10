﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Infrastructure.Data;
using SamplePrism.Persistance.Data;

namespace SamplePrism.Persistance.Implementations
{
    [Export(typeof(IWorkPeriodDao))]
    class WorkPeriodDao : IWorkPeriodDao
    {
        public void StartWorkPeriod(string description, IWorkspace workspace)
        {
            var latestWorkPeriod = workspace.Last<WorkPeriod>();
            if (latestWorkPeriod != null && latestWorkPeriod.StartDate == latestWorkPeriod.EndDate)
            {
                return;
            }

            var now = DateTime.Now;
            var newPeriod = new WorkPeriod
            {
                StartDate = now,
                EndDate = now,
                StartDescription = description,
            };

            workspace.Add(newPeriod);
            workspace.CommitChanges();
        }

        public void StopWorkPeriod(string description, IWorkspace workspace)
        {
            var period = workspace.Last<WorkPeriod>();
            if (period.EndDate == period.StartDate)
            {
                period.EndDate = DateTime.Now;
                period.EndDescription = description;
                workspace.CommitChanges();
            }
        }

        public IEnumerable<WorkPeriod> GetLastWorkPeriods(int count)
        {
            return Dao.Last<WorkPeriod>(count);
        }
    }
}
