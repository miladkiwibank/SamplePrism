﻿using System.Collections.Generic;
using System.Windows.Threading;
using SamplePrism.Domain.Models.Accounts;
using SamplePrism.Domain.Models.Entities;
using SamplePrism.Domain.Models.Menus;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Domain.Models.Users;
using SamplePrism.Presentation.Services.Common;
using SamplePrism.Services.Common;

namespace SamplePrism.Presentation.Services
{
    public class CurrentDepartmentData
    {
        public Department Model { get; set; }
        public int Id { get { return Model != null ? Model.Id : 0; } }
        public string Name { get { return Model != null ? Model.Name : ""; } }
        public string PriceTag { get { return Model != null ? Model.PriceTag : ""; } }
        public int TicketCreationMethod { get { return Model != null ? Model.TicketCreationMethod : 0; } }
        public int TicketTypeId { get { return Model != null ? Model.TicketTypeId : 0; } }
        public int ScreenMenuId { get { return Model != null ? Model.ScreenMenuId : 0; } }
    }

    public interface IApplicationState
    {
        Dispatcher MainDispatcher { get; set; }
        AppScreens ActiveAppScreen { get; }

        string NumberPadValue { get; }
        User CurrentLoggedInUser { get; }
        CurrentDepartmentData CurrentDepartment { get; }
        TicketType CurrentTicketType { get; set; }
        TicketType TempTicketType { get; set; }
        EntityScreen SelectedEntityScreen { get; }
        EntityScreen TempEntityScreen { get; }
        WorkPeriod CurrentWorkPeriod { get; }
        WorkPeriod PreviousWorkPeriod { get; }
        bool IsCurrentWorkPeriodOpen { get; }
        bool IsLocked { get; }
        Terminal CurrentTerminal { get; }
        bool IsLandscape { get; set; }

        ProductTimer GetProductTimer(int menuItemId);
        IEnumerable<OrderTagGroup> GetOrderTagGroups(params int[] menuItemIds);
        IEnumerable<AccountTransactionDocumentType> GetAccountTransactionDocumentTypes(int accountTypeId);
        IEnumerable<AccountTransactionDocumentType> GetBatchDocumentTypes(IEnumerable<string> accountTypeNamesList);
        IEnumerable<PaymentType> GetPaymentScreenPaymentTypes();
        IEnumerable<ChangePaymentType> GetChangePaymentTypes();
        IEnumerable<TicketTagGroup> GetTicketTagGroups();
        IEnumerable<AutomationCommandData> GetAutomationCommands();
        IEnumerable<CalculationSelector> GetCalculationSelectors();
        IEnumerable<EntityScreen> GetEntityScreens();
        IEnumerable<EntityScreen> GetTicketEntityScreens();
        IEnumerable<TaxTemplate> GetTaxTemplates(int menuItemId);

        Printer GetReportPrinter();
        Printer GetTransactionPrinter();

        void NotifyEvent(string eventName, object dataObject);

        void ResetState();
    }
}
