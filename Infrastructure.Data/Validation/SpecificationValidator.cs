using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Validation
{
    public abstract class SpecificationValidator<T> where T : class
    {
        public abstract string GetErrorMessage(T model);
    }

    public class GenericDeleteValidator<T> : SpecificationValidator<T> where T : class
    {
        private readonly Func<T, bool> m_validationFunction;
        private readonly string m_modelName;
        private readonly string m_entityName;

        public GenericDeleteValidator(Func<T, bool> validationFunction, string modelName, string entityName)
        {
            m_validationFunction = validationFunction;
            m_modelName = modelName;
            m_entityName = entityName;
        }

        public override string GetErrorMessage(T model)
        {
            var result = m_validationFunction.Invoke(model);
            return result ? string.Format("{0},{1}", m_modelName, m_entityName) : "";
        }
    }

    public enum SuggestedOperation
    {
        Break, Refresh, Continue
    }

    public class ConcurrencyCheckResult
    {
        public string ErrorMessage { get; set; }

        public SuggestedOperation SuggestedOperation { get; set; }

        public static ConcurrencyCheckResult Create(SuggestedOperation suggestedOperation, string errorMessage = "")
        {
            return new ConcurrencyCheckResult { ErrorMessage = errorMessage, SuggestedOperation = suggestedOperation };
        }

        public static ConcurrencyCheckResult Break(string errorMessage)
        {
            return Create(SuggestedOperation.Break, errorMessage);
        }

        public static ConcurrencyCheckResult Refresh()
        {
            return Create(SuggestedOperation.Refresh);
        }

        public static ConcurrencyCheckResult Continue()
        {
            return Create(SuggestedOperation.Continue);
        }
    }

    public abstract class ConcurrencyValidator<T> where T : class
    {
        public abstract ConcurrencyCheckResult GetErrorMessage(T current, T loaded);
    }
}
