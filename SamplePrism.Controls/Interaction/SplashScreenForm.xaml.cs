using System.ComponentModel;
using System.Windows;

namespace SamplePrism.Presentation.Controls.Interaction
{
    /// <summary>
    /// SplashScreenForm.xaml 的交互逻辑
    /// </summary>
    public partial class SplashScreenForm : Window
    {
        public SplashScreenForm()
        {
            this.InitializeComponent();
        }

        private void SplashScreenForm_OnClosing(object sender, CancelEventArgs e)
        {
            (sender as Window).Topmost = true;
        }
    }
}