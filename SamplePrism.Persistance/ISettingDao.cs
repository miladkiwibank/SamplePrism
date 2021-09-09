using System.Collections.Generic;
using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Persistance
{
    public interface ISettingDao
    {
        string GetNextString(int numeratorId);
        int GetNextNumber(int numeratorId);
        IEnumerable<Terminal> GetTerminals();
    }
}
