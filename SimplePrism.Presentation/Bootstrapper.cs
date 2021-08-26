using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using SimplePrism.Presentation.Common.Services;
using SimplePrism.Presentation.Controls.Interaction;
using SimplePrism.Presentation.Services;
using SimplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace SimplePrism.Presentation
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
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
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
            return base.CreateModuleCatalog();
            //return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
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
        }
    }
}
