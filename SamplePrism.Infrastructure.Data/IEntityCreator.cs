using System.Collections.Generic;

namespace SamplePrism.Infrastructure.Data
{
    public interface IEntityCreator<out TModel>
    {
        IEnumerable<TModel> CreateItems(IEnumerable<string> data);
    }
}