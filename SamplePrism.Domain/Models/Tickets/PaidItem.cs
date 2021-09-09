using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Tickets
{
    public class PaidItem : ValueClass
    {
        public string Key { get; set; }
        public decimal Quantity { get; set; }
        public int TicketId { get; set; }
    }
}
