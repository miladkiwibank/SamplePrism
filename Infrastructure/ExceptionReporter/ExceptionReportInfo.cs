using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.ExceptionReporter
{
    /// <summary>
    /// Class ExceptionReportInfo.
    /// </summary>
    /// <seealso cref="Infrastructure.ExceptionReporter.Disposable" />
    public class ExceptionReportInfo : Disposable
    {
        /// <summary>
        /// The exceptions
        /// </summary>
        private readonly List<Exception> m_exceptions = new List<Exception>();

        /// <summary>
        /// Gets or sets the main exception.
        /// </summary>
        /// <value>The main exception.</value>
        public Exception MainException
        {
            get
            {
                if (this.m_exceptions.Count <= 0)
                {
                    return null;
                }
                return this.m_exceptions[0];
            }
            set
            {
                this.m_exceptions.Clear();
                this.m_exceptions.Add(value);
            }
        }

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>The exceptions.</value>
        public IList<Exception> Exceptions => m_exceptions.AsReadOnly();

        /// <summary>
        /// Gets or sets the custom message.
        /// </summary>
        /// <value>The custom message.</value>
        public string CustomMessage { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the exception date.
        /// </summary>
        /// <value>The exception date.</value>
        public DateTime ExceptionDate { get; set; }

        /// <summary>
        /// Gets or sets the user explanation.
        /// </summary>
        /// <value>The user explanation.</value>
        public string UserExplanation { get; set; }

        /// <summary>
        /// Gets or sets the application assembly.
        /// </summary>
        /// <value>The application assembly.</value>
        public Assembly AppAssembly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [take screenshot].
        /// </summary>
        /// <value><c>true</c> if [take screenshot]; otherwise, <c>false</c>.</value>
        public bool TakeScreenshot { get; set; }

        /// <summary>
        /// Sets the exceptions.
        /// </summary>
        /// <param name="exceptions">The exceptions.</param>
        public void SetExceptions(IEnumerable<Exception> exceptions)
        {
            this.m_exceptions.Clear();
            this.m_exceptions.AddRange(exceptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionReportInfo"/> class.
        /// </summary>
        public ExceptionReportInfo()
        {
            this.SetDefaultValues();
        }

        /// <summary>
        /// Sets the default values.
        /// </summary>
        private void SetDefaultValues()
        {
            this.TakeScreenshot = false;
        }
    }
}