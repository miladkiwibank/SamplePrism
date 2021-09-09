using System.Windows;
using System.Windows.Controls;
using SamplePrism.Domain.Models.Entities;
using SamplePrism.Presentation.Services;

namespace SamplePrism.Presentation.Common.Widgets
{
    public interface IWidgetCreator
    {
        string GetCreatorName();
        string GetCreatorDescription();
        FrameworkElement CreateWidgetControl(IDiagram widget, ContextMenu contextMenu);
        Widget CreateNewWidget();
        IDiagram CreateWidgetViewModel(Widget widget, IApplicationState applicationState);
    }
}