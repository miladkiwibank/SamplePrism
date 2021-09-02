using Prism.Mvvm;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Modules.NavigationModule.ViewModels
{
    public class NavigationViewModel : BindableBase
    {
        public ObservableCollection<ICategoryCommand> CategoryView =>
            new ObservableCollection<ICategoryCommand>(
                PresentationServices.NavigationCommandCategories.OrderBy(x => x.Order));

        public void Refresh()
        {
            RaisePropertyChanged(nameof(CategoryView));
        }
    }
}
