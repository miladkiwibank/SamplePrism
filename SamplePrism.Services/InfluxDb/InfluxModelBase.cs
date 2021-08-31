using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;

namespace SamplePrism.Services.InfluxDb
{
    public abstract class InfluxModelBase : ICloneable
    {

        [InfluxTimestamp]
        public DateTime Timestamp { get; set; }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
