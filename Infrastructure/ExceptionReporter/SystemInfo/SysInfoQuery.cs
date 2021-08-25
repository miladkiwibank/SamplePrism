namespace Infrastructure.ExceptionReporter.SystemInfo
{
    /// <summary>
    /// Class SysInfoQuery.
    /// </summary>
    public class SysInfoQuery
    {
        /// <summary>
        /// The name
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// The use name as display field
        /// </summary>
        private readonly bool _useNameAsDisplayField;
        /// <summary>
        /// The query text
        /// </summary>
        private readonly string _queryText;

        /// <summary>
        /// Gets the query text.
        /// </summary>
        /// <value>The query text.</value>
        public string QueryText
        {
            get
            {
                return this._queryText;
            }
        }

        /// <summary>
        /// Gets the display field.
        /// </summary>
        /// <value>The display field.</value>
        public string DisplayField
        {
            get
            {
                if (!this._useNameAsDisplayField)
                {
                    return "Caption";
                }
                return "Name";
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SysInfoQuery"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="query">The query.</param>
        /// <param name="useNameAsDisplayField">if set to <c>true</c> [use name as display field].</param>
        public SysInfoQuery(string name, string query, bool useNameAsDisplayField)
        {
            this._name = name;
            this._useNameAsDisplayField = useNameAsDisplayField;
            this._queryText = query;
        }
    }
}