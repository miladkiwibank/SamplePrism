﻿using System.ComponentModel.Composition;
using SamplePrism.Domain.Models.Tickets;

namespace SamplePrism.Services.Implementations.PrinterModule.ValueChangers
{
    [Export]
    public class TicketEntityValueChanger : AbstractValueChanger<TicketEntity>
    {
        private readonly ICacheService _cacheService;

        [ImportingConstructor]
        public TicketEntityValueChanger(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public override string GetTargetTag()
        {
            return "ENTITIES";
        }

        protected override string GetModelName(TicketEntity model)
        {
            var entityType = _cacheService.GetEntityTypeById(model.EntityTypeId);
            return entityType == null ? "" : entityType.EntityName;
        }
    }
}
