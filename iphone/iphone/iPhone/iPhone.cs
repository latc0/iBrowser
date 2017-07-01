using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace iphone
{
    public class iPhone
    {
        private DeviceNotificationCallback dnc;
        private bool connected;
        private string current_directory, link;

        internal IntPtr iPhoneHandle;
        internal IntPtr hAFC;
        internal IntPtr hService;
        
        public event ConnectEventHandler Disconnect;
        public event ConnectEventHandler Connect;
       
        public iPhone(ConnectEventHandler myConnectHandler, ConnectEventHandler myDisconnectHandler)
        {
            Connect += myConnectHandler;
            Disconnect += myDisconnectHandler;
            if (!doConstruction())
                iBrowser.ErrorChange = 3;
        }
        
        public IntPtr AFCHandle
        {
            get
            {
                return hAFC;
            }
        }
        
        public Disk GetDiskStats()
        {
            Disk d = new Disk();
            if (MobileDevice.AMDeviceConnect(iPhoneHandle) == 0)
            {
                int ret = 0;
                if ((ret = MobileDevice.AMDeviceStartSession(iPhoneHandle)) != 1 || MobileDevice.AMDeviceStartSession(iPhoneHandle) == 0)
                {
                    if (ret == -402653155)
                    {
                        MobileDevice.AMDeviceStopSession(iPhoneHandle);
                        MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                        if (MobileDevice.AMDeviceConnect(iPhoneHandle) == 0 && (ret = MobileDevice.AMDeviceStartSession(iPhoneHandle)) == 0)
                        {
                        }
                    }
                    string tDataA = (MobileDevice.AMDeviceCopyValue(iPhoneHandle, "com.apple.disk_usage", "TotalDataAvailable"));
                    d.TotalUserAvailable = Math.Round(((double)(long.Parse(tDataA) - 230485760L) / 1024 / 1024 / 1024), 2) + " GB";

                    string tDataC = (MobileDevice.AMDeviceCopyValue(iPhoneHandle, "com.apple.disk_usage", "TotalDataCapacity"));
                    d.TotalUserCapacity = Math.Round(((double)(long.Parse(tDataC)) / 1024 / 1024 / 1024), 2) + " GB";

                    string tSysA = (MobileDevice.AMDeviceCopyValue(iPhoneHandle, "com.apple.disk_usage", "TotalSystemAvailable"));
                    d.TotalSystemAvailable = "Free space: " + Math.Round(((double)(long.Parse(tSysA)) / 1024 / 1024 / 1024), 2) + " GB";

                    string tSysC = (MobileDevice.AMDeviceCopyValue(iPhoneHandle, "com.apple.disk_usage", "TotalSystemCapacity"));
                    d.TotalSystemCapacity = Math.Round(((double)(long.Parse(tSysC)) / 1024 / 1024 / 1024), 2) + " GB";

                    string mu = (MobileDevice.AMDeviceCopyValue(iPhoneHandle, "com.apple.disk_usage", "MobileApplicationUsage"));
                    d.AppUsage = "App usage: " + Math.Round(((double)(long.Parse(mu)) / 1024 / 1024 / 1024), 2) + " GB";

                    string tDisk = MobileDevice.AMDeviceCopyValue(iPhoneHandle, "com.apple.disk_usage", "TotalDiskCapacity");
                    d.TotalDiskCapacity = "Total disk capacity: " + Math.Round(((double)(long.Parse(tDisk)) / 1024 / 1024 / 1024), 2) + " GB";
                }
                MobileDevice.AMDeviceStopSession(iPhoneHandle);
            }
            MobileDevice.AMDeviceDisconnect(iPhoneHandle);//-402653155
            return d;
        }

        public DateTime GetCreationTime(string path)
        {
            long dateBirth = long.Parse(GetFileInfo(path)["st_birthtime"]) / 1000000000;
            DateTime dateStr = UnixTimeStampToDateTime(dateBirth);
            return dateStr;
        }
        public DateTime GetModifiedTime(string path)
        {
            long dateMod = long.Parse(GetFileInfo(path)["st_mtime"]) / 1000000000;
            DateTime dateStr = UnixTimeStampToDateTime(dateMod);
            return dateStr;
        } 

        protected void OnConnect(ConnectEventArgs args)
        {
            ConnectEventHandler handler = Connect;
            if (handler != null)
            {
                handler(this, args);
            }
        }
        protected void OnDisconnect(ConnectEventArgs args)
        {
            ConnectEventHandler handler = Disconnect;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public ulong FileSize(string path)
        {
            bool is_dir;
            ulong size;

            GetFileInfo(path, out size, out is_dir);
            return size;
        }

        public string[] GetFiles(string path)
        {
            string full_path = FullPath(CurrentDirectory, path);

            IntPtr hAFCDir = IntPtr.Zero;
            if (MobileDevice.AFCDirectoryOpen(hAFC, full_path, ref hAFCDir) != 0)
            { }
            string buffer = null;
            ArrayList paths = new ArrayList();
            MobileDevice.AFCDirectoryRead(hAFC, hAFCDir, ref buffer);

            while (buffer != null)
            {
                if (!IsDirectory(FullPath(full_path, buffer))) paths.Add(buffer);
                MobileDevice.AFCDirectoryRead(hAFC, hAFCDir, ref buffer);
            }
            MobileDevice.AFCDirectoryClose(hAFC, hAFCDir);
            return (string[])paths.ToArray(typeof(string));
        }
        public string[] GetDirectories(string path)
        {
            IntPtr hAFCDir = IntPtr.Zero;
            string full_path = FullPath(CurrentDirectory, path);

            MobileDevice.AFCDirectoryOpen(hAFC, full_path, ref hAFCDir);
            string buffer = null;
            ArrayList paths = new ArrayList();
            MobileDevice.AFCDirectoryRead(hAFC, hAFCDir, ref buffer);

            while (buffer != null)
            {
                if ((buffer != ".") && (buffer != "..") && IsDirectory(FullPath(full_path, buffer)))
                    paths.Add(buffer);
                MobileDevice.AFCDirectoryRead(hAFC, hAFCDir, ref buffer);
            }
            MobileDevice.AFCDirectoryClose(hAFC, hAFCDir);
            return (string[])paths.ToArray(typeof(string));
        }
        public string LinkTarget
        {
            get { return link; }
            set { link = value; }
        }
        public string CurrentDirectory
        {
            get
            {
                return current_directory;
            }

            set
            {
                string new_path = FullPath(current_directory, value);
                if (!IsDirectory(new_path))
                {
                    throw new Exception("Invalid directory specified");
                }
                current_directory = new_path;
            }
        }
        public string GetDetailedInfo()
        {
            string s = "";

            string[] values = {
                "ActivationState",
                "BasebandBootloaderVersion",
                "BasebandVersion",
                "BluetoothAddress",
                "BuildVersion",
                "DeviceClass",
                "DeviceName",
                "DeviceColor",
                "FirmwareVersion",
                "HardwareModel",
                "ModelNumber",
                "PhoneNumber",
                "ProductType",
                "ProductVersion",
                "SerialNumber",
                "UniqueDeviceID",
                "WiFiAddress"
            };

            if (MobileDevice.AMDeviceConnect(iPhoneHandle) == 0)
            {
                if (MobileDevice.AMDeviceStartSession(iPhoneHandle) == 0)
                {
                    foreach (string val in values)
                    {
                        string value = MobileDevice.AMDeviceCopyValue(iPhoneHandle, 0U, val);
                        if (value != string.Empty)
                            s += val + ": " + value + "\n";
                    }
                }
                MobileDevice.AMDeviceStopSession(iPhoneHandle);
            }
            MobileDevice.AMDeviceDisconnect(iPhoneHandle);
            return s;
        }
        public int GetBatteryPercentage()
        {
            if (MobileDevice.AMDeviceConnect(iPhoneHandle) == 0)
            {
                if (MobileDevice.AMDeviceStartSession(iPhoneHandle) == 0)
                {
                    return int.Parse(MobileDevice.AMDeviceCopyValue(iPhoneHandle, "com.apple.mobile.battery", "BatteryCurrentCapacity"));
                }
                MobileDevice.AMDeviceStopSession(iPhoneHandle);
            }
            MobileDevice.AMDeviceDisconnect(iPhoneHandle);
            return 0;
        }
        public string DeviceVersion
        {
            get
            {
                return MobileDevice.AMDeviceCopyValue(iPhoneHandle, 0U, "ProductVersion");
            }
        }
        public string DeviceName
        {
            get
            {
                return MobileDevice.AMDeviceCopyValue(iPhoneHandle, 0U, "DeviceName");
            }
        }

        public void Delete(string path)
        {
            if (IsDirectory(path))
                DeleteDirectory(path, true);
            else if (IsFile(path))
                DeleteFile(path);
        }        
        public void CreateDirectory(string path)
        {
            int ret = MobileDevice.AFCDirectoryCreate(hAFC, FullPath(CurrentDirectory, path));
            if (ret != 0)
            {
                Console.WriteLine("WARNING: Could not create path!");
                Console.WriteLine("Returned {0} from creation of {1}", ret, path);
            }
        }               
        public void Rename(string sourceName, string destName)
        {
            int ret = MobileDevice.AFCRenamePath(hAFC, FullPath(CurrentDirectory, sourceName), FullPath(CurrentDirectory, destName));
            if (ret != 0)
            {
                Console.WriteLine("WARNING: Could not rename path!");
                Console.WriteLine("Returned {0} from renaming of {1} to {2}", ret, sourceName, destName);
            }
        }
        public void Eject()
        {
            if (hAFC != IntPtr.Zero)
            {
                MobileDevice.AFCConnectionClose(hAFC);
                MobileDevice.AMDeviceStopSession(iPhoneHandle);
                MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                hAFC = IntPtr.Zero;
                iPhoneHandle = IntPtr.Zero;
                connected = false;
            }
        }

        public bool Exists(string path)
        {
            IntPtr data = IntPtr.Zero;
            int ret = MobileDevice.AFCFileInfoOpen(hAFC, Encoding.UTF8.GetBytes(path), out data);
            if (ret == 0)
                MobileDevice.AFCKeyValueClose(data);
            return ret == 0;
        }
        public bool IsDirectory(string path)
        {
            bool is_dir;
            ulong size;

            GetFileInfo(path, out size, out is_dir);
            return is_dir;
        }
        public bool IsFile(string path)
        {
            string s = Get_st_ifmt(path);
            if (s != null)
                return Get_st_ifmt(path) == "S_IFREG";
            else return false;
        }
        public bool IsLink(string path)
        {
            return Get_st_ifmt(path) == "S_IFLNK";
        }
        
        public ConnectError ConnectToPhone()
        {
            int con = 0;
            if ((con = MobileDevice.AMDeviceConnect(iPhoneHandle)) != 0)
            {
                MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                if (con == -402653052) return ConnectError.DeviceNotConnected;
                return ConnectError.Connect;
            }
            if (MobileDevice.AMDeviceIsPaired(iPhoneHandle) == 0)
            {
                int ret = MobileDevice.AMDevicePair(iPhoneHandle);
                if (ret == -402653158)
                {//requires passcode
                    iBrowser.ErrorChange = 6;
                    MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                    return ConnectError.RequiresPasscode;
                }
                if (ret != 0)
                {
                    MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                    return ConnectError.Paired;
                }
            }
            int ret2;
            if ((ret2 = MobileDevice.AMDeviceValidatePairing(iPhoneHandle)) != 0)
            {
                if (ret2 == -402653156)
                {//invalid host id
                    iBrowser.ErrorChange = 1;
                    MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                    return ConnectError.InvalidHostId;
                }
                else
                {
                    MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                    return ConnectError.ValidatePair;
                }
            }

            if (MobileDevice.AMDeviceStartSession(iPhoneHandle) == 1)
            {
                MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                return ConnectError.StartSession;
            }
            if (MobileDevice.AMDeviceStartService(iPhoneHandle, "com.apple.afc2", ref hService, IntPtr.Zero) != 0)
            {
                int ret = 0;
                if ((ret = MobileDevice.AMDeviceStartService(iPhoneHandle, "com.apple.afc", ref hService, IntPtr.Zero)) != 0)
                {
                    if (ret == -402653158)
                    {//requires passcode
                        iBrowser.ErrorChange = 6;
                        MobileDevice.AMDeviceStopSession(iPhoneHandle);
                        MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                        return ConnectError.RequiresPasscode;
                    }
                    else {
                        MobileDevice.AMDeviceStopSession(iPhoneHandle);
                        MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                        Console.WriteLine(ret);
                        return ConnectError.StartService;
                    }
                }
            }
            if (MobileDevice.AFCConnectionOpen(hService, 0, ref hAFC) != 0)
            {
                MobileDevice.AFCConnectionClose(iPhoneHandle);
                MobileDevice.AMDeviceStopSession(iPhoneHandle);
                MobileDevice.AMDeviceDisconnect(iPhoneHandle);
                return ConnectError.ConnOpen;
            }
            connected = true;
            return ConnectError.None;
        }
        public ConnectError ConnectViaDevice(IntPtr device)
        {
            if (iPhoneHandle != IntPtr.Zero && this.connected && hAFC != IntPtr.Zero)
            {
                MobileDevice.AFCConnectionClose(hAFC);
                MobileDevice.AMDeviceStopSession(iPhoneHandle);
                MobileDevice.AMDeviceDisconnect(iPhoneHandle);
            }
            iPhoneHandle = device;
            return ConnectToPhone();
        }


        //Private
        private string Get_st_ifmt(string path)
        {
            Dictionary<string, string> fi = GetFileInfo(path);
            if (fi.ContainsKey("st_ifmt"))
            {
                if (fi.ContainsKey("LinkTarget"))
                    LinkTarget = fi["LinkTarget"];
                return fi["st_ifmt"];
            }
            else return (string)null;
        }

        private Dictionary<string, string> GetFileInfo(string path)
        {
            Dictionary<string, string> ans = new Dictionary<string, string>();
            IntPtr data = IntPtr.Zero;

            int ret = MobileDevice.AFCFileInfoOpen(hAFC, Encoding.UTF8.GetBytes(path), out data);
            if (ret == 0 && data != IntPtr.Zero)
            {
                IntPtr pname, pvalue;

                while (MobileDevice.AFCKeyValueRead(data, out pname, out pvalue) == 0 && pname != IntPtr.Zero && pvalue != IntPtr.Zero)
                {
                    string name = Marshal.PtrToStringAnsi(pname);
                    string value = Marshal.PtrToStringAnsi(pvalue);
                    ans.Add(name, value);
                }

                MobileDevice.AFCKeyValueClose(data);
            }

            return ans;
        }
        
        private bool doConstruction()
        {
            dnc = new DeviceNotificationCallback(NotifyCallback);
            IntPtr notification;
            try
            {
                if (MobileDevice.AMDeviceNotificationSubscribe(dnc, 0, 0, 0, out notification) != 0)
                    return false;
                current_directory = "/";
                return true;
            }
            catch (System.DllNotFoundException dll){
                System.Windows.Forms.MessageBox.Show("iTunes DLL not found!");
                return false;
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void NotifyCallback(ref AMDeviceNotificationCallbackInfo callback)
        {
            if (callback.msg == NotificationMessage.Connected)
            {
                Console.WriteLine("Connecting");
                iPhoneHandle = callback.dev_ptr;
                ConnectError error = ConnectError.DeviceNotConnected;
                while (error != ConnectError.None)
                    error = ConnectToPhone();
                    Console.WriteLine("Error: " + error);
                OnConnect(new ConnectEventArgs(callback));
            }
            else if (callback.msg == NotificationMessage.Disconnected)
            {
                connected = false;
                OnDisconnect(new ConnectEventArgs(callback));
            }
        }
        private void InternalDeleteDirectory(string path)
        {
            string full_path = FullPath(CurrentDirectory, path);
            string[] contents = GetFiles(path);
            for (int i = 0; i < contents.Length; i++)
            {
                DeleteFile(full_path + "/" + contents[i]);
            }

            contents = GetDirectories(path);
            for (int i = 0; i < contents.Length; i++)
            {
                InternalDeleteDirectory(full_path + "/" + contents[i]);
            }

            DeleteDirectory(path);
        }
        private void GetFileInfo(string path, out ulong size, out bool directory)
        {
            Dictionary<string, string> fi = GetFileInfo(path);

            size = fi.ContainsKey("st_size") ? System.UInt64.Parse(fi["st_size"]) : 0;

            bool SLink = false;
            directory = false;
            if (fi.ContainsKey("st_ifmt"))
            {
                switch (fi["st_ifmt"])
                {
                    case "S_IFDIR": directory = true; break;
                    case "S_IFLNK": SLink = true; break;
                }
            }

            if (SLink)
            { // test for symbolic directory link
                IntPtr hAFCDir = IntPtr.Zero;

                if (directory = (MobileDevice.AFCDirectoryOpen(hAFC, path, ref hAFCDir) == 0))
                    MobileDevice.AFCDirectoryClose(hAFC, hAFCDir);
            }
        }
        private void DeleteFile(string path)
        {
            string full_path = FullPath(CurrentDirectory, path);
            if (Exists(full_path))
            {
                int ret = MobileDevice.AFCRemovePath(hAFC, full_path);
                if (ret != 0)
                {
                    Console.WriteLine("WARNING: Could not delete path!");
                    Console.WriteLine("Returned {0} from deletion of {1}", ret, path);
                }
            }
        }
        private void DeleteDirectory(string path)
        {
            string full_path = FullPath(CurrentDirectory, path);
            if (IsDirectory(full_path))
            {
                int ret = MobileDevice.AFCRemovePath(hAFC, full_path);
                if (ret != 0)
                {
                    Console.WriteLine("WARNING: Could not delete path!");
                    Console.WriteLine("Returned {0} from deletion of {1}", ret, path);
                }
            }
        }
        private void DeleteDirectory(string path, bool recursive)
        {
            if (!recursive)
            {
                DeleteDirectory(path);
                return;
            }

            string full_path = FullPath(CurrentDirectory, path);
            if (IsDirectory(full_path))
            {
                InternalDeleteDirectory(path);
            }

        }

        internal string FullPath(string currentDirectory, string currentPath)
        {

            if ((currentDirectory == null) || (currentDirectory == String.Empty))
            {
                currentDirectory = "/";
            }

            if ((currentPath == null) || (currentPath == String.Empty))
            {
                currentPath = "/";
            }

            string[] path_parts;
            if (currentPath[0] == '/')
            {
                path_parts = currentPath.Split('/');
            }
            else if (currentDirectory[0] == '/')
            {
                path_parts = (currentDirectory + "/" + currentPath).Split('/');
            }
            else
            {
                path_parts = ("/" + currentDirectory + "/" + currentPath).Split('/');
            }

            string[] result_parts = new string[path_parts.Length];
            int target_index = 0;

            for (int i = 0; i < path_parts.Length; i++)
            {
                if (path_parts[i] == "..")
                {
                    if (target_index > 0)
                    {
                        target_index--;
                    }
                }
                else if ((path_parts[i] == ".") || (path_parts[i] == ""))
                {
                    // Do nothing
                }
                else
                {
                    result_parts[target_index++] = path_parts[i];
                }
            }

            return "/" + String.Join("/", result_parts, 0, target_index);
        }
    }
}
