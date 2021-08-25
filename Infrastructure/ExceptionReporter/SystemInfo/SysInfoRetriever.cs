using System;
using System.Collections.Generic;
using System.Management;

namespace Infrastructure.ExceptionReporter.SystemInfo
{
    /// <summary>
    /// Class SysInfoRetriever.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class SysInfoRetriever : IDisposable
    {
        /// <summary>
        /// The system information searcher
        /// </summary>
        private ManagementObjectSearcher _sysInfoSearcher;
        /// <summary>
        /// The system information result
        /// </summary>
        private SysInfoResult _sysInfoResult;
        /// <summary>
        /// The system information query
        /// </summary>
        private SysInfoQuery _sysInfoQuery;

        /// <summary>
        /// Retrieves the specified system information query.
        /// </summary>
        /// <param name="sysInfoQuery">The system information query.</param>
        /// <returns>SysInfoResult.</returns>
        public SysInfoResult Retrieve(SysInfoQuery sysInfoQuery)
        {
            this._sysInfoQuery = sysInfoQuery;
            this._sysInfoSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", this._sysInfoQuery.QueryText));
            this._sysInfoResult = new SysInfoResult(this._sysInfoQuery.Name);
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = this._sysInfoSearcher.Get().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ManagementObject managementObject = (ManagementObject)enumerator.Current;
                    this._sysInfoResult.AddNode(managementObject.GetPropertyValue(this._sysInfoQuery.DisplayField).ToString().Trim());
                    this._sysInfoResult.AddChildren(this.GetChildren(managementObject));
                }
            }
            return this._sysInfoResult;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="managementObject">The management object.</param>
        /// <returns>IEnumerable&lt;SysInfoResult&gt;.</returns>
        private IEnumerable<SysInfoResult> GetChildren(ManagementBaseObject managementObject)
        {
            SysInfoResult sysInfoResult = null;
            ICollection<SysInfoResult> collection = new List<SysInfoResult>();
            foreach (PropertyData current in managementObject.Properties)
            {
                if (sysInfoResult == null)
                {
                    sysInfoResult = new SysInfoResult(this._sysInfoQuery.Name + "_Child");
                    collection.Add(sysInfoResult);
                }
                string item = string.Format("{0} = {1}", current.Name, Convert.ToString(current.Value));
                sysInfoResult.Nodes.Add(item);
            }
            return collection;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            this._sysInfoSearcher.Dispose();
        }
    }
}