using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public abstract class ViewModelBase : BindableBase
    {
        ~ViewModelBase()
        {
#if DEBUG
            string msg = string.Format("{0} ({1}) ({2}) Finalized", GetType().Name, Header, GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
#endif
            Dispose(false);
        }

        public string Header { get; set; }

        protected abstract string GetHeader();

        protected void SetActiveView(IEnumerable<VisibleViewModelBase> views, VisibleViewModelBase wm)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(views);
            if (collectionView != null && collectionView.Contains(wm))
                collectionView.MoveCurrentTo(wm);
        }

        protected VisibleViewModelBase GetActiveView(IEnumerable<VisibleViewModelBase> views)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(views);
            return collectionView.CurrentItem as VisibleViewModelBase;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}