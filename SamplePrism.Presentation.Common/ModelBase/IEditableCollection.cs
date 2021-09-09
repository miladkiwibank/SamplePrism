using SamplePrism.Presentation.Common.Commands;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public interface IEditableCollection
    {
        ICaptionCommand AddItemCommand { get; set; }
        ICaptionCommand EditItemCommand { get; set; }
        ICaptionCommand DeleteItemCommand { get; set; }
    }
}
