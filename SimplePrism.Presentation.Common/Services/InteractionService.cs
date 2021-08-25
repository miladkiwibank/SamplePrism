using Microsoft.Practices.ServiceLocation;
using SimplePrism.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimplePrism.Presentation.Common.Services
{
    public static class InteractionService
    {
        public struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
        }

        static InteractionService()
        {
            UserIntraction = ServiceLocator.Current.GetInstance<IUserInteraction>();
            DialogService = ServiceLocator.Current.GetInstance<IDialogService>();
        }

        private const uint WmMousefirst = 512u;
        private const uint WmMouselast = 521u;

        public static IUserInteraction UserIntraction { get; private set; }

        public static IDialogService DialogService { get; private set; }
    }
}
