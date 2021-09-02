using SamplePrism.Modules.ManagementModule.Views;
using Prism.Modularity;
using Prism.Regions;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Services.Common;
using SamplePrism.Modules.ManagementModule.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace SamplePrism.Modules.ManagementModule
{
    public class ManagementModule : VisibleModuleBase
    {
        private readonly IRegionManager m_regionManager;
        private readonly ManagementView m_dashboardView;

        public ManagementModule(IRegionManager regionManager, ManagementView dashboardView/*, UserManager userManager*/)
            : base(regionManager, AppScreens.Management)
        {
            m_regionManager = regionManager;
            m_dashboardView = dashboardView;
            SetNavigationCommand("管理", "普通", "", 70);
            PermissionRegistry.RegisterPermission(PermissionNames.OpenDashboard, PermissionCategories.Navigation, "可以打开管理界面");
        }

        protected override bool CanNavigate(string arg)
        {
            //return base.CanNavigate(arg);
            return true;
        }

        protected override void OnNavigate(string obj)
        {
            base.OnNavigate(obj);
            ((ManagementViewModel)m_dashboardView.DataContext).Refresh();
        }

        public override object GetVisibleView()
        {
            return m_dashboardView;
        }

        protected override void OnPreInitialization()
        {
            ViewModelLocationProvider.Register<ManagementView, ManagementViewModel>();

            //base.OnPreInitialization();
            m_regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ManagementView));
        }

    }
}