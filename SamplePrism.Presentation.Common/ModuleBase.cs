using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Common.ModelBase;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common
{
    static class ObjectCache
    {
        private static readonly IDictionary<Type, VisibleViewModelBase> Cache = new Dictionary<Type, VisibleViewModelBase>();
        private static readonly IList<string> NonSingletonTypeNames = new List<string>();

        static ObjectCache()
        {
            NonSingletonTypeNames.Add(typeof(EntityCollectionViewModelBase<,>).Name);
            EventServiceFactory.EventService.GetEvent<GenericEvent<VisibleViewModelBase>>().Subscribe(OnViewClosed);
        }

        private static void OnViewClosed(EventParameters<VisibleViewModelBase> eventParameters)
        {
            if (eventParameters.Topic == EventTopicNames.ViewClosed)
            {
                Cache[eventParameters.Value.GetType()] = null;
            }
        }

        public static void Add(Type type)
        {
            Cache.Add(type, null);
        }

        public static bool Contains(Type type)
        {
            return Cache[type] != null;
        }

        public static void Update(Type type, VisibleViewModelBase modelBase)
        {
            Cache[type] = modelBase;
        }

        public static VisibleViewModelBase Get(Type type)
        {
            return Cache[type];
        }

        public static VisibleViewModelBase Activate<TView>() where TView : VisibleViewModelBase
        {
            if (!Contains(typeof(TView)))
            {
                try
                {
                    var view = NonSingletonTypeNames.Contains(typeof(TView).Name)
                        ? Activator.CreateInstance<TView>()
                        : ServiceLocator.Current.GetInstance<TView>();
                    Update(typeof(TView), view);
                }
                catch (Exception)
                {
                    Update(typeof(TView), Activator.CreateInstance<TView>());
                }
            }
            return Get(typeof(TView));
        }
    }

    public abstract class ModuleBase : IModule
    {
        private readonly List<ICategoryCommand> m_dashboardCommands = new List<ICategoryCommand>();

        public void Initialize()
        {
            var moduleLifecycleService = ServiceLocator.Current.GetInstance<IModuleInitializationService>();
            moduleLifecycleService.RegisterForStage(OnPreInitialization, ModuleInitializationStage.PreInitialization);
            moduleLifecycleService.RegisterForStage(OnInitialization, ModuleInitializationStage.Initialization);
            moduleLifecycleService.RegisterForStage(OnPostInitialization, ModuleInitializationStage.PostInitialization);
            moduleLifecycleService.RegisterForStage(OnStartUp, ModuleInitializationStage.StartUp);
        }

        protected virtual void OnPreInitialization()
        {

        }

        protected virtual void OnInitialization()
        {

        }

        protected virtual void OnPostInitialization()
        {
            m_dashboardCommands.ForEach(x => x.PublishEvent(EventTopicNames.DashboardCommandAdded));
            m_dashboardCommands.Clear();
        }

        protected virtual void OnStartUp()
        {

        }

        protected void AddDashboardCommand<TView>(string caption, string category, int order = 0)
            where TView : VisibleViewModelBase
        {
            m_dashboardCommands.Add(new CategoryCommand<TView>(caption, category, OnExecute) { Order = order });
        }

        private static void OnExecute<TView>(TView view)
            where TView : VisibleViewModelBase
        {
            view.PublishEvent(EventTopicNames.ViewClosed, true);
        }
    }
}
