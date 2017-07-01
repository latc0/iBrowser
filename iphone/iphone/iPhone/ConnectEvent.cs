using System;
using System.Runtime.InteropServices;

namespace iphone
{
	/// <summary>
	/// Provides data for the <see>Connected</see> and <see>Disconnected</see> events.
	/// </summary>
	public class ConnectEventArgs : EventArgs{
		private NotificationMessage	message;
		private IntPtr device;
 
		internal ConnectEventArgs(AMDeviceNotificationCallbackInfo cbi) {
			message = cbi.msg;
            device = cbi.dev_ptr;
		}

		/// <summary>
		/// Returns the information for the device that was connected or disconnected.
		/// </summary>
		public IntPtr Device {
			get { return device; }
		}

		/// <summary>
		/// Returns the type of event.
		/// </summary>
		public NotificationMessage Message {
			get { return message; }
		}
	}

    /// <summary>
    /// Represents the method that will handle the <see>Connected</see> and <see>Disconnected</see> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">A <see>ConnectEventArgs</see> that contains the data.</param>
    public delegate void ConnectEventHandler(object sender, ConnectEventArgs args);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct AMDeviceNotificationCallbackInfo
    {
        public IntPtr dev
        {
            get
            {
                return dev_ptr;
            }
        }
        internal IntPtr dev_ptr;
        public NotificationMessage msg;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMDeviceNotification
    {
        private DeviceNotificationCallback callback;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceNotificationCallback(ref AMDeviceNotificationCallbackInfo callback_info);
}
