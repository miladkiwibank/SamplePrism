using Infrastructure.ExceptionReporter.SystemInfo;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.ExceptionReporter
{
    /// <summary>
    /// Class ExceptionReportGenerator.
    /// </summary>
    /// <seealso cref="Infrastructure.ExceptionReporter.Disposable" />
    public class ExceptionReportGenerator : Disposable
    {
        /// <summary>
        /// The report information
        /// </summary>
        private readonly ExceptionReportInfo _reportInfo;
        /// <summary>
        /// The system information results
        /// </summary>
        private readonly List<SysInfoResult> _sysInfoResults = new List<SysInfoResult>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionReportGenerator"/> class.
        /// </summary>
        /// <param name="reportInfo">The report information.</param>
        /// <exception cref="ExceptionReportGeneratorException">reportInfo cannot be null</exception>
        public ExceptionReportGenerator(ExceptionReportInfo reportInfo)
        {
            if (reportInfo == null)
            {
                throw new ExceptionReportGeneratorException("reportInfo cannot be null");
            }
            this._reportInfo = reportInfo;
            this._reportInfo.ExceptionDate = DateTime.UtcNow;
            this._reportInfo.UserName = Environment.UserName;
            this._reportInfo.MachineName = Environment.MachineName;
            if (this._reportInfo.AppAssembly == null)
            {
                this._reportInfo.AppAssembly = (Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly());
            }
        }

        /// <summary>
        /// Creates the exception report.
        /// </summary>
        /// <returns>System.String.</returns>
        public string CreateExceptionReport()
        {
            IList<SysInfoResult> orFetchSysInfoResults = this.GetOrFetchSysInfoResults();
            ExceptionReportBuilder exceptionReportBuilder = new ExceptionReportBuilder(this._reportInfo, orFetchSysInfoResults);
            return exceptionReportBuilder.Build();
        }

        /// <summary>
        /// Gets the or fetch system information results.
        /// </summary>
        /// <returns>IList&lt;SysInfoResult&gt;.</returns>
        public IList<SysInfoResult> GetOrFetchSysInfoResults()
        {
            if (this._sysInfoResults.Count == 0)
            {
                this._sysInfoResults.AddRange(ExceptionReportGenerator.CreateSysInfoResults());
            }
            return this._sysInfoResults.AsReadOnly();
        }

        /// <summary>
        /// Creates the system information results.
        /// </summary>
        /// <returns>IEnumerable&lt;SysInfoResult&gt;.</returns>
        private static IEnumerable<SysInfoResult> CreateSysInfoResults()
        {
            SysInfoRetriever sysInfoRetriever = new SysInfoRetriever();
            return new List<SysInfoResult>
            {
                sysInfoRetriever.Retrieve(SysInfoQueries.OperatingSystem).Filter(new string[]
                {
                    "CodeSet",
                    "CurrentTimeZone",
                    "FreePhysicalMemory",
                    "OSArchitecture",
                    "OSLanguage",
                    "Version"
                }),
                sysInfoRetriever.Retrieve(SysInfoQueries.Machine).Filter(new string[]
                {
                    "Machine",
                    "UserName",
                    "TotalPhysicalMemory",
                    "Manufacturer",
                    "Model"
                })
            };
        }

        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            this._reportInfo.Dispose();
            base.DisposeManagedResources();
        }
    }
}