using System.Collections.Generic;
using SamplePrism.Domain.Models.Tasks;

namespace SamplePrism.Presentation.Services.Implementations.TaskModule
{
    class TaskCache
    {
        public IEnumerable<Task> Tasks { get; set; }
        public TaskCache()
        {
            Tasks = new List<Task>();
        }
    }
}