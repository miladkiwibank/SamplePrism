using System.Collections.Generic;
using SamplePrism.Domain.Models.Automation;

namespace SamplePrism.Persistance
{
    public interface IAutomationDao
    {
        Dictionary<string, string> GetScripts();
        AppAction GetActionById(int appActionId);
        IEnumerable<string> GetAutomationCommandNames();
    }
}
