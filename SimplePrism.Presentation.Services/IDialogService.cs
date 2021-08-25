using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// 弹出消息
        /// </summary>
        void ShowMessage(string message);

        /// <summary>
        /// 弹出确认.
        /// </summary>
        /// <param name="question">询问的内容.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Confirm(string question);
    }
}
