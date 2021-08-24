using SimplePrism.Presentation.Properties;
using SimplePrism.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Microsoft.Practices.Unity;
using System.IO;

namespace SimplePrism.Presentation
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //private Mutex m_mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //程序单实例运行
            //m_mutex = new Mutex(true, "SimplePrism", out bool createdNew);
            //if (!createdNew)
            //{
            //    Environment.Exit(0);
            //}

            var process = RunningInstance();

            if (process != null)
            {
                HandleRunningInstance(process);

                Environment.Exit(Environment.ExitCode);
                return;
            }

            //是否启动调试窗
            if (Settings.Default.DEBUG)
            {
                ConsoleManager.Toggle();
                ConsoleManager.Show();
            }

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();

                //验证授权路数
                //#if RELEASE
                //if (!Register.CheckOrRegister(out int MaxChannelCount))
                //{
                //    Environment.Exit(Environment.ExitCode);
                //}
                //var applicationStateSetter = bootstrapper.Container.Resolve<IApplicationStateSetter>();
                //applicationStateSetter.SetMaxChannelCount(MaxChannelCount);
                //#endif
            }
            catch (ConfigurationErrorsException ex)
            {
                HandleConfigurationError(ex);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private void HandleConfigurationError(ConfigurationErrorsException ex)
        {
            try
            {
                string path = (ex.InnerException is ConfigurationErrorsException ex2) ? ex2.Filename : ex.Filename;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            finally
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            if (ex == null) return;

            //TODO: Show exception dialog
            Environment.Exit(1);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        #region 应用单例运行
        private static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);

            foreach (Process item in processes)
            {
                if (item.Id != currentProcess.Id)
                {
                    return item;
                }
            }

            return null;
        }

        private const int SW_SHOWNOMAL = 1;
        private const int SW_MAXIMIZE = 3;

        ///<summary>
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL); //显示
            SetForegroundWindow(instance.MainWindowHandle); //到最前端
        }
        #endregion
    }
}
