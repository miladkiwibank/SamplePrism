using SimplePrism.Presentation.Common.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Common.Services
{
    /// <summary>
    /// 展示层菜单命令服务
    /// </summary>
    public static class PresentationServices
    {
        public static ObservableCollection<ICategoryCommand> CommandCategories { get; private set; }
    }
}
