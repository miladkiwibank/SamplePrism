using System.ComponentModel.Composition;
using SamplePrism.Domain.Models.Accounts;

namespace SamplePrism.Services.Implementations.PrinterModule.ValueChangers
{
    [Export]
    public class AccountTransactionValueChanger : AbstractValueChanger<AccountTransaction>
    {
        public override string GetTargetTag()
        {
            return "TRANSACTIONS";
        }

        protected override string GetModelName(AccountTransaction model)
        {
            return model.Name;
        }
    }
}