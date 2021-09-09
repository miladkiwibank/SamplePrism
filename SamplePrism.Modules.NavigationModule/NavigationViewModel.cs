using System.Collections.ObjectModel;
using System.Linq;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Common.Services;

namespace SamplePrism.Modules.NavigationModule
{
    public class NavigationViewModel : ObservableObject
    {
        public ObservableCollection<ICategoryCommand> CategoryView
        {
            get
            {
                return new ObservableCollection<ICategoryCommand>(
                    PresentationServices.NavigationCommandCategories.OrderBy(x => x.Order));
            }
        }

        public void Refresh()
        {
            RaisePropertyChanged(nameof(CategoryView));
        }
    }
}
