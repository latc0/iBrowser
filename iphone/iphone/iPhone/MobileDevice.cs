using System;
using System.Runtime.InteropServices;
using System.Text;

namespace iphone
{
    internal class MobileDevice
    {
        const string DLLName = "iTunesMobileDevice.dll";

        static MobileDevice()
        {
			const string dllpath = @"C:\Program Files\Common Files\Apple\Mobile Device Support\";
			const string apppath = @"C:\Program Files\Common Files\Apple\Apple Application Support\";

			Environment.SetEnvironmentVariable("Path", string.Join(";", 
				new String[] { 
				Environment.GetEnvironmentVariable("Path"), 
				dllpath,
				apppath
			}));		
		}

        public static IntPtr StringToCFString(string value)
        {
            IntPtr num = IntPtr.Zero;
            try
            {
                byte[] numArray = new byte[value.Length + 1];
                Encoding.ASCII.GetBytes(value, 0, value.Length, numArray, 0);
                return __CFStringMakeConstantString(numArray);
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }

        }

        public static byte[] StringToCString(string value)
        {
            byte[] bytes = new byte[value.Length + 1];
            Encoding.ASCII.GetBytes(value, 0, value.Length, bytes, 0);
            return bytes;
        }

        public static IntPtr CFStringMakeConstantString(string s)
        {
            return __CFStringMakeConstantString(StringToCString(s));
        }

        public static int AMDeviceStartService(IntPtr device, string service_name, ref IntPtr serviceHandle, IntPtr unknown)
        {
            return AMDeviceStartService(device, StringToCFString(service_name), ref serviceHandle, unknown);
        }

        public static string AMDeviceCopyValue(IntPtr device, uint unknown, string name)
        {
            IntPtr ptr = IntPtr.Zero;
            IntPtr cfstring = StringToCFString(name);
            try
            {
                if (cfstring != IntPtr.Zero)
                    ptr = AMDeviceCopyValue_Int(device, unknown, cfstring);
                if (ptr != IntPtr.Zero)
                {
                    byte num1 = Marshal.ReadByte(ptr, 16);
                    byte num2 = Marshal.ReadByte(ptr, 17);
                    byte num3 = Marshal.ReadByte(ptr, 18);
                    return (int)num1 <= 0 || (int)num2 != 0 || (int)num3 != 0 ? ((int)num1 <= 0 ? "" : Marshal.PtrToStringAnsi(new IntPtr(ptr.ToInt64() + 17L), (int)num1)) : Marshal.PtrToStringAuto(new IntPtr(ptr.ToInt64() + 24L), (int)num1).Trim();
                }
            }
            catch (Exception)
            {
                ////LogMessager.Write(string.Format("StackTrace:{0}---Message:{1}", (object)ex.StackTrace, (object)ex.Message), //LogMessager.LogMessageType.Error);
            }
            return string.Empty;
        }

        public static string AMDeviceCopyValue(IntPtr device, string service, string name)
        {
            long returnvalue = 0L;
            try
            {
                IntPtr servicecfstr = StringToCFString(service);
                IntPtr cfstring1 = StringToCFString(name);
                IntPtr cfstring2 = !service.Equals("") ? AMDeviceCopyValue(device, servicecfstr, cfstring1) : AMDeviceCopyValue_Int(device, 0U, cfstring1);
                if (cfstring2 != IntPtr.Zero)
                    CFNumberGetValue(cfstring2, CFNumberType.kCFNumberLongLongType, ref returnvalue);
            }
            catch (Exception)
            {
                ////LogMessager.Write(string.Format("StackTrace:{0}---Message:{1}", (object)ex.StackTrace, (object)ex.Message), //LogMessager.LogMessageType.Error);
            }
            return returnvalue.ToString();
        }

        public static int AFCDirectoryOpen(IntPtr conn, string path, ref IntPtr dir)
        {
            try
            {
                int ret = AFCDirectoryOpen(conn, Encoding.UTF8.GetBytes(path), ref dir);
                return ret;
            }
            catch (AccessViolationException)
            {
                return 0;
            }

        }

        public static int AFCDirectoryRead(IntPtr conn, IntPtr dir, ref string buffer)
        {
            int ret;

            IntPtr ptr = IntPtr.Zero;
            ret = AFCDirectoryRead(conn, dir, ref ptr);
            if ((ret == 0) && (ptr != null))
            {
                buffer = Marshal.PtrToStringAnsi(ptr);
            }
            else
            {
                buffer = null;
            }
            return ret;
        }


        public enum CFNumberType
        {
            kCFNumberLongLongType = 11,
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr __CFStringMakeConstantString(byte[] s);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CFNumberGetValue(IntPtr cfstring, CFNumberType theType, ref long returnvalue);


        //AMDevice
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDevicePair(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceNotificationSubscribe(DeviceNotificationCallback callback, uint unused1, uint unused2, uint unused3, out IntPtr am_device_notification_ptr);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceConnect(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceDisconnect(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceIsPaired(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceValidatePairing(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceStartSession(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceStopSession(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceRelease(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AMDeviceStartService(IntPtr device, IntPtr service_name, ref IntPtr handle, IntPtr unknown);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        extern private static IntPtr AMDeviceCopyValue(IntPtr device, IntPtr servicecfstr, IntPtr cfstring);

        [DllImport(DLLName, EntryPoint = "AMDeviceCopyValue", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr AMDeviceCopyValue_Int(IntPtr device, uint unknown, IntPtr cfstring);


        //AFC Connection
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCConnectionClose(IntPtr afcHandle);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCConnectionOpen(IntPtr handle, uint io_timeout, ref IntPtr conn);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCConnectionIsValid(IntPtr conn);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCConnectionInvalidate(IntPtr conn);


        //AFC Directory
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCDirectoryOpen(IntPtr conn, byte[] path, ref IntPtr dir);     

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCDirectoryRead(IntPtr conn, IntPtr dir, ref IntPtr dirent);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCDirectoryClose(IntPtr conn, IntPtr dir);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCDirectoryCreate(IntPtr conn, string path);


        //AFC File
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileInfoOpen(IntPtr conn, byte[] path, out IntPtr handle);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileRefOpen(IntPtr conn, byte[] path, int mode, out long handle);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileRefClose(IntPtr conn, long handle);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileRefRead(IntPtr conn, long handle, byte[] buffer, ref long len);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileRefWrite(IntPtr conn, long handle, byte[] buffer, uint len);

        // FIXME - not working, arguments? Always returns 7
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		public extern static int AFCFileRefSeek(IntPtr conn, long handle, long pos, long origin);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileRefTell(IntPtr conn, Int64 handle, ref uint position);

        // FIXME - not working, arguments?
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileRefSetFileSize(IntPtr conn, Int64 handle, uint size);


        //AFC Other
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCDeviceInfoOpen(IntPtr conn, ref IntPtr buffer);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCKeyValueRead(IntPtr dict, out IntPtr key, out IntPtr val);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCKeyValueClose(IntPtr dict);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCRemovePath(IntPtr conn, string path);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCRenamePath(IntPtr conn, string old_path, string new_path);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFlushData(IntPtr conn, Int64 handle);
    }
}