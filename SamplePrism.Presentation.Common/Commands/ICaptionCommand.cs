using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SamplePrism.Presentation.Common.Commands
{
    public interface ICaptionCommand : ICommand
    {
        string Caption { get; set; }
    }
}
