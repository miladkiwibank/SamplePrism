using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using SamplePrism.Domain.Models.Accounts;
using SamplePrism.Domain.Models.Settings;

namespace SamplePrism.Services
{
    public interface IReportService
    {
        void PrintAccountScreen(AccountScreen accountScreen, WorkPeriod workperiod, Printer printer);
        void PrintAccountTransactions(Account account, WorkPeriod workPeriod, Printer printer, string filter);
    }
}
