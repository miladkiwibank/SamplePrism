using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SamplePrism.Presentation.Common.Commands
{
    public class CaptionCommand : DelegateCommand, ICommand, ICaptionCommand
    {
        public CaptionCommand(string caption, Action executeMethod)
            : base(executeMethod)
        {
            Caption = caption;
        }

        public CaptionCommand(string caption, Action executeMethod, Func<bool> canExecuteMethod)
            : base(() => executeMethod(), () => canExecuteMethod())
        {
            Caption = caption;
        }

        public string Caption { get; set; }

        public new event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class CaptionCommand<T> : DelegateCommand<T>, ICommand
    {
        public CaptionCommand(string caption, Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
            Caption = caption;
        }

        public CaptionCommand(string caption, Action<T> executeMethod)
            : base(executeMethod)
        {
            Caption = caption;
        }

        public string Caption { get; set; }

        public new event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
