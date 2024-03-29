﻿using Prism.Commands;
using System;
using System.ComponentModel;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public abstract class VisibleViewModelBase : ViewModelBase
    {
        public abstract Type GetViewType();

        [Browsable(false)]
        public VisibleViewModelBase CallingView { get; set; }

        private DelegateCommand<object> _closeCommand;

        [Browsable(false)]
        public DelegateCommand<object> CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand<object>(OnRequestClose, CanClose)); }
        }

        protected virtual bool CanClose(object arg)
        {
            return true;
        }

        private void PublishClose()
        {
            CommonEventPublisher.PublishViewClosedEvent(this);
        }

        private void OnRequestClose(object obj)
        {
            PublishClose();
        }

        public virtual void OnClosed()
        {
            //override if needed
        }

        public virtual void OnShown()
        {
            //override if needed
        }
    }
}