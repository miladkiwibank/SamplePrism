using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SamplePrism.Domain.Models.Users;
using SamplePrism.Localization.Properties;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;

namespace SamplePrism.Modules.LoginModule
{
    public class LoginModule : VisibleModuleBase
    {
        readonly IRegionManager _regionManager;
        private readonly LoginView _loginView;
        private readonly IUserService _userService;

        public LoginModule(IRegionManager regionManager, LoginView loginView, IUserService userService)
            : base(regionManager, AppScreens.LoginScreen)
        {
            _regionManager = regionManager;
            _loginView = loginView;
            _userService = userService;
            SetNavigationCommand(Resources.Logout, Resources.Common, "images/bmp.png", 99);
        }

        protected override bool CanNavigate(string arg)
        {
            return true;
        }

        protected override void OnNavigate(string obj)
        {
            _userService.LogoutUser();
        }

        protected override void OnPreInitialization()
        {
            base.OnPreInitialization();
            ViewModelLocationProvider.Register<LoginView, LoginViewModel>();
        }

        protected override void OnInitialization()
        {
            //_regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(LoginView));
            _regionManager.AddToRegion(RegionNames.MainRegion, _loginView);

            EventServiceFactory.EventService.GetEvent<GenericEvent<User>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.UserLoggedOut)
                        Activate();
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent<EventAggregator>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.ShellInitialized) Activate();
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent<string>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.PinSubmitted)
                        PinEntered(x.Value);
                });
        }

        public void PinEntered(string pin)
        {
            //_userService.LoginUser(pin);
            var user = new User { Name = "Admin" };
            user.PublishEvent(EventTopicNames.UserLoggedIn);
        }

        public override object GetVisibleView()
        {
            return _loginView;
        }
    }
}
