using SimplePrism.Presentation.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Common
{
    public static class ExceptionReporter
    {
        public static void Show(params Exception[] exceptions)
        {
            if (exceptions == null)
            {
                return;
            }
            try
            {
                ErrorReportViewModel errorReportViewModel = new ErrorReportViewModel(exceptions);
                ErrorReportView errorReportView = new ErrorReportView() { DataContext = errorReportViewModel };
                errorReportView.ShowDialog();
                string errorMessage = errorReportViewModel.GetErrorReport();
                //Logger.Default.Error(errorMessage);
            }
            catch (Exception ex)
            {
                InteractionService.UserIntraction.DisplayPopup("", "", ex.Message);
            }
        }
    }
}
