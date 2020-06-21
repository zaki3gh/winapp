using System;
using System.Collections.Generic;
using System.Text;

namespace PowerRequest
{
    class MonitorPower
    {
        /// <summary>
        ///  turn off display.
        /// </summary>
        /// <returns></returns>
        public static bool TurnOffDisplay()
        {
            var shellWnd = mylib.WinApi.NativeMethods.GetShellWindow();
            if (shellWnd == IntPtr.Zero)
            {
                return false;
            }
            if (!mylib.WinApi.NativeMethods.PostMessage(
                shellWnd,
                mylib.WinApi.WindowMessage.WM_SYSCOMMAND,
                mylib.WinApi.SysCommandParams.SC_MONITORPOWER,
                mylib.WinApi.SysCommandParams.GoingToLowPower))
            {
                return false;
            }
            if (!mylib.WinApi.NativeMethods.PostMessage(
                shellWnd,
                mylib.WinApi.WindowMessage.WM_SYSCOMMAND,
                mylib.WinApi.SysCommandParams.SC_MONITORPOWER,
                mylib.WinApi.SysCommandParams.BeingShutOff))
            {
                return false;
            }
            return true;
        }
    }
}
