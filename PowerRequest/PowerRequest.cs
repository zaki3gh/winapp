using System;
using System.Collections.Generic;
using System.Text;

namespace PowerRequest
{
    /// <summary>
    ///  PowerRequest.
    /// </summary>
    class PowerRequest1 : IDisposable
    {
        /// <summary>
        ///  Constructor, PowerCreateRequest().
        /// </summary>
        /// <param name="reason"></param>
        public PowerRequest1(string reason)
        {
            var context = new NativeMethods.REASON_CONTEXT_Simple(reason);
            this.powerRequest = NativeMethods.PowerCreateRequest(ref context);
        }

        /// <summary>
        ///  PowerSetRequest().
        /// </summary>
        public void Set()
        {
            if (this.powerRequest == null)
            {
                throw new InvalidOperationException();
            }

            if (!NativeMethods.PowerSetRequest(this.powerRequest, ToNativeRequestType(RequestType)))
            {

            }
        }

        /// <summary>
        ///  PowerClearRequest().
        /// </summary>
        public void Clear()
        {
            if (this.powerRequest == null)
            {
                throw new InvalidOperationException();
            }

            if (!NativeMethods.PowerClearRequest(this.powerRequest, ToNativeRequestType(RequestType)))
            {

            }
        }

        /// <summary>
        ///  Whether Set() or Clear() can work.
        /// </summary>
        public bool IsValid
        {
            get => this.powerRequest != null;
        }

        /// <summary>
        ///  PowerRequestType to be set;
        /// </summary>
        public PowerRequestType RequestType
        {
            get;
            set;
        }

        /// <summary>
        ///  PowerRequest handle.
        /// </summary>
        private PowerRequestHandle powerRequest;

        /// <summary>
        ///  convert PowerRequestType to Win32 API POWER_REQUEST_TYPE.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private NativeMethods.POWER_REQUEST_TYPE ToNativeRequestType(PowerRequestType requestType)
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

    public enum PowerRequestType
    {
        PowerRequestDisplayRequired,
        PowerRequestSystemRequired,
        PowerRequestAwayModeRequired,
        PowerRequestExecutionRequired
    }
}
