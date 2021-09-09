using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Infrastructure.Data.Entity
{
    public class AggregateRoot<TKey> : IAggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public string Name { get; set; }
    }

    public class AggregateRoot : AggregateRoot<int>
    {
    }
}
