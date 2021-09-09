using System.ComponentModel.DataAnnotations;
using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Settings
{
    public class ProgramSettingValue : EntityClass
    {
        [StringLength(250)]
        public string Value { get; set; }
    }
}
