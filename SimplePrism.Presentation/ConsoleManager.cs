using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class ConsoleManager
    {
        private static readonly ConsoleCtrlDelegate ConsoleCtrlDelegateHandlerRoutine = HandlerRoutine;

        #region Code Page Identifiers

        private static readonly Dictionary<int, string[]> DicCodePageIdentifiers = new Dictionary<int, string[]>
    {
        {37, new[] {"IBM037", "IBM EBCDIC US-Canada"}},
        {437, new[] {"IBM437", "OEM United States"}},
        {500, new[] {"IBM500", "IBM EBCDIC International"}},
        {708, new[] {"ASMO-708", "Arabic (ASMO 708)"}},
        {709, new[] {"", "Arabic (ASMO-449+, BCON V4)"}},
        {710, new[] {"", "Arabic - Transparent Arabic"}},
        {720, new[] {"DOS-720", "Arabic (Transparent ASMO); Arabic (DOS)"}},
        {737, new[] {"ibm737", "OEM Greek (formerly 437G); Greek (DOS)"}},
        {775, new[] {"ibm775", "OEM Baltic; Baltic (DOS)"}},
        {850, new[] {"ibm850", "OEM Multilingual Latin 1; Western European (DOS)"}},
        {852, new[] {"ibm852", "OEM Latin 2; Central European (DOS)"}},
        {855, new[] {"IBM855", "OEM Cyrillic (primarily Russian)"}},
        {857, new[] {"ibm857", "OEM Turkish; Turkish (DOS)"}},
        {858, new[] {"IBM00858", "OEM Multilingual Latin 1 + Euro symbol"}},
        {860, new[] {"IBM860", "OEM Portuguese; Portuguese (DOS)"}},
        {861, new[] {"ibm861", "OEM Icelandic; Icelandic (DOS)"}},
        {862, new[] {"DOS-862", "OEM Hebrew; Hebrew (DOS)"}},
        {863, new[] {"IBM863", "OEM French Canadian; French Canadian (DOS)"}},
        {864, new[] {"IBM864", "OEM Arabic; Arabic (864)"}},
        {865, new[] {"IBM865", "OEM Nordic; Nordic (DOS)"}},
        {866, new[] {"cp866", "OEM Russian; Cyrillic (DOS)"}},
        {869, new[] {"ibm869", "OEM Modern Greek; Greek, Modern (DOS)"}},
        {870, new[] {"IBM870", "IBM EBCDIC Multilingual/ROECE (Latin 2); IBM EBCDIC Multilingual Latin 2"}},
        {874, new[] {"windows-874", "ANSI/OEM Thai (ISO 8859-11); Thai (Windows)"}},
        {875, new[] {"cp875", "IBM EBCDIC Greek Modern"}},
        {932, new[] {"shift_jis", "ANSI/OEM Japanese; Japanese (Shift-JIS)"}},
        {936, new[] {"gb2312", "ANSI/OEM Simplified Chinese (PRC, Singapore); Chinese Simplified (GB2312)"}},
        {949, new[] {"ks_c_5601-1987", "ANSI/OEM Korean (Unified Hangul Code)"}},
        {950, new[] {"big5", "ANSI/OEM Traditional Chinese (Taiwan; Hong Kong SAR, PRC); Chinese Traditional (Big5)"}},
        {1026, new[] {"IBM1026", "IBM EBCDIC Turkish (Latin 5)"}},
        {1047, new[] {"IBM01047", "IBM EBCDIC Latin 1/Open System"}},
        {1140, new[] {"IBM01140", "IBM EBCDIC US-Canada (037 + Euro symbol); IBM EBCDIC (US-Canada-Euro)"}},
        {1141, new[] {"IBM01141", "IBM EBCDIC Germany (20273 + Euro symbol); IBM EBCDIC (Germany-Euro)"}},
        {1142, new[] {"IBM01142", "IBM EBCDIC Denmark-Norway (20277 + Euro symbol); IBM EBCDIC (Denmark-Norway-Euro)"}},
        {1143, new[] {"IBM01143", "IBM EBCDIC Finland-Sweden (20278 + Euro symbol); IBM EBCDIC (Finland-Sweden-Euro)"}},
        {1144, new[] {"IBM01144", "IBM EBCDIC Italy (20280 + Euro symbol); IBM EBCDIC (Italy-Euro)"}},
        {1145, new[] {"IBM01145", "IBM EBCDIC Latin America-Spain (20284 + Euro symbol); IBM EBCDIC (Spain-Euro)"}},
        {1146, new[] {"IBM01146", "IBM EBCDIC United Kingdom (20285 + Euro symbol); IBM EBCDIC (UK-Euro)"}},
        {1147, new[] {"IBM01147", "IBM EBCDIC France (20297 + Euro symbol); IBM EBCDIC (France-Euro)"}},
        {1148, new[] {"IBM01148", "IBM EBCDIC International (500 + Euro symbol); IBM EBCDIC (International-Euro)"}},
        {1149, new[] {"IBM01149", "IBM EBCDIC Icelandic (20871 + Euro symbol); IBM EBCDIC (Icelandic-Euro)"}},
        {1200, new[] {"utf-16", "Unicode UTF-16, little endian byte order (BMP of ISO 10646); available only to managed applications"}},
        {1201, new[] {"unicodeFFFE", "Unicode UTF-16, big endian byte order; available only to managed applications"}},
        {1250, new[] {"windows-1250", "ANSI Central European; Central European (Windows)"}},
        {1251, new[] {"windows-1251", "ANSI Cyrillic; Cyrillic (Windows)"}},
        {1252, new[] {"windows-1252", "ANSI Latin 1; Western European (Windows)"}},
        {1253, new[] {"windows-1253", "ANSI Greek; Greek (Windows)"}},
        {1254, new[] {"windows-1254", "ANSI Turkish; Turkish (Windows)"}},
        {1255, new[] {"windows-1255", "ANSI Hebrew; Hebrew (Windows)"}},
        {1256, new[] {"windows-1256", "ANSI Arabic; Arabic (Windows)"}},
        {1257, new[] {"windows-1257", "ANSI Baltic; Baltic (Windows)"}},
        {1258, new[] {"windows-1258", "ANSI/OEM Vietnamese; Vietnamese (Windows)"}},
        {1361, new[] {"Johab", "Korean (Johab)"}},
        {10000, new[] {"macintosh", "MAC Roman; Western European (Mac)"}},
        {10001, new[] {"x-mac-japanese", "Japanese (Mac)"}},
        {10002, new[] {"x-mac-chinesetrad", "MAC Traditional Chinese (Big5); Chinese Traditional (Mac)"}},
        {10003, new[] {"x-mac-korean", "Korean (Mac)"}},
        {10004, new[] {"x-mac-arabic", "Arabic (Mac)"}},
        {10005, new[] {"x-mac-hebrew", "Hebrew (Mac)"}},
        {10006, new[] {"x-mac-greek", "Greek (Mac)"}},
        {10007, new[] {"x-mac-cyrillic", "Cyrillic (Mac)"}},
        {10008, new[] {"x-mac-chinesesimp", "MAC Simplified Chinese (GB 2312); Chinese Simplified (Mac)"}},
        {10010, new[] {"x-mac-romanian", "Romanian (Mac)"}},
        {10017, new[] {"x-mac-ukrainian", "Ukrainian (Mac)"}},
        {10021, new[] {"x-mac-thai", "Thai (Mac)"}},
        {10029, new[] {"x-mac-ce", "MAC Latin 2; Central European (Mac)"}},
        {10079, new[] {"x-mac-icelandic", "Icelandic (Mac)"}},
        {10081, new[] {"x-mac-turkish", "Turkish (Mac)"}},
        {10082, new[] {"x-mac-croatian", "Croatian (Mac)"}},
        {12000, new[] {"utf-32", "Unicode UTF-32, little endian byte order; available only to managed applications"}},
        {12001, new[] {"utf-32BE", "Unicode UTF-32, big endian byte order; available only to managed applications"}},
        {20000, new[] {"x-Chinese_CNS", "CNS Taiwan; Chinese Traditional (CNS)"}},
        {20001, new[] {"x-cp20001", "TCA Taiwan"}},
        {20002, new[] {"x_Chinese-Eten", "Eten Taiwan; Chinese Traditional (Eten)"}},
        {20003, new[] {"x-cp20003", "IBM5550 Taiwan"}},
        {20004, new[] {"x-cp20004", "TeleText Taiwan"}},
        {20005, new[] {"x-cp20005", "Wang Taiwan"}},
        {20105, new[] {"x-IA5", "IA5 (IRV International Alphabet No. 5, 7-bit); Western European (IA5)"}},
        {20106, new[] {"x-IA5-German", "IA5 German (7-bit)"}},
        {20107, new[] {"x-IA5-Swedish", "IA5 Swedish (7-bit)"}},
        {20108, new[] {"x-IA5-Norwegian", "IA5 Norwegian (7-bit)"}},
        {20127, new[] {"us-ascii", "US-ASCII (7-bit)"}},
        {20261, new[] {"x-cp20261", "T.61"}},
        {20269, new[] {"x-cp20269", "ISO 6937 Non-Spacing Accent"}},
        {20273, new[] {"IBM273", "IBM EBCDIC Germany"}},
        {20277, new[] {"IBM277", "IBM EBCDIC Denmark-Norway"}},
        {20278, new[] {"IBM278", "IBM EBCDIC Finland-Sweden"}},
        {20280, new[] {"IBM280", "IBM EBCDIC Italy"}},
        {20284, new[] {"IBM284", "IBM EBCDIC Latin America-Spain"}},
        {20285, new[] {"IBM285", "IBM EBCDIC United Kingdom"}},
        {20290, new[] {"IBM290", "IBM EBCDIC Japanese Katakana Extended"}},
        {20297, new[] {"IBM297", "IBM EBCDIC France"}},
        {20420, new[] {"IBM420", "IBM EBCDIC Arabic"}},
        {20423, new[] {"IBM423", "IBM EBCDIC Greek"}},
        {20424, new[] {"IBM424", "IBM EBCDIC Hebrew"}},
        {20833, new[] {"x-EBCDIC-KoreanExtended", "IBM EBCDIC Korean Extended"}},
        {20838, new[] {"IBM-Thai", "IBM EBCDIC Thai"}},
        {20866, new[] {"koi8-r", "Russian (KOI8-R); Cyrillic (KOI8-R)"}},
        {20871, new[] {"IBM871", "IBM EBCDIC Icelandic"}},
        {20880, new[] {"IBM880", "IBM EBCDIC Cyrillic Russian"}},
        {20905, new[] {"IBM905", "IBM EBCDIC Turkish"}},
        {20924, new[] {"IBM00924", "IBM EBCDIC Latin 1/Open System (1047 + Euro symbol)"}},
        {20932, new[] {"EUC-JP", "Japanese (JIS 0208-1990 and 0212-1990)"}},
        {20936, new[] {"x-cp20936", "Simplified Chinese (GB2312); Chinese Simplified (GB2312-80)"}},
        {20949, new[] {"x-cp20949", "Korean Wansung"}},
        {21025, new[] {"cp1025", "IBM EBCDIC Cyrillic Serbian-Bulgarian"}},
        {21027, new[] {"", "(deprecated)"}},
        {21866, new[] {"koi8-u", "Ukrainian (KOI8-U); Cyrillic (KOI8-U)"}},
        {28591, new[] {"iso-8859-1", "ISO 8859-1 Latin 1; Western European (ISO)"}},
        {28592, new[] {"iso-8859-2", "ISO 8859-2 Central European; Central European (ISO)"}},
        {28593, new[] {"iso-8859-3", "ISO 8859-3 Latin 3"}},
        {28594, new[] {"iso-8859-4", "ISO 8859-4 Baltic"}},
        {28595, new[] {"iso-8859-5", "ISO 8859-5 Cyrillic"}},
        {28596, new[] {"iso-8859-6", "ISO 8859-6 Arabic"}},
        {28597, new[] {"iso-8859-7", "ISO 8859-7 Greek"}},
        {28598, new[] {"iso-8859-8", "ISO 8859-8 Hebrew; Hebrew (ISO-Visual)"}},
        {28599, new[] {"iso-8859-9", "ISO 8859-9 Turkish"}},
        {28603, new[] {"iso-8859-13", "ISO 8859-13 Estonian"}},
        {28605, new[] {"iso-8859-15", "ISO 8859-15 Latin 9"}},
        {29001, new[] {"x-Europa", "Europa 3"}},
        {38598, new[] {"iso-8859-8-i", "ISO 8859-8 Hebrew; Hebrew (ISO-Logical)"}},
        {50220, new[] {"iso-2022-jp", "ISO 2022 Japanese with no halfwidth Katakana; Japanese (JIS)"}},
        {50221, new[] {"csISO2022JP", "ISO 2022 Japanese with halfwidth Katakana; Japanese (JIS-Allow 1 byte Kana)"}},
        {50222, new[] {"iso-2022-jp", "ISO 2022 Japanese JIS X 0201-1989; Japanese (JIS-Allow 1 byte Kana - SO/SI)"}},
        {50225, new[] {"iso-2022-kr", "ISO 2022 Korean"}},
        {50227, new[] {"x-cp50227", "ISO 2022 Simplified Chinese; Chinese Simplified (ISO 2022)"}},
        {50229, new[] {"", "ISO 2022 Traditional Chinese"}},
        {50930, new[] {"", "EBCDIC Japanese (Katakana) Extended"}},
        {50931, new[] {"", "EBCDIC US-Canada and Japanese"}},
        {50933, new[] {"", "EBCDIC Korean Extended and Korean"}},
        {50935, new[] {"", "EBCDIC Simplified Chinese Extended and Simplified Chinese"}},
        {50936, new[] {"", "EBCDIC Simplified Chinese"}},
        {50937, new[] {"", "EBCDIC US-Canada and Traditional Chinese"}},
        {50939, new[] {"", "EBCDIC Japanese (Latin) Extended and Japanese"}},
        {51932, new[] {"euc-jp", "EUC Japanese"}},
        {51936, new[] {"EUC-CN", "EUC Simplified Chinese; Chinese Simplified (EUC)"}},
        {51949, new[] {"euc-kr", "EUC Korean"}},
        {51950, new[] {"", "EUC Traditional Chinese"}},
        {52936, new[] {"hz-gb-2312", "HZ-GB2312 Simplified Chinese; Chinese Simplified (HZ)"}},
        {54936, new[] {"GB18030", "Windows XP and later: GB18030 Simplified Chinese (4 byte); Chinese Simplified (GB18030)"}},
        {57002, new[] {"x-iscii-de", "ISCII Devanagari"}},
        {57003, new[] {"x-iscii-be", "ISCII Bangla"}},
        {57004, new[] {"x-iscii-ta", "ISCII Tamil"}},
        {57005, new[] {"x-iscii-te", "ISCII Telugu"}},
        {57006, new[] {"x-iscii-as", "ISCII Assamese"}},
        {57007, new[] {"x-iscii-or", "ISCII Odia"}},
        {57008, new[] {"x-iscii-ka", "ISCII Kannada"}},
        {57009, new[] {"x-iscii-ma", "ISCII Malayalam"}},
        {57010, new[] {"x-iscii-gu", "ISCII Gujarati"}},
        {57011, new[] {"x-iscii-pa", "ISCII Punjabi"}},
        {65000, new[] {"utf-7", "Unicode (UTF-7)"}},
        {65001, new[] {"utf-8", "Unicode (UTF-8)"}}
    };

        #endregion Code Page Identifiers

        private static bool HasConsole => GetConsoleWindow() != IntPtr.Zero;

        private delegate bool ConsoleCtrlDelegate(int dwCtrlType); //定义处理程序委托

        #region Method

        /// <summary>
        ///     禁用关闭按钮
        /// </summary>
        private static void CloseButtonRemove()
        {
            var windowHandler = FindWindow(null, Process.GetCurrentProcess().MainModule.FileName); //与控制台标题名一样的路径,根据控制台标题找控制台
            var closeMenu = GetSystemMenu((IntPtr)windowHandler, IntPtr.Zero); //找关闭按钮
            var scClose = 0xF060;
            RemoveMenu(closeMenu, scClose, 0x0); //关闭按钮禁用
        }

        private static void InvalidateOutAndError()
        {
            var type = typeof(Console);
            var _out = type.GetField("_out", BindingFlags.Static | BindingFlags.NonPublic);
            var error = type.GetField("_error", BindingFlags.Static | BindingFlags.NonPublic);
            var initializeStdOutError = type.GetMethod("InitializeStdOutError", BindingFlags.Static | BindingFlags.NonPublic);
            Debug.Assert(_out != null);
            Debug.Assert(error != null);
            Debug.Assert(initializeStdOutError != null);
            _out.SetValue(null, null);
            error.SetValue(null, null);
            initializeStdOutError.Invoke(null, new object[] { true });
        }

        private static void SetOutAndErrorNull()
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
        }

        //当关闭Console时，系统会发送下面的消息
        private const int CtrlCEvent = 0; //无论是从键盘输入或由GenerateConsoleCtrlEvent功能信号产生的一个CTRL + C接收信号

        private const int CtrlBreakEvent = 1; //无论是从键盘输入或由GenerateConsoleCtrlEvent信号产生的一个CTRL + BREAK信号接收。
        private const int CtrlCloseEvent = 2; //信号系统，当用户关闭控制台（通过单击控制台窗口菜单上的关闭按钮，或通过从任务管理器结束任务）
        private const int CtrlLogoffEvent = 5; //用户注销时系统发送到所有控制台进程的信号。此信号不指示哪个用户正在注销，因此不能进行任何假设。请注意，此信号仅由服务接收。交互式应用程序在注销时终止，因此当系统发送此信号时，它们不存在。
        private const int CtrlShutdownEvent = 6; //系统关闭时系统发送的信号。在系统发送此信号时，交互式应用程序不存在，因此在这种情况下它只能被服务接收。服务还有自己的关闭事件的通知机制。这个信号还可以通过使用应用程序生成的GenerateConsoleCtrlEvent。

        /// <summary>
        ///     处理程序例程，在这里编写对指定事件的处理程序代码
        ///     注意：在VS中调试执行时，在这里设置断点，但不会中断；会提示：无可用源；
        /// </summary>
        /// <param name="ctrlType"></param>
        /// <returns></returns>
        private static bool HandlerRoutine(int ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlCEvent:
                    OnCtrlCPressed(null, null);
                    Console.WriteLine("Ctrl+C按下，阻止");
                    return true; //这里返回true，表示阻止响应系统对该程序的操作成功
                case CtrlBreakEvent:
                    Console.WriteLine("Ctrl+BREAK按下，阻止");
                    return true;

                case CtrlCloseEvent:
                    Console.WriteLine("CLOSE");
                    break;

                case CtrlLogoffEvent:
                    Console.WriteLine("LOGOFF");
                    break;

                case CtrlShutdownEvent:
                    Console.WriteLine("SHUTDOWN");
                    break;
            }
            return true; //true 表示阻止响应系统对该程序的操作 //false 忽略处理，让系统进行默认操作
        }

        #endregion Method

        #region 导入API函数

        /// <summary>
        ///     添加或删除从调用进程处理函数列表中的应用definedhandlerroutinefunction。如果没有指定的事件处理函数，函数集的可继承的属性，确定是否调用过程忽略了Ctrl + C信号。
        /// </summary>
        /// <param name="handlerRoutine">指向要添加或删除的程序定义HandlerRoutine函数的指针。 此参数可以为NULL。</param>
        /// <param name="add">
        ///     如果这个参数为TRUE，处理程序被添加; 如果是FALSE，则处理程序被删除。如果HandlerRoutine参数为NULL，一个TRUE值会导致调用进程忽略CTRL +
        ///     C输入，以及一个FALSE值恢复CTRL + C输入的正常处理。忽略或处理CTRL + C的此属性由子进程继承。
        /// </param>
        /// <returns>如果函数成功，返回值为非零。如果函数失败，返回值为零。</returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handlerRoutine, bool add);

        /// <summary>
        ///     为当前进程分配一个新控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        /// <summary>
        ///     使调用进程从其控制台分离
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        /// <summary>
        ///     检索与调用进程相关联的控制台窗口句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        /// <summary>
        ///     检取与调用进程有关的控制台所用的输出代码页的等价内容，以便将输出函数所写入的内容转换成显示图象
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern int GetConsoleOutputCP();

        /// <summary>
        ///     获得一个顶层窗口的句柄，该窗口的类名和窗口名与给定的字符串相匹配。这个函数不查找子窗口。
        /// </summary>
        /// <param name="lpClassName">
        ///     指向一个指定了类名的空结束字符串，或一个标识类名字符串的成员的指针。如果该参数为一个成员，则它必须为前次调用theGlobafAddAtom函数产生的全局成员。该成员为16位，必须位于IpClassName的低
        ///     16位，高位必须为 0。
        /// </param>
        /// <param name="lpWindowName">指向一个指定了窗口名（窗口标题）的空结束字符串。如果该参数为空，则为所有窗口全匹配。</param>
        /// <returns>返回值：如果函数成功，返回值为具有指定类名和窗口名的窗口句柄；如果函数失败，返回值为NULL。</returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        ///     该函数允许应用程序为复制或修改而访问窗口菜单（系统菜单或控制菜单）。
        ///     任何没有用GetSystemMenu函数来生成自己的窗口菜单拷贝的窗口将接受标准窗口菜单。
        ///     窗口菜单最初包含的菜单项有多种标识符值，如SC_CLOSE，SC_MOVE和SC_SIZE。
        ///     窗口菜单上的菜单项发送WM_SYSCOMMAND消息。
        /// </summary>
        /// <param name="hWnd">拥有窗口菜单拷贝的窗口的句柄。</param>
        /// <param name="bRevert">指定将执行的操作。如果此参数为FALSE，GetSystemMenu返回当前使用窗口菜单的拷贝的句柄。该拷贝初始时与窗口菜单相同，但可以被修改。如果此参数为TRUE，GetSystemMenu重置窗口菜单到缺省状态。如果存在先前的窗口菜单，将被销毁。</param>
        /// <returns>如果参数bRevert为FALSE，返回值是窗口菜单的拷贝的句柄：如果参数bRevert为TRUE，返回值是NULL。</returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        /// <summary>
        ///     删除指定的菜单项或弹出式菜单
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="nPos"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        #endregion 导入API函数

        #region Event

        #region CtrlCPressed

        public delegate void CtrlCPressedHandler(object sender, ConsoleCancelEventArgs e);

        public static event CtrlCPressedHandler CtrlCPressed;

        private static void OnCtrlCPressed(object sender, ConsoleCancelEventArgs e)
        {
            CtrlCPressed?.Invoke(sender, e);
        }

        #endregion CtrlCPressed

        #endregion Event

        #region 对外开放的方法

        /// Creates a new console instance if the process is not attached to a console already.
        public static void Show()
        {
            //#if DEBUG
            try
            {
                if (!HasConsole)
                {
                    AllocConsole();
                    InvalidateOutAndError();
                    //Console.CancelKeyPress += OnCtrlCPressed;
                    //if (SetConsoleCtrlHandler(ConsoleCtrlDelegateHandlerRoutine, true))
                    //    Console.Write("成功阻止窗口关闭-");
                    CloseButtonRemove();
                    var getConsoleOutputCpInfo = GetConsoleOutputCP();
                    if (DicCodePageIdentifiers.ContainsKey(getConsoleOutputCpInfo))
                        Console.WriteLine($"当前窗口信息：{DicCodePageIdentifiers[getConsoleOutputCpInfo][0]},[{DicCodePageIdentifiers[getConsoleOutputCpInfo][1]}]");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            //#endif
        }

        /// If the process has a console attached to it, it will be detached and no longer visible. Writing to the System.Console is still possible, but no output will be shown.
        public static void Hide()
        {
            //#if DEBUG
            try
            {
                if (HasConsole)
                {
                    SetOutAndErrorNull();
                    FreeConsole();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            //#endif
        }

        /// <summary>
        ///     切换触发
        /// </summary>
        public static void Toggle()
        {
            try
            {
                if (HasConsole)
                    Hide();
                else
                    Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion 对外开放的方法
    }
}
