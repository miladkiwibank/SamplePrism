using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SimplePrism.Controls.Interaction
{
    internal class ConfirmationWindowViewModel : BindableBase
    {
        private readonly Window _window;

        public IEnumerable<ConfirmationWindowCommandButton> Buttons { get; set; }

        public string Question { get; set; }

        public DelegateCommand<string> ButtonClickCommand { get; set; }

        public string BackgroundColor { get; private set; }

        public string ForegroundColor { get; private set; }

        public bool IsVerticalLayout { get; set; }

        public bool IsHorizontalLayout { get; set; }

        public ConfirmationWindowViewModel(string question, string buttons, Window window, string background)
        {
            this._window = window;
            this.SetBackgroundColor(background);
            this.Question = question.Replace("\\r", Environment.NewLine);
            //this.Buttons =
            //    from button in buttons.SplitCsv()
            //    select new ConfirmationWindowCommandButton(button) into x
            //    where !string.IsNullOrEmpty(x.DisplayName)
            //    select x;
            this.IsHorizontalLayout = this.Buttons.Any((ConfirmationWindowCommandButton x) => !string.IsNullOrEmpty(x.Description));
            this.IsVerticalLayout = !this.IsHorizontalLayout;
            this.ButtonClickCommand = new DelegateCommand<string>(new Action<string>(this.OnButtonClick));
        }

        private void SetBackgroundColor(string backgroundColor)
        {
            this.BackgroundColor = backgroundColor;
            this.ForegroundColor = this.B2F(this.BackgroundColor);
        }

        private string B2F(string backgroundColor)
        {
            object obj = ColorConverter.ConvertFromString(backgroundColor);
            if (obj == null || ConfirmationWindowViewModel.Brightness((obj is Color) ? ((Color)obj) : default(Color)) >= 156)
            {
                return "Black";
            }
            return "White";
        }

        private static int Brightness(Color c)
        {
            return (int)Math.Sqrt((double)(c.R * c.R) * 0.241 + (double)(c.G * c.G) * 0.691 + (double)(c.B * c.B) * 0.068);
        }

        private void OnButtonClick(string obj)
        {
            this._window.Tag = obj;
            this._window.Close();
        }
    }
}