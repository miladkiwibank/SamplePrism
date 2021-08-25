using SimplePrism.Controls.Interaction;
using System.Windows;

namespace SimplePrism.Presentation.Controls.Interaction
{
    /// <summary>
    /// ConfirmationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public ConfirmationWindow()
        {
            this.InitializeComponent();
        }

        public ConfirmationWindow(string question, string buttons, string backgroundColor)
            : this()
        {
            base.DataContext = new ConfirmationWindowViewModel(question, buttons, this, backgroundColor);
        }
    }
}