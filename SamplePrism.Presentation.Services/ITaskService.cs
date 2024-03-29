﻿using System.Collections.Generic;
using SamplePrism.Domain.Models.Tasks;

namespace SamplePrism.Presentation.Services
{
    public interface ITaskService
    {
        Task AddNewTask(int taskTypeId, string taskContent, Dictionary<string, string> customFields, bool saveTask);
        IEnumerable<Task> GetTasks(int taskTypeId, int timeDelta = 0);
        IEnumerable<Task> SaveTasks(int taskTypeId, IEnumerable<Task> tasks, int timeDelta = 0);
    }
}
