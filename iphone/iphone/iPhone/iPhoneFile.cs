using System;
using System.IO;
using System.Text;

namespace iphone
{

    public class iPhoneFile : Stream
    {
        private enum OpenMode
        {
            None = 0,
            Read = 2,
            Write = 3
        }

        private iPhone phone;
        private long handle;
        private OpenMode mode;

        private iPhoneFile(iPhone phone, long handle, OpenMode mode)
            : base()
        {
            this.phone = phone;
            this.handle = handle;
            this.mode = mode;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            long len = (long)count;
            byte[] temp = buffer;
            MobileDevice.AFCFileRefRead(phone.AFCHandle, handle, temp, ref len);
            return (int)len;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            uint len = (uint)count;
            byte[] temp = buffer;
            MobileDevice.AFCFileRefWrite(phone.AFCHandle, handle, temp, len);
        }

        public static iPhoneFile Open(iPhone phone, string path, FileAccess openmode)
        {
            OpenMode mode = OpenMode.None;

            switch (openmode)
            {
                case FileAccess.Read:
                    mode = OpenMode.Read;
                    break;
                case FileAccess.Write:
                    mode = OpenMode.Write;
                    break;
            }

            long handle;
            string full_path;

            full_path = phone.FullPath(phone.CurrentDirectory, path);
            if (MobileDevice.AFCFileRefOpen(phone.AFCHandle, Encoding.UTF8.GetBytes(full_path), (int)mode, out handle) != 0)
            {//10 =  device is locked
                iBrowser.stopCopy = true;
                return (iPhoneFile)null;
            }
            iBrowser.stopCopy = false;
            return new iPhoneFile(phone, handle, mode);
        }
        public static iPhoneFile OpenRead(iPhone phone, string path)
        {
            return iPhoneFile.Open(phone, path, FileAccess.Read);
        }
        public static iPhoneFile OpenWrite(iPhone phone, string path)
        {
            return iPhoneFile.Open(phone, path, FileAccess.Write);
        }

        public override bool CanRead
        {
            get
            {
                return (mode == OpenMode.Read);
            }
        }
        public override bool CanSeek
        {
            get { return false; }
        }
        public override bool CanWrite
        {
            get
            {
                return (mode == OpenMode.Write);
            }
        }
        public override long Length
        {
            get { throw new NotImplementedException("The method or operation is not implemented."); }
        }
        public override long Position
        {
            get
            {
                uint ret;
                ret = 0;

                MobileDevice.AFCFileRefTell(phone.AFCHandle, handle, ref ret);
                return (long)ret;
            }
            set
            {
                this.Seek(value, SeekOrigin.Begin);
            }
        }
        public override void SetLength(long value)
        {
            int ret;

            ret = MobileDevice.AFCFileRefSetFileSize(phone.AFCHandle, handle, (uint)value);
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            MobileDevice.AFCFileRefSeek(phone.AFCHandle, handle, (long)(uint)offset, 0L);
            return offset;
        }
        public override void Flush()
        {
            MobileDevice.AFCFlushData(phone.AFCHandle, handle);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (handle != 0)
                {
                    MobileDevice.AFCFileRefClose(phone.AFCHandle, handle);
                    handle = 0;
                }
            }
            base.Dispose(disposing);
        }
    }
}
