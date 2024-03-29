﻿using System;
using System.Windows;
using System.Windows.Threading;
using SamplePrism.Infrastructure.Settings;
using SamplePrism.Localization.Properties;

namespace SamplePrism.Services.Implementations.PrinterModule
{
    public class AsyncPrintTask
    {
        public static void Exec(bool highPriority, Action action, ILogService logService)
        {
            if (highPriority)
            {
                InternalExec(action, logService);
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                   new Action(() => InternalExec(action, logService)));
            }
        }

        private static void InternalExec(Action action, ILogService logService)
        {
            try
            {
                LocalSettings.UpdateThreadLanguage();
                action.Invoke();
            }
            catch (Exception e)
            {
                logService.LogError(e, Resources.PrintErrorMessage + e.Message);
            }
        }
    }
}