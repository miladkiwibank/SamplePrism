using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace SimplePrism.Presentation.Controls.Interaction
{
    public class CloseBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty StoryboardProperty = DependencyProperty.Register("Storyboard", typeof(Storyboard), typeof(CloseBehavior), new PropertyMetadata(null));

        public Storyboard Storyboard
        {
            get
            {
                return (Storyboard)base.GetValue(CloseBehavior.StoryboardProperty);
            }
            set
            {
                base.SetValue(CloseBehavior.StoryboardProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Closing += new CancelEventHandler(this.onWindowClosing);
        }

        private void onWindowClosing(object sender, CancelEventArgs e)
        {
            if (this.Storyboard == null)
            {
                return;
            }
            e.Cancel = true;
            base.AssociatedObject.Closing -= new CancelEventHandler(this.onWindowClosing);
            this.Storyboard.Completed += delegate (object o, EventArgs a)
            {
                base.AssociatedObject.Close();
            };
            this.Storyboard.Begin(base.AssociatedObject);
        }
    }
}