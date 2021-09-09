using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyTools.Wpf;
using SamplePrism.Controls.Properties;
using SamplePrism.Presentation.Common.ModelBase;
using SamplePrism.Presentation.Common.Services;

namespace SamplePrism.Presentation.Controls.Interaction
{
    /// <summary>
    /// Interaction logic for PropertyEditorForm.xaml
    /// </summary>
    public partial class PropertyEditorForm : Window
    {
        public PropertyEditorForm()
        {
            InitializeComponent();
            Height = Settings.Default.PEHeight;
            Width = Settings.Default.PEWidth;
            PropertyEditorControl.PropertyControlFactory = new PropertyControlFactory();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.PEHeight = Height;
            Settings.Default.PEWidth = Width;
        }
    }
}
