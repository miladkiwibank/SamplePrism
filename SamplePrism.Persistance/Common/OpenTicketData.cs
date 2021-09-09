using System;
using System.Collections.Generic;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Infrastructure.Helpers;

namespace SamplePrism.Persistance.Common
{
    public class OpenTicketData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastOrderDate { get; set; }
        public string TicketNumber { get; set; }
        public decimal RemainingAmount { get; set; }
        public IEnumerable<TicketEntity> TicketEntities { get; set; }
        public string TicketTags { get; set; }

        private List<TicketTagValue> _ticketTagValues;
        public List<TicketTagValue> TicketTagValues
        {
            get { return _ticketTagValues ?? (_ticketTagValues = JsonHelper.Deserialize<List<TicketTagValue>>(TicketTags)); }
        }
    }
}