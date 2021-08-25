using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimplePrism.Presentation.Common.Commands
{
    public interface ICategoryCommand : ICommand
    {
        string Category { get; set; }

        string ImageSource { get; set; }

        int Order { get; set; }
    }
}
