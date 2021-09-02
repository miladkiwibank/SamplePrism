using Prism.Commands;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public abstract class VisibleViewModelBase : ViewModelBase
    {
        public abstract Type GetViewType();

        [Browsable(false)]
        public VisibleViewModelBase CallingView { get; set; }

        private DelegateCommand<object> m_closeCommand;

        public DelegateCommand<object> CloseCommand =>
            m_closeCommand ?? (m_closeCommand = new DelegateCommand<object>(OnRequestClose, CanClose));

        protected virtual bool CanClose(object arg)
        {
            return true;
        }

        private void OnRequestClose(object obj)
        {
            this.PublishEvent(EventTopicNames.ViewClosed, true);
        }

        public virtual void OnClosed()
        {
            // overried if needed
        }

        public virtual void OnShown()
        {
            // overried if needed
        }
    }
}
