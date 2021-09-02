using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common
{
    public abstract class VisibleModuleBase : ModuleBase
    {
        private readonly IRegionManager m_regionManager;
        private readonly AppScreens m_appScreen;
        private readonly IApplicationStateSetter m_applicationStateSetter;
        private ICategoryCommand m_navigationCommand;

        protected VisibleModuleBase(IRegionManager regionManager, AppScreens appScreen)
        {
            m_applicationStateSetter = ServiceLocator.Current.GetInstance<IApplicationStateSetter>();
            m_regionManager = regionManager;
            m_appScreen = appScreen;
        }

        public void Activate()
        {
            m_applicationStateSetter.SetCurrentApplicationScreen(m_appScreen);
            m_regionManager.ActivateRegion(RegionNames.MainRegion, GetVisibleView());
        }

        protected void SetNavigationCommand(string caption, string category, string image, int order = 0)
        {
            m_navigationCommand = new CategoryCommand<string>(caption, category, image, OnNavigate, CanNavigate) { Order = order };
        }

        protected virtual bool CanNavigate(string arg)
        {
            return true;
        }

        protected virtual void OnNavigate(string obj)
        {
            Activate();
        }

        public abstract object GetVisibleView();

        protected sealed override void OnPostInitialization()
        {
            if (m_navigationCommand != null)
                m_navigationCommand.PublishEvent(EventTopicNames.NavigationCommandAdded);
            base.OnPostInitialization();
        }
    }
}
