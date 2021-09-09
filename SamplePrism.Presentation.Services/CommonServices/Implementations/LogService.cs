using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using SamplePrism.Infrastructure.ExceptionReporter;
using SamplePrism.Localization.Properties;

namespace SamplePrism.Services.Implementations
{

    public class LogService : ILogService
    {
        public void LogError(Exception e)
        {
            MessageBox.Show(Resources.ErrorLogMessage + e.Message, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Stop);
            Logger.Log(e);
        }

        public void LogError(Exception e, string userMessage)
        {
            MessageBox.Show(userMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
            Logger.Log(e);
        }

        public void Log(string message)
        {
            Logger.Log(message);
        }
    }
}
