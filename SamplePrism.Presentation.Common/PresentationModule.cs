using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using SamplePrism.Presentation.Common.Widgets;

namespace SamplePrism.Presentation.Common
{
    //[ModuleExport(typeof(PresentationModule))]
    public class PresentationModule : ModuleBase
    {
        public IEnumerable<IWidgetCreator> RegisteredCreators { get; set; }

        protected override void OnInitialization()
        {
            foreach (var registeredCreator in RegisteredCreators)
            {
                WidgetCreatorRegistry.RegisterWidgetCreator(registeredCreator);
            }
            base.OnInitialization();
        }
    }
}
