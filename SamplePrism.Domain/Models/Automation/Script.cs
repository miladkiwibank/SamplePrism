using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Automation
{
    public class Script : EntityClass
    {
        public string HandlerName { get; set; }
        public string Code { get; set; }
    }
}
