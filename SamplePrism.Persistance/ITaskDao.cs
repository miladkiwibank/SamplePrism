using System;
using System.Collections.Generic;
using SamplePrism.Domain.Models.Tasks;

namespace SamplePrism.Persistance
{
    public interface ITaskDao
    {
        void SaveTask(Task task);
        IEnumerable<Task> GetTasks(int taskTypeId, DateTime endDate);
    }
}
