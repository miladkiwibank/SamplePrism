using System.Collections.Generic;
using SamplePrism.Domain.Models.Accounts;
using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Tickets
{
    public class CalculationType : EntityClass, IOrderable
    {
        public int SortOrder { get; set; }

        public string UserString
        {
            get { return Name; }
        }

        public int CalculationMethod { get; set; }
        public decimal Amount { get; set; }
        public decimal MaxAmount { get; set; }
        public bool IncludeTax { get; set; }
        public bool DecreaseAmount { get; set; }
        public bool UsePlainSum { get; set; }
        public bool ToggleCalculation { get; set; }
        public virtual AccountTransactionType AccountTransactionType { get; set; }
    }
}
