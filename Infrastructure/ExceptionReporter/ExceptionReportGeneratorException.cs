using System;

namespace Infrastructure.ExceptionReporter
{
    /// <summary>
    /// Class ExceptionReportGeneratorException.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class ExceptionReportGeneratorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionReportGeneratorException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ExceptionReportGeneratorException(string message) : base(message)
        {
        }
    }
}