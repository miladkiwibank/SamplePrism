using SimplePrism.Presentation.Common.Commands;
using SimplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Common.Services
{
    /// <summary>
    /// 展示层菜单命令服务
    /// </summary>
    public static class PresentationServices
    {
        public static ObservableCollection<ICategoryCommand> NavigationCommandCategories { get; private set; }
        public static ObservableCollection<DashboardCommandCategory> DashboardCommandCategories { get; private set; }

        public static void Initialize()
        {
            NavigationCommandCategories = new ObservableCollection<ICategoryCommand>();
            DashboardCommandCategories = new ObservableCollection<DashboardCommandCategory>();
            EventServiceFactory.EventService.GetEvent<GenericEvent<ICategoryCommand>>().Subscribe(OnCommandAdded);
        }

        private static void OnCommandAdded(EventParameters<ICategoryCommand> parameters)
        {
            if (parameters.Topic == EventTopicNames.NavigationCommandAdded)
                NavigationCommandCategories.Add(parameters.Value);

            if (parameters.Topic == EventTopicNames.DashboardCommandAdded)
            {
                var category = DashboardCommandCategories.FirstOrDefault(x => x.Category == parameters.Value.Category);
                if (category == null)
                {
                    category = new DashboardCommandCategory(parameters.Value.Category);
                    DashboardCommandCategories.Add(category);
                }
                if (parameters.Value.Order > category.Order)
                    category.Order = parameters.Value.Order;
                category.AddCommand(parameters.Value);
            }
        }
    }
}
