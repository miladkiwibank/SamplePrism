using SamplePrism.Infrastructure.Data;
using SamplePrism.Persistance.Data;

namespace SamplePrism.Persistance.Specification
{
    public static class SpecificationDaoExtentions
    {
        public static bool Exists<T>(this ISpecification<T> specification) where T : class,IValueClass
        {
            return Dao.Exists(specification.SatisfiedBy());
        }

        public static bool NotExists<T>(this ISpecification<T> specification) where T : class,IValueClass
        {
            return !Exists(specification);
        }

        public static string Validate<T>(this ISpecification<T> specification, string errorMessage) where T : class,IValueClass
        {
            return Exists(specification) ? errorMessage : "";
        }
    }
}
