using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.ExceptionReporter.SystemInfo
{
    /// <summary>
    /// Class SysInfoResult.
    /// </summary>
    public class SysInfoResult
    {
        /// <summary>
        /// The name
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// The nodes
        /// </summary>
        private readonly List<string> _nodes = new List<string>();
        /// <summary>
        /// The child results
        /// </summary>
        private readonly List<SysInfoResult> _childResults = new List<SysInfoResult>();

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        public List<string> Nodes
        {
            get
            {
                return this._nodes;
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
        /// Gets the child results.
        /// </summary>
        /// <value>The child results.</value>
        public List<SysInfoResult> ChildResults
        {
            get
            {
                return this._childResults;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SysInfoResult"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SysInfoResult(string name)
        {
            this._name = name;
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void AddNode(string node)
        {
            this._nodes.Add(node);
        }

        /// <summary>
        /// Adds the children.
        /// </summary>
        /// <param name="children">The children.</param>
        public void AddChildren(IEnumerable<SysInfoResult> children)
        {
            this.ChildResults.AddRange(children);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        private void Clear()
        {
            this._nodes.Clear();
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        private void AddRange(IEnumerable<string> nodes)
        {
            this._nodes.AddRange(nodes);
        }

        /// <summary>
        /// Filters the specified filter strings.
        /// </summary>
        /// <param name="filterStrings">The filter strings.</param>
        /// <returns>SysInfoResult.</returns>
        public SysInfoResult Filter(string[] filterStrings)
        {
            List<string> nodes = (
                from node in this.ChildResults[0].Nodes
                from filter in filterStrings
                where node.Contains(filter + " = ")
                select node).ToList<string>();
            this.ChildResults[0].Clear();
            this.ChildResults[0].AddRange(nodes);
            return this;
        }
    }
}