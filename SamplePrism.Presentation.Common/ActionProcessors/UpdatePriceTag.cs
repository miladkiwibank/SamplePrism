using System.ComponentModel.Composition;
using SamplePrism.Localization.Properties;
using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;
using SamplePrism.Services;
using SamplePrism.Services.Common;

namespace SamplePrism.Presentation.Common.ActionProcessors
{
    [Export(typeof(IActionType))]
    class UpdatePriceTag : ActionType
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMethodQueue _methodQueue;
        private readonly ITriggerService _triggerService;
        private readonly IApplicationState _applicationState;

        [ImportingConstructor]
        public UpdatePriceTag(IDepartmentService departmentService, IMethodQueue methodQueue, ITriggerService triggerService,
            IApplicationState applicationState)
        {
            _departmentService = departmentService;
            _methodQueue = methodQueue;
            _triggerService = triggerService;
            _applicationState = applicationState;
        }

        public override void Process(ActionData actionData)
        {
            var priceTag = actionData.GetAsString("PriceTag");
            var departmentName = actionData.GetAsString("DepartmentName");
            _departmentService.UpdatePriceTag(departmentName, priceTag);
            _methodQueue.Queue("ResetCache", () => Helper.ResetCache(_triggerService, _applicationState));
        }

        protected override object GetDefaultData()
        {
            return new { DepartmentName = "", PriceTag = "" };
        }

        protected override string GetActionName()
        {
            return Resources.UpdatePriceTag;
        }

        protected override string GetActionKey()
        {
            return ActionNames.UpdatePriceTag;
        }
    }
}
