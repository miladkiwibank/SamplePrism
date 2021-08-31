using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Network
{
    public static class TelnetExtensions
    {
        /// <summary>
        /// 检测指定的IP地址的端口号是否允许Telnet。
        /// </summary>
        /// <param name="ip">要检测的IP地址。</param>
        /// <param name="port">要检测的端口号。</param>
        /// <param name="timeout">Telnet的超时时间，默认1000毫秒。</param>
        /// <returns>true：telnet成功；false：telnet失败。</returns>
        public static bool Telnet(this string ip, int port, int timeout = 1000)
        {
            if (port >= 65535 || port <= 0)
            {
                return false;
            }
            if (!IPAddress.TryParse(ip, out IPAddress address))
            {
                return false;
            }
            try
            {
                bool success = false;
                using (Socket socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        ManualResetEvent manual = new ManualResetEvent(initialState: false);
                        socket.BeginConnect(ip, port, delegate (IAsyncResult ar)
                        {
                            Socket socket2 = (Socket)ar.AsyncState;
                            if (socket2 != null)
                            {
                                try
                                {
                                    socket2.EndConnect(ar);
                                    success = true;
                                }
                                catch
                                {
                                    success = false;
                                }
                                finally
                                {
                                    manual.Set();
                                }
                            }
                        }, socket);
                        manual.WaitOne(timeout, exitContext: false);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 异步检测指定的IP地址的端口号是否允许Telnet。
        /// </summary>
        /// <param name="ip">要检测的IP地址。</param>
        /// <param name="port">要检测的端口号。</param>
        /// <param name="timeout">Telnet的超时时间，默认1000毫秒。</param>
        /// <returns>true：telnet成功；false：telnet失败。</returns>
        public static async Task<bool> TelnetAsync(this string ip, int port, int timeout = 1000)
        {
            return await Task.Factory.StartNew(() => ip.Telnet(port, timeout)).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
