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
        public PowerRequestWrapper(PowerRequestType requestType, string reason)
        {
            this.RequestType = requestType;
            var context = new NativeMethods.REASON_CONTEXT_Simple(reason);
            this.powerRequest = NativeMethods.PowerCreateRequest(ref context);

        }

        /// <summary>
        ///  PowerSetRequest().
        /// </summary>
        public void Set()
        {
            if (!IsValid)
            {
                throw new InvalidOperationException();
            }

            IsSet = NativeMethods.PowerSetRequest(this.powerRequest, ToNativeRequestType(RequestType));
        }

        /// <summary>
        ///  PowerClearRequest().
        /// </summary>
        public void Clear()
        {
            if (!IsValid)
            {
                throw new InvalidOperationException();
            }
            if (!IsSet)
            {
                return;
            }

            IsSet = false;
            if (!NativeMethods.PowerClearRequest(this.powerRequest, ToNativeRequestType(RequestType)))
            {

            }
        }

        /// <summary>
        ///  PowerRequestType to be set;
        /// </summary>
        public PowerRequestType RequestType
        {
            get;
            private set;
        }

        /// <summary>
        ///  PowerRequest handle.
        /// </summary>
        private PowerRequestHandle powerRequest;

        /// <summary>
        ///  Whether Set() or Clear() can work.
        /// </summary>
        public bool IsValid
        {
            get => this.powerRequest != null;
        }

        /// <summary>
        ///  true if PowerSetRequest() is successfully called.
        /// </summary>
        public bool IsSet
        {
            get;
            private set;
        }

        /// <summary>
        ///  convert PowerRequestType to Win32 API POWER_REQUEST_TYPE.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private static NativeMethods.POWER_REQUEST_TYPE ToNativeRequestType(PowerRequestType requestType)
        {
            return (NativeMethods.POWER_REQUEST_TYPE)(UInt32)requestType;
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
                this.powerRequest?.Dispose();
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
