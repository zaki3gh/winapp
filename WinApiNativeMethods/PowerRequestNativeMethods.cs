using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Configuration;

namespace mylib.WinApi
{
    /// <summary>
    ///  P/Invoke definitions for PowerRequest.
    /// </summary>
    internal partial class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern public PowerRequestHandle PowerCreateRequest(ref REASON_CONTEXT_Simple Context);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern public bool PowerClearRequest(PowerRequestHandle PowerRequest, POWER_REQUEST_TYPE RequestType);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern public bool PowerSetRequest(PowerRequestHandle PowerRequest, POWER_REQUEST_TYPE RequestType);


        /// <summary>
        ///  REASON_CONTEXT structre, Flags=POWER_REQUEST_CONTEXT_SIMPLE_STRING.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct REASON_CONTEXT_Simple
        {
            UInt32 Version;
            UInt32 Flags;
            [MarshalAs(UnmanagedType.LPWStr)]
            String SimpleReasonString;

            public REASON_CONTEXT_Simple(String reason)
            {
                Version = POWER_REQUEST_CONTEXT_VERSION;
                Flags = POWER_REQUEST_CONTEXT_SIMPLE_STRING;
                SimpleReasonString = reason;
            }
        }

        static UInt32 POWER_REQUEST_CONTEXT_VERSION = 0;
        //static UInt32 POWER_REQUEST_CONTEXT_DETAILED_STRING = 0x2;
        static UInt32 POWER_REQUEST_CONTEXT_SIMPLE_STRING = 0x1;

        public enum POWER_REQUEST_TYPE : UInt32
        {
            PowerRequestDisplayRequired = 0,
            PowerRequestSystemRequired,
            PowerRequestAwayModeRequired,
            PowerRequestExecutionRequired
        }
    }

    /// <summary>
    ///  PowerRequest handle.
    /// </summary>
    internal class PowerRequestHandle : Microsoft.Win32.SafeHandles.SafeHandleMinusOneIsInvalid
    {
        /// <summary>
        ///  Constructor.
        /// </summary>
        public PowerRequestHandle() : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.CloseHandle(this.handle);
        }
    }
}
