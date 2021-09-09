using SamplePrism.Controls.Properties;
using System.Windows;

namespace SamplePrism.Presentation.Controls.Interaction
{
    /// <summary>
    /// Interaction logic for ListSorterForm.xaml
    /// </summary>
    public partial class ListSorterForm : Window
    {
        public ListSorterForm()
        {
            InitializeComponent();
            Height = Settings.Default.LSHeight;
            Width = Settings.Default.LSWidth;
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.LSHeight = Height;
            Settings.Default.LSWidth = Width;
            Settings.Default.Save();
        }
    }
}
