using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SamplePrism.Services.InfluxConfig;

namespace SamplePrism.Services.InfluxDb
{
    public abstract class InfluxServiceBase<T> where T : new()
    {
        public InfluxServiceBase(InfluxConnection connection, string tableName)
        {
            this.Connection = connection;
            this.TableName = tableName;
        }

        public InfluxConnection Connection { get; set; }

        public string TableName { get; set; }

        public void Add(T data)
        {
            Connection.Write(TableName, data);
        }
    }
}
