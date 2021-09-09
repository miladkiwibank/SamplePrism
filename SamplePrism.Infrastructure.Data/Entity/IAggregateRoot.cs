using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Infrastructure.Data.Entity
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {

    }

    public interface IAggregateRoot : IAggregateRoot<int>
    {

    }
}
