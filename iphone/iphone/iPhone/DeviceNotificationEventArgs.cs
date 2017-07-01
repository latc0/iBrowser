using System;

namespace iphone
{
	/// <summary>
	/// Provides data for the <see>DfuConnect</see>, <see>DfuDisconnect</see>, <see>RecoveryModeEnter</see> and <see>RecoveryModeLeave</see> events.
	/// </summary>
	public class DeviceNotificationEventArgs : EventArgs {
		private AMRecoveryDevice	device;

		internal DeviceNotificationEventArgs(AMRecoveryDevice device) {
			this.device = device;
		}

		internal AMRecoveryDevice Device {
			get {
				return device;
			}
		}
	}
}
