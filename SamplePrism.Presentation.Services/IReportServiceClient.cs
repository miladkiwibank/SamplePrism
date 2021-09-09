using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamplePrism.Domain.Models.Accounts;

namespace SamplePrism.Presentation.Services
{
    public interface IReportServiceClient
    {
        void PrintAccountScreen(AccountScreen accountScreen);
        void PrintAccountTransactions(Account account,string filter);
    }
}
