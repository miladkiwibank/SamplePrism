using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ExceptionReporter.SystemInfo
{
    /// <summary>
    /// Class SysInfoResultMapper.
    /// </summary>
    public static class SysInfoResultMapper
    {
        /// <summary>
        /// Creates the string list.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>System.String.</returns>
        public static string CreateStringList(IEnumerable<SysInfoResult> results)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (SysInfoResult current in results)
            {
                stringBuilder.AppendLine(current.Name);
                foreach (string current2 in current.Nodes)
                {
                    stringBuilder.AppendLine("-" + current2);
                    foreach (SysInfoResult current3 in current.ChildResults)
                    {
                        foreach (string current4 in current3.Nodes)
                        {
                            stringBuilder.AppendLine("--" + current4);
                        }
                    }
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}