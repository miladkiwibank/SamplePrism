using System.Collections.Generic;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Services.Common;

namespace SamplePrism.Services
{
    public interface ISettingService
    {
        Terminal GetTerminalByName(string name);
        Terminal GetDefaultTerminal();
        IEnumerable<Terminal> GetTerminals();
        IProgramSettings ProgramSettings { get; }
        IProgramSetting GetProgramSetting(string settingName);
        IProgramSetting ReadSetting(string settingName);
        IProgramSetting ReadLocalSetting(string settingName);
        IProgramSetting ReadGlobalSetting(string settingName);
        string ReplaceSettingValues(string value, string template = "\\{:([^}]+)\\}"); 
        void SaveProgramSettings();
        int GetNextNumber(int numeratorId);
        string GetNextString(int numeratorId);
        void ResetCache();
        void ClearSettingCache();
    }
}
