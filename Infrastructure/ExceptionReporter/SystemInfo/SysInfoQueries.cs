namespace Infrastructure.ExceptionReporter.SystemInfo
{
    /// <summary>
    /// Class SysInfoQueries.
    /// </summary>
    public static class SysInfoQueries
    {
        /// <summary>
        /// The operating system
        /// </summary>
        public static readonly SysInfoQuery OperatingSystem = new SysInfoQuery("Operating System", "Win32_OperatingSystem", false);
        /// <summary>
        /// The machine
        /// </summary>
        public static readonly SysInfoQuery Machine = new SysInfoQuery("Machine", "Win32_ComputerSystem", true);
    }
}