using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SamplePrism.Presentation.Common.ModelBase
{
    /// <summary>
    /// EntityCollectionBaseView.xaml 的交互逻辑
    /// </summary>
    public partial class EntityCollectionBaseView : UserControl
    {
        private readonly Timer m_updateTimer;
        private string m_beforeText;

        public EntityCollectionBaseView()
        {
            InitializeComponent();
            m_updateTimer = new Timer(500);
            m_updateTimer.Elapsed += UpdateTimerElapsed;
        }

        private void UpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            m_updateTimer.Stop();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(RefreshItems));
        }

        private void RefreshItems()
        {
            if (FilterTextBox.Text != m_beforeText)
                ((EntityCollectionViewModelBase)DataContext).RefreshItems();
        }

        private void MainGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EntityCollectionViewModelBase bm && bm.EditItemCommand.CanExecute(null))
                bm.EditItemCommand.Execute(null);
        }

        private void MainGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (((EntityCollectionViewModelBase)DataContext).EditItemCommand.CanExecute(null))
                    ((EntityCollectionViewModelBase)DataContext).EditItemCommand.Execute(null);
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            m_updateTimer.Stop();
            if (e.Key == Key.Enter)
            {
                ((EntityCollectionViewModelBase)DataContext).RefreshItems();
                FilterTextBox.SelectAll();
            }
            else if (e.Key == Key.Down)
            {
                //MainGrid.BackgroundFocus();
            }
            else
            {
                m_beforeText = FilterTextBox.Text;
                m_updateTimer.Start();
            }
        }
    }
}
