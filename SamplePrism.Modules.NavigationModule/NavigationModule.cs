using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SamplePrism.Modules.NavigationModule.ViewModels;
using SamplePrism.Modules.NavigationModule.Views;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Modules.NavigationModule
{
    public class NavigationModule : VisibleModuleBase
    {
        private readonly IRegionManager m_regionManager;
        private readonly NavigationView m_navigationView;
        private readonly IApplicationState m_applicationState;

        public NavigationModule(IRegionManager regionManager, NavigationView navigationView, IApplicationState applicationState)
            : base(regionManager, AppScreens.Navigation)
        {
            m_regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            m_applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
            m_navigationView = navigationView ?? throw new ArgumentNullException(nameof(navigationView));
            m_regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(NavigationView));
            PermissionRegistry.RegisterPermission(PermissionNames.OpenNavigation, PermissionCategories.Navigation, "打开导航");

            //EventServiceFactory.EventService.GetEvent<GenericEvent<User>>()
            //    .Subscribe(x =>
            //    {
            //        if (x.Topic == EventTopicNames.UserSignedIn)
            //            ActivateNavigation();
            //    });
            EventServiceFactory.EventService.GetEvent<GenericEvent<EventAggregator>>()
                .Subscribe(x =>
                {
                    if (x.Topic == EventTopicNames.ActivateNavigation)
                        ActivateNavigation();
                });
            //EventServiceFactory.EventService.GetEvent<GenericEvent<AppScreenChangeData>>()
            //    .Subscribe(x =>
            //    {
            //        if (x.Topic == EventTopicNames.ScreenChanged)
            //        {
            //            m_applicationState.NotifyEvent(RuleEventNames.ApplicationScreenChanged, new
            //            {
            //                PreviousScreen = Enum.GetName(typeof(AppScreens), x.Value.PreviousScreen),
            //                CurrentScreen = Enum.GetName(typeof(AppScreens), x.Value.CurrentScreen)
            //            });
            //        }
            //    });
        }

        public override object GetVisibleView()
        {
            return m_navigationView;
        }

        protected override void OnPreInitialization()
        {
            //base.OnPreInitialization();
            ViewModelLocationProvider.Register<NavigationView, NavigationViewModel>();

        }

        private void ActivateNavigation()
        {
            //TODO check permission
            if (true)// m_userManager.IsUserPermittedFor(PermissionNames.OpenNavigation)
            {
                Activate();
                ((NavigationViewModel)m_navigationView.DataContext).Refresh();
            }
            else
            {
                //TODO: open no permission view
            }
        }
    }
}
