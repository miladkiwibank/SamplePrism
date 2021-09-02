using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Common.Services;
using SamplePrism.Presentation.Controls.Interaction;
using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;
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
            //TODO: LocalizationDictionary.ChangeLanguage(LocalSettings.CurrentLanguage);

            InteractionService.UserIntraction.ToggleSplashScreen();
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)Shell;
            InteractionService.UserIntraction.ToggleSplashScreen();
            Application.Current.MainWindow.Show();

            EventServiceFactory.EventService.PublishEvent(EventTopicNames.ShellInitlized); //通知Shell初始化完成
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
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
            Container.RegisterType<IModuleInitializationService, ModuleInitializationService>(new ContainerControlledLifetimeManager());
        }
    }
}
