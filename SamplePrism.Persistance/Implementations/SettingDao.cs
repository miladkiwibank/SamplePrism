using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Infrastructure;
using SamplePrism.Domain.Models.Settings;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Infrastructure.Data;
using SamplePrism.Infrastructure.Data.Validation;
using SamplePrism.Localization.Properties;
using SamplePrism.Persistance.Data;

namespace SamplePrism.Persistance.Implementations
{
    public class SettingDao : ISettingDao
    {
        public SettingDao()
        {
            ValidatorRegistry.RegisterDeleteValidator(new NumeratorDeleteValidator());
        }

        public string GetNextString(int numeratorId)
        {
            using (var workspace = WorkspaceFactory.Create())
            {
                var numerator = workspace.Single<Numerator>(x => x.Id == numeratorId);
                numerator.Number++;
                try
                {
                    workspace.CommitChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return GetNextString(numeratorId);
                }
                return numerator.GetNumber();
            }
        }

        public int GetNextNumber(int numeratorId)
        {
            using (var workspace = WorkspaceFactory.Create())
            {
                var numerator = workspace.Single<Numerator>(x => x.Id == numeratorId);
                numerator.Number++;
                try
                {
                    workspace.CommitChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return GetNextNumber(numeratorId);
                }
                return numerator.Number;
            }
        }

        public IEnumerable<Terminal> GetTerminals()
        {
            return Dao.Query<Terminal>();
        }
    }

    internal class NumeratorDeleteValidator : SpecificationValidator<Numerator>
    {
        public override string GetErrorMessage(Numerator model)
        {
            if (Dao.Exists<TicketType>(x => x.OrderNumerator.Id == model.Id))
                return string.Format(Resources.DeleteErrorUsedBy_f, Resources.Numerator, Resources.TicketType);
            if (Dao.Exists<TicketType>(x => x.TicketNumerator.Id == model.Id))
                return string.Format(Resources.DeleteErrorUsedBy_f, Resources.Numerator, Resources.TicketType);
            return "";
        }
    }
}
