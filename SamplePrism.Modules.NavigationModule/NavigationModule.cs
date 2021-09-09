using System;
using Prism.Events;
using Prism.Regions;
using SamplePrism.Domain.Models.Users;
using SamplePrism.Localization.Properties;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;
using SamplePrism.Services.Common;

namespace SamplePrism.Modules.NavigationModule
{
    public class NavigationModule : VisibleModuleBase
    {
        private readonly IRegionManager _regionManager;
        private readonly NavigationView _navigationView;
        private readonly IUserService _userService;
        private readonly IApplicationState _applicationState;

        public NavigationModule(IRegionManager regionManager, NavigationView navigationView, IUserService userService,
            IApplicationState applicationState)
            : base(regionManager, AppScreens.Navigation)
        {
            _regionManager = regionManager;
            _navigationView = navigationView;
            _userService = userService;
            _applicationState = applicationState;

            PermissionRegistry.RegisterPermission(PermissionNames.OpenNavigation, PermissionCategories.Navigation, Resources.CanOpenNavigation);

            EventServiceFactory.EventService.GetEvent<GenericEvent<User>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.UserLoggedIn)
                        ActivateNavigation();
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent<EventAggregator>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.ActivateNavigation)
                        ActivateNavigation();
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent<AppScreenChangeData>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.Changed)
                    {
                        _applicationState.NotifyEvent(RuleEventNames.ApplicationScreenChanged,
                            new
                            {
                                PreviousScreen = Enum.GetName(typeof(AppScreens), x.Value.PreviousScreen),
                                CurrentScreen = Enum.GetName(typeof(AppScreens), x.Value.CurrentScreen)
                            });
                    }
                });
        }

        public override object GetVisibleView()
        {
            return _navigationView;
        }

        protected override void OnInitialization()
        {
            _regionManager.AddToRegion(RegionNames.MainRegion, _navigationView);
            //_regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(NavigationView));
        }

        private void ActivateNavigation()
        {
            if (_userService.IsUserPermittedFor(PermissionNames.OpenNavigation))
            {
                Activate();
                ((NavigationViewModel)_navigationView.DataContext).Refresh();
            }
            else if (_applicationState.IsCurrentWorkPeriodOpen)
            {
                EventServiceFactory.EventService.PublishEvent(EventTopicNames.ActivatePosView);
            }
        }
    }
}
