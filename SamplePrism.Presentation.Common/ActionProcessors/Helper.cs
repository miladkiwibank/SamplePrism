using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;

namespace SamplePrism.Presentation.Common.ActionProcessors
{
    internal static class Helper
    {
        public static void ResetCache(ITriggerService triggerService, IApplicationState applicationState)
        {
            triggerService.UpdateCronObjects();
            EventServiceFactory.EventService.PublishEvent(EventTopicNames.ResetCache, true);
            applicationState.ResetState();
            applicationState.CurrentDepartment.PublishEvent(EventTopicNames.SelectedDepartmentChanged);
            applicationState.CurrentTicketType.PublishEvent(EventTopicNames.TicketTypeChanged);
        }
    }
}
