using Infrastructure.ExceptionReporter.SystemInfo;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.ExceptionReporter
{
    /// <summary>
    /// Class ExceptionReportBuilder.
    /// </summary>
    public class ExceptionReportBuilder
    {
        /// <summary>
        /// The report information
        /// </summary>
        private readonly ExceptionReportInfo _reportInfo;
        /// <summary>
        /// The string builder
        /// </summary>
        private StringBuilder m_stringBuilder;
        /// <summary>
        /// The system information results
        /// </summary>
        private readonly IEnumerable<SysInfoResult> _sysInfoResults;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionReportBuilder"/> class.
        /// </summary>
        /// <param name="reportInfo">The report information.</param>
        public ExceptionReportBuilder(ExceptionReportInfo reportInfo)
        {
            this._reportInfo = reportInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionReportBuilder"/> class.
        /// </summary>
        /// <param name="reportInfo">The report information.</param>
        /// <param name="sysInfoResults">The system information results.</param>
        public ExceptionReportBuilder(ExceptionReportInfo reportInfo, IEnumerable<SysInfoResult> sysInfoResults) : this(reportInfo)
        {
            this._sysInfoResults = sysInfoResults;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Build()
        {
            this.m_stringBuilder = new StringBuilder().AppendLine("-----------------------------");
            this.BuildGeneralInfo();
            this.BuildExceptionInfo();
            this.BuildAssemblyInfo();
            this.BuildSysInfo();
            return this.m_stringBuilder.ToString();
        }

        /// <summary>
        /// Builds the general information.
        /// </summary>
        private void BuildGeneralInfo()
        {
            this.m_stringBuilder.AppendLine("[General Info]")
                .AppendLine()
                .AppendLine("Application: RD")
                //.AppendLine("Version:     " + LocalSettings.AppVersion)
                //.AppendLine("Region:      " + LocalSettings.CurrentLanguage)
                //.AppendLine("DB:          " + LocalSettings.DatabaseLabel)
                .AppendLine("Machine:     " + this._reportInfo.MachineName)
                .AppendLine("User:        " + this._reportInfo.UserName)
                .AppendLine("Date:        " + this._reportInfo.ExceptionDate.ToShortDateString())
                .AppendLine("Time:        " + this._reportInfo.ExceptionDate.ToShortTimeString())
                .AppendLine();
            this.m_stringBuilder.AppendLine("User Explanation:")
                .AppendLine().AppendFormat("{0} said \"{1}\"", this._reportInfo.UserName, this._reportInfo.UserExplanation)
                .AppendLine()
                .AppendLine("-----------------------------")
                .AppendLine();
        }

        /// <summary>
        /// Builds the exception information.
        /// </summary>
        private void BuildExceptionInfo()
        {
            for (int i = 0; i < this._reportInfo.Exceptions.Count; i++)
            {
                Exception exception = this._reportInfo.Exceptions[i];
                this.m_stringBuilder.AppendLine(string.Format("[Exception Info {0}]", i + 1)).AppendLine().AppendLine(ExceptionReportBuilder.ExceptionHierarchyToString(exception)).AppendLine().AppendLine("-----------------------------").AppendLine();
            }
        }

        /// <summary>
        /// Builds the assembly information.
        /// </summary>
        private void BuildAssemblyInfo()
        {
            this.m_stringBuilder.AppendLine("[Assembly Info]").AppendLine().AppendLine(ExceptionReportBuilder.CreateReferencesString(this._reportInfo.AppAssembly)).AppendLine("-----------------------------").AppendLine();
        }

        /// <summary>
        /// Builds the system information.
        /// </summary>
        private void BuildSysInfo()
        {
            this.m_stringBuilder.AppendLine("[System Info]").AppendLine();
            this.m_stringBuilder.Append(SysInfoResultMapper.CreateStringList(this._sysInfoResults));
            this.m_stringBuilder.AppendLine("-----------------------------").AppendLine();
        }

        /// <summary>
        /// Creates the references string.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>System.String.</returns>
        public static string CreateReferencesString(Assembly assembly)
        {
            StringBuilder stringBuilder = new StringBuilder();
            AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
            for (int i = 0; i < referencedAssemblies.Length; i++)
            {
                AssemblyName assemblyName = referencedAssemblies[i];
                stringBuilder.AppendLine(string.Format("{0}, Version={1}", assemblyName.Name, assemblyName.Version));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Exceptions the hierarchy to string.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>System.String.</returns>
        private static string ExceptionHierarchyToString(Exception exception)
        {
            Exception ex = exception;
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            while (ex != null)
            {
                if (num++ == 0)
                {
                    stringBuilder.AppendLine("Top-level Exception");
                }
                else
                {
                    stringBuilder.AppendLine("Inner Exception " + (num - 1));
                }
                stringBuilder.AppendLine("Type:        " + ex.GetType()).AppendLine("Message:     " + ex.Message).AppendLine("Source:      " + ex.Source);
                if (ex.StackTrace != null)
                {
                    stringBuilder.AppendLine("Stack Trace: " + ex.StackTrace.Trim());
                }
                stringBuilder.AppendLine();
                ex = ex.InnerException;
            }
            string text = stringBuilder.ToString();
            return text.TrimEnd(new char[0]);
        }
    }
}