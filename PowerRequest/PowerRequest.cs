using System;
using System.Collections.Generic;
using System.Text;

namespace PowerRequest
{
    /// <summary>
    ///  PowerRequest.
    /// </summary>
    internal class PowerRequestWrapper : IDisposable
    {
        /// <summary>
        ///  Constructor, PowerCreateRequest().
        /// </summary>
        /// <param name="reason"></param>
        /// <exception cref="System.ComponentModel.Win32Exception">PowerCreateRequest failed.</exception>
        public PowerRequestWrapper(string reason)
        {
            var context = new mylib.WinApi.NativeMethods.REASON_CONTEXT_Simple(reason);
            _powerRequest = mylib.WinApi.NativeMethods.PowerCreateRequest(ref context);
            if (_powerRequest == null)
            {
                throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        ///  PowerSetRequest().
        /// </summary>
        public bool Set(PowerRequestType requestType)
        {
            if (_powerRequest.IsClosed)
            {
                throw new ObjectDisposedException("_powerrequest");
            }

            return mylib.WinApi.NativeMethods.PowerSetRequest(_powerRequest, ToNativeRequestType(requestType));
        }

        /// <summary>
        ///  PowerClearRequest().
        /// </summary>
        public bool Clear(PowerRequestType requestType)
        {
            if (_powerRequest.IsClosed)
            {
                throw new ObjectDisposedException("_powerrequest");
            }

            return mylib.WinApi.NativeMethods.PowerClearRequest(_powerRequest, ToNativeRequestType(requestType));
        }


        /// <summary>
        ///  PowerRequest handle.
        /// </summary>
        private mylib.WinApi.PowerRequestHandle _powerRequest;

        /// <summary>
        ///  convert PowerRequestType to Win32 API POWER_REQUEST_TYPE.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private static mylib.WinApi.NativeMethods.POWER_REQUEST_TYPE ToNativeRequestType(PowerRequestType requestType)
        {
            return (mylib.WinApi.NativeMethods.POWER_REQUEST_TYPE)(UInt32)requestType;
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _powerRequest.Dispose();
            }
        }
        #endregion
    }

    /// <summary>
    ///  Power Request Type.
    /// </summary>
    /// <seealso cref="NativeMethods.POWER_REQUEST_TYPE"/>
    public enum PowerRequestType
    {
        DisplayRequired,
        SystemRequired,
        AwayModeRequired,
        ExecutionRequired
    }
}
