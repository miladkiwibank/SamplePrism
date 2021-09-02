using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public abstract class ModelListViewModelBase : ViewModelBase
    {
        public ModelListViewModelBase()
        {
            Views = new ObservableCollection<VisibleViewModelBase>();
            AttachEvents();
        }

        public ObservableCollection<VisibleViewModelBase> Views { get; set; }

        private void AttachEvents()
        {
            EventServiceFactory.EventService.GetEvent<GenericEvent<VisibleViewModelBase>>()
                .Subscribe(x =>
                {
                    if (x.Topic == EventTopicNames.ViewClosed)
                    {
                        if (x.Value != null)
                        {
                            if (Views.Contains(x.Value))
                                Views.Remove(x.Value);
                            if (x.Value.CallingView != null)
                                SetActiveView(Views, x.Value.CallingView);
                            x.Value.OnClosed();
                            x.Value.CallingView = null;
                            x.Value.Dispose();
                        }
                    }

                    if (x.Topic == EventTopicNames.ViewAdded && x.Value != null)
                    {
                        x.Value.CallingView = GetActiveView(Views);
                        if (!Views.Contains(x.Value))
                            Views.Add(x.Value);
                        SetActiveView(Views, x.Value);
                        x.Value.OnShown();
                    }
                });
        }
    }
}
