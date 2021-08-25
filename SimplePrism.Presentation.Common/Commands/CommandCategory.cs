using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Common.Commands
{
    public class DashboardCommandCategory
    {

        public DashboardCommandCategory(string category)
        {
            Category = category;
            m_commands = new List<ICategoryCommand>();
        }

        public void AddCommand(ICategoryCommand command)
        {
            m_commands.Add(command);
        }

        public int Order { get; set; }

        public string Category { get; set; }

        private List<ICategoryCommand> m_commands;

        public IEnumerable<ICategoryCommand> Commands
        {
            get { return m_commands.OrderBy(x => x.Order); }
        }
    }
}
