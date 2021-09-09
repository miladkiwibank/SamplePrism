using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SamplePrism.Domain.Models.Tasks;
using SamplePrism.Persistance.Data;

namespace SamplePrism.Persistance.Implementations
{
    [Export(typeof(ITaskDao))]
    class TaskDao : ITaskDao
    {
        public void SaveTask(Task task)
        {
            Dao.Save(task);
        }

        public IEnumerable<Task> GetTasks(int taskTypeId, DateTime endDate)
        {
            return Dao.Query<Task>(x => x.TaskTypeId == taskTypeId && (x.EndDate > endDate || !x.Completed), x => x.TaskTokens);
        }
    }
}
