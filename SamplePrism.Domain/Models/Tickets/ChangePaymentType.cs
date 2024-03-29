﻿using System.Collections.Generic;
using SamplePrism.Domain.Models.Accounts;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Tickets
{
    public class ChangePaymentType : EntityClass, IOrderable
    {
        public ChangePaymentType()
        {
            _changePaymentTypeMaps = new List<ChangePaymentTypeMap>();
        }

        public int SortOrder { get; set; }
        public string UserString { get { return Name; } }
        public virtual AccountTransactionType AccountTransactionType { get; set; }
        public virtual Account Account { get; set; }
        
        private IList<ChangePaymentTypeMap> _changePaymentTypeMaps;
        public virtual IList<ChangePaymentTypeMap> ChangePaymentTypeMaps
        {
            get { return _changePaymentTypeMaps; }
            set { _changePaymentTypeMaps = value; }
        }

        public ChangePaymentTypeMap AddPChangeaymentTemplateMap()
        {
            var map = new ChangePaymentTypeMap();
            ChangePaymentTypeMaps.Add(map);
            return map;
        }

    }
}
