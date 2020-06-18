using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace zakilib.WinApiNativeMethods
{
    /// <summary>
    ///  Window関連のP/Invoke.
    /// </summary>
    internal partial class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr GetShellWindow();

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);
    }

    /// <summary>
    ///  Window message id.
    /// </summary>
    internal static class WindowMessage
    {
        public static uint WM_SYSCOMMAND = 0x0112;
    }

    /// <summary>
    ///  WM_SYSCOMMAND's wParam and lParam.
    /// </summary>
    internal static class SysCommandParams
    {
        public static UIntPtr SC_MONITORPOWER = (UIntPtr)0xF170u;
        public static IntPtr PoweringOn = (IntPtr)(-1);
        public static IntPtr GoingToLowPower = (IntPtr)1;
        public static IntPtr BeingShutOff = (IntPtr)2;
    }
}
