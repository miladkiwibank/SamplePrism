using Prism.Commands;
using Prism.Mvvm;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Common.ModelBase;
using SamplePrism.Presentation.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Modules.ManagementModule.ViewModels
{
    public class ManagementViewModel : ModelListViewModelBase
    {
        public ObservableCollection<DashboardCommandCategory> CategoryView
        {
            get
            {
                var result = new ObservableCollection<DashboardCommandCategory>(
                    PresentationServices.DashboardCommandCategories.OrderBy(x => x.Order));
                return result;
            }
        }

        protected override string GetHeaderInfo()
        {
            return "Dashboard";
        }

        public void Refresh()
        {
            RaisePropertyChanged(nameof(CategoryView));
        }
    }
}
