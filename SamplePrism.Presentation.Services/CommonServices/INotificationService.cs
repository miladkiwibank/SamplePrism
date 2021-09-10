﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamplePrism.Services.Common;

namespace SamplePrism.Services
{
    public interface INotificationService
    {
        void NotifyEvent(string eventName, object dataObject, int terminalId, int departmentId, int userRoleId, int ticketTypeId, Action<ActionData> dataAction);
    }
}
