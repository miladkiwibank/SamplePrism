using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Common.ModelBase;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;

namespace SamplePrism.Presentation.Common
{
    internal static class ObjectCache
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
                    Update(typeof(TView),
                           NonSingletonTypeNames.Contains(typeof(TView).Name)
                               ? Activator.CreateInstance<TView>()
                               : ServiceLocator.Current.GetInstance<TView>());
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
        private readonly List<ICategoryCommand> _dashboardCommands = new List<ICategoryCommand>();

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
            _dashboardCommands.ForEach(x => x.PublishEvent(EventTopicNames.DashboardCommandAdded));
            _dashboardCommands.Clear();
        }

        protected virtual void OnStartUp()
        {
        }

        protected void AddDashboardCommand<TView>(string caption, string category, int order = 0) where TView : VisibleViewModelBase
        {
            _dashboardCommands.Add(new CategoryCommand<TView>(caption, category, OnExecute) { Order = order });
            ObjectCache.Add(typeof(TView));
        }

        private static void OnExecute<TView>(TView obj) where TView : VisibleViewModelBase
        {
            CommonEventPublisher.PublishViewAddedEvent(ObjectCache.Activate<TView>());
        }
    }
}