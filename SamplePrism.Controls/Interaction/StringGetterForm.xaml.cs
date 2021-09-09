using SamplePrism.Controls.Properties;
using System;
using System.Windows;

namespace SamplePrism.Presentation.Controls.Interaction
{
    /// <summary>
    /// Interaction logic for StringGetterForm.xaml
    /// </summary>
    public partial class StringGetterForm : Window
    {
        public StringGetterForm()
        {
            InitializeComponent();
            Width = Settings.Default.SGWidth;
            Height = Settings.Default.SGHeight;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "";
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            TextBox.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.SGHeight = Height;
            Settings.Default.SGWidth = Width;
        }
    }
}
