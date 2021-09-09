using System.ComponentModel.Composition;
using SamplePrism.Domain.Models.Tickets;

namespace SamplePrism.Services.Implementations.PrinterModule.ValueChangers
{
    [Export]
    public class PreCalculationValueChanger : AbstractValueChanger<Calculation>
    {
        public override string GetTargetTag()
        {
            return "DISCOUNTS";
        }

        protected override string GetModelName(Calculation model)
        {
            return model.Name;
        }
    }
}
