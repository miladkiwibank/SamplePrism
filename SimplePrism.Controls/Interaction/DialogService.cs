using SimplePrism.Presentation.Controls.Interaction;
using SimplePrism.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Controls.Interaction
{
    internal class DialogService : IDialogService
    {
        public bool Confirm(string question)
        {
            ConfirmationWindow confirmationWindow = new ConfirmationWindow(question,
                string.Format("{0},{1}", "确认", "取消"), "White");
            return confirmationWindow.ShowDialog().GetValueOrDefault();

        }

        public void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
