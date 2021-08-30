using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using NLog;
using SimplePrism.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace SimplePrism.Services
{
    class Program
    {
        static ILogger m_logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
#if DEBUG
#else
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif
            HostFactory.Run(x =>
            {
                x.UseNLog();
                x.Service<DeviceServiceControl>();
                x.SetServiceName("SamplePrismService");
                x.SetDisplayName("SamplePrism数据接入服务");
                x.SetDescription("SamplePrism 服务端");
            });
#if DEBUG
            while (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                return;
            }
#endif
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                m_logger.Error(ex);
            }
        }
    }

    class DeviceServiceControl : ServiceControl, ServiceSuspend
    {
        private ILogger m_logger = LogManager.GetCurrentClassLogger();

        IEventLoopGroup udpWorkerGroup = new MultithreadEventLoopGroup();
        Bootstrap udpBootstrap = new Bootstrap();
        IChannel udpChannel;

        public DeviceServiceControl()
        {
            //TODO: 读取配置参数,加载服务
            //int port = Settings.Default.Port;


        }

        public bool Continue(HostControl hostControl)
        {
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            return true;
        }

        public bool Start(HostControl hostControl)
        {
            udpBootstrap.Group(udpWorkerGroup)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Option(ChannelOption.SoReuseaddr, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    channel.Pipeline.AddLast("PA_UDP", new UdpServerHandler());
                }));
            udpChannel = udpBootstrap.BindAsync(Settings.Default.Port).Result;

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            udpChannel.CloseAsync();
            udpWorkerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            return true;
        }
    }

    class UdpServerHandler : SimpleChannelInboundHandler<DatagramPacket>
    {
        private readonly ILogger m_logger = LogManager.GetCurrentClassLogger();

        protected override void ChannelRead0(IChannelHandlerContext ctx, DatagramPacket msg)
        {
            if (!msg.Content.IsReadable()) return;

            string message = msg.Content.ToString(Encoding.UTF8);

            m_logger.Debug(message);

            //Receive
            //byte[] bytes = Encoding.UTF8.GetBytes("Hello client" + DateTime.Now.Ticks);

            //IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);

            //ctx.WriteAndFlushAsync(new DatagramPacket(buffer, msg.Sender));
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //base.ExceptionCaught(context, exception);

            m_logger.Error(exception);
            context.CloseAsync();
        }

    }
}
