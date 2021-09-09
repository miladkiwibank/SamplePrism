using System.ComponentModel.Composition;
using SamplePrism.Domain.Models.Tickets;

namespace SamplePrism.Services.Implementations.PrinterModule.ValueChangers
{
    [Export]
    public class PostCalculationValueChanger : AbstractValueChanger<Calculation>
    {
        public override string GetTargetTag()
        {
            return "SERVICES";
        }

        protected override string GetModelName(Calculation model)
        {
            return model.Name;
        }
    }
}
