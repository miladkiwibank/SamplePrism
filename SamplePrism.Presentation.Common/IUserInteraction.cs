using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common
{
    public interface IUserInteraction
    {
        void ToggleSplashScreen();

        //void ActivateSplashScreen();

        /// <summary>
        /// 弹出是否操作提示框
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        bool AskQuestion(string question);

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        void GiveFeedBack(string message);

        /// <summary>
        /// 右下角弹出提示
        /// </summary>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="headerColor"></param>
        /// <param name="action"></param>
        /// <param name="actionParameter"></param>
        void DisplayPopup(string name, string title, string content, string headerColor = "DarkRed",
            Action<object> action = null, object actionParameter = null);
    }
}
