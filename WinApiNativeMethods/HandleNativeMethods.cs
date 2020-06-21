using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace mylib.WinApi
{
    internal partial class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern public bool CloseHandle(IntPtr handle);
    }
}
