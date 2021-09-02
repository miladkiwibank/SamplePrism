using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Validation
{
    public static class ValidatorRegistry
    {
        private static readonly Dictionary<Type, object> SaveValidators = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, object> DeleteValidators = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, object> ConcurrencyValidators = new Dictionary<Type, object>();

        public static void RegisterSaveValidator<T>(SpecificationValidator<T> validator) where T : class
        {
            SaveValidators.Add(typeof(T), validator);
        }

        public static void RegisterDeleteValidator<T>(SpecificationValidator<T> validator) where T : class
        {
            DeleteValidators.Add(typeof(T), validator);
        }

        public static void RegisterDeleteValidator<T>(Func<T, bool> validationFunction, string modelName, string entityName)
            where T : class
        {
            SpecificationValidator<T> validator = new GenericDeleteValidator<T>(validationFunction, modelName, entityName);
            DeleteValidators.Add(typeof(T), validator);
        }

        public static void RegisterConcurrencyValidator<T>(ConcurrencyValidator<T> validator) where T : class
        {
            ConcurrencyValidators.Add(typeof(T), validator);
        }

        public static string GetSaveErrorMessage<T>(T model) where T : class
        {
            SpecificationValidator<T> validator = SaveValidators.TryGetValue(typeof(T), out object tmp)
                ? (SpecificationValidator<T>)tmp : default;
            return validator != null ? validator.GetErrorMessage(model) : "";
        }

        public static string GetDeleteErrorMessage<T>(T model) where T : class
        {
            SpecificationValidator<T> validator = DeleteValidators.TryGetValue(typeof(T), out object tmp)
                ? (SpecificationValidator<T>)tmp : default;
            return validator != null ? validator.GetErrorMessage(model) : "";
        }

        public static ConcurrencyCheckResult GetConcurrencyErrorMessage<T>(T current, T loaded) where T : class
        {
            ConcurrencyValidator<T> validator = ConcurrencyValidators.TryGetValue(typeof(T), out object tmp)
                ? (ConcurrencyValidator<T>)tmp : default;
            return validator != null ? validator.GetErrorMessage(current, loaded) : ConcurrencyCheckResult.Continue();
        }
    }
}
