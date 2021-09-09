using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using SamplePrism.Infrastructure.Settings;
using SamplePrism.Localization.Engine;
using SamplePrism.Persistance;
using SamplePrism.Persistance.Implementations;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Common.Services;
using SamplePrism.Presentation.Controls.Interaction;
using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;
using SamplePrism.Presentation.Services.Implementations;
using SamplePrism.Presentation.Services.Implementations.UserModule;
using SamplePrism.Services;
using SamplePrism.Services.Implementations;
using SamplePrism.Services.Implementations.AccountModule;
using SamplePrism.Services.Implementations.AutomationModule;
using SamplePrism.Services.Implementations.DepartmentModule;
using SamplePrism.Services.Implementations.EntityModule;
using SamplePrism.Services.Implementations.ExpressionModule;
using SamplePrism.Services.Implementations.PrinterModule;
using SamplePrism.Services.Implementations.SettingsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace SamplePrism.Presentation
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            //return base.CreateShell();
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            LocalizeDictionary.ChangeLanguage(LocalSettings.CurrentLanguage);

            InteractionService.UserIntraction.ToggleSplashScreen();
            ServiceLocator.Current.GetInstance<IApplicationState>().MainDispatcher = Application.Current.Dispatcher;

            base.InitializeShell();

            Application.Current.MainWindow = (Shell)Shell;
            InteractionService.UserIntraction.ToggleSplashScreen();
            Application.Current.MainWindow.Show();

            EventServiceFactory.EventService.PublishEvent(EventTopicNames.ShellInitialized); //通知Shell初始化完成
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            var moduleInitializationService = ServiceLocator.Current.GetInstance<IModuleInitializationService>();
            moduleInitializationService.Initialize();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //return base.CreateModuleCatalog();
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IApplicationState, ApplicationState>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IApplicationStateSetter, ApplicationState>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserInteraction, UserInteraction>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMethodQueue, MethodQueue>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IModuleInitializationService, ModuleInitializationService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IDepartmentService, DepartmentService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICacheService, CacheService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICacheDao, CacheDao>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISettingService, SettingService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISettingDao, SettingDao>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IPrinterService, PrinterService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPrinterDao, PrinterDao>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IExpressionService, ExpressionService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IAutomationDao, AutomationDao>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IEntityService, EntityService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEntityDao, EntityDao>(new ContainerControlledLifetimeManager());

            Container.RegisterType<INotificationService, NotificationService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<ILogService, LogService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAccountDao, AccountDao>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserDao, UserDao>(new ContainerControlledLifetimeManager());
        }
    }
}
