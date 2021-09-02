using Prism.Events;
using Prism.Regions;
using SamplePrism.Modules.SignInModule.Views;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Modules.SignInModule
{
    public class SignInModule : VisibleModuleBase
    {
        private readonly IRegionManager m_regionManager;
        private readonly SignInView m_signInView;

        public SignInModule(IRegionManager regionManager, SignInView signInView)
            : base(regionManager, AppScreens.SignInScreen)
        {
            m_regionManager = regionManager;
            m_signInView = signInView;

            SetNavigationCommand("退出", "普通", "");
        }

        protected override bool CanNavigate(string arg)
        {
            return true;
        }

        protected override void OnNavigate(string obj)
        {
            //TODO: execute signout
            //base.OnNavigate(obj);
        }

        protected override void OnInitialization()
        {
            //base.OnInitialization();
            m_regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(SignInView));

            //EventServiceFactory.EventService.GetEvent<GenericEvent<User>>()
            //    .Subscribe(x =>
            //    {
            //        if (x.Topic == EventTopicNames.UserSignedOut)
            //        {
            //            Activate();
            //        }
            //    });

            EventServiceFactory.EventService.GetEvent<GenericEvent<EventAggregator>>()
                .Subscribe(x =>
                {
                    if (x.Topic == EventTopicNames.ShellInitlized)
                        Activate();
                });

            EventServiceFactory.EventService.GetEvent<GenericEvent<string>>()
                .Subscribe(x =>
                {
                    if (x.Topic == EventTopicNames.UserSignedIn)
                        SignedIn(x.Value);
                });
        }

        public void SignedIn(string password)
        {
            //TODO: User signin 
        }

        public override object GetVisibleView()
        {
            return m_signInView;
        }
    }
}
