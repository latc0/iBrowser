using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace iphone
{
    public class ShellEx
    {
        private const int SHGFI_SMALLICON = 0x1;
        private const int SHGFI_LARGEICON = 0x0;
        private const int SHIL_JUMBO = 0x4;
        private const int SHIL_EXTRALARGE = 0x2;
        private const int WM_CLOSE = 0x0010;

        public enum IconSizeEnum
        {
            SmallIcon16 = SHGFI_SMALLICON,
            MediumIcon32 = SHGFI_LARGEICON,
            LargeIcon48 = SHIL_EXTRALARGE,
            ExtraLargeIcon = SHIL_JUMBO
        }

        [DllImport("user32")]
        private static extern
            IntPtr SendMessage(
            IntPtr handle,
            int Msg,
            IntPtr wParam,
            IntPtr lParam);


        [DllImport("shell32.dll")]
        private static extern int SHGetImageList(
            int iImageList,
            ref Guid riid,
            out Shell.IImageList ppv);

        [DllImport("Shell32.dll")]
        public static extern int SHGetFileInfo(
            string pszPath, 
            int dwFileAttributes, 
            ref Shell.SHFILEINFO psfi, 
            int cbFileInfo, 
            uint uFlags);

        [DllImport("user32")]
        public static extern int DestroyIcon(
            IntPtr hIcon);

        public static Bitmap GetFolderIcon(IconSizeEnum iconsize)
        {
            IntPtr hIcon = GetIconHandleFromFolderPath("aPath", iconsize);
            return getBitmapFromIconHandle(hIcon);
        }

        public static Bitmap GetFileIcon(string filepath, IconSizeEnum iconsize)
        {
            IntPtr hIcon = GetIconHandleFromFilePath(filepath, iconsize);
            return getBitmapFromIconHandle(hIcon);
        }

        public static string GetFileTypeDescription(string fileName)
        {
            var shinfo = new Shell.SHFILEINFO();

            const uint SHGFI_TYPENAME = 0x400;
            const uint SHGFI_USEFILEATTRIBUTES = 0x10;
            const int FILE_ATTRIBUTE_NORMAL = 0x80;
            uint flags = SHGFI_TYPENAME | SHGFI_USEFILEATTRIBUTES;

            SHGetFileInfo(fileName, FILE_ATTRIBUTE_NORMAL, ref shinfo, Marshal.SizeOf(shinfo), flags);
            DestroyIcon(shinfo.hIcon);
            SendMessage(shinfo.hIcon, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            return shinfo.szTypeName;
        }

        private static Bitmap getBitmapFromIconHandle(IntPtr hIcon)
        {
            if (hIcon == IntPtr.Zero) throw new FileNotFoundException();
            var myIcon = Icon.FromHandle(hIcon);
            var bitmap = myIcon.ToBitmap();
            myIcon.Dispose();
            DestroyIcon(hIcon);
            SendMessage(hIcon, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            return bitmap;
        }

        private static IntPtr GetIconHandleFromFilePath(string filepath, IconSizeEnum iconsize)
        {
            var shinfo = new Shell.SHFILEINFO();

            const uint SHGFI_ICON = 0x100;
            const uint SHGFI_USEFILEATTRIBUTES = 0x10;
            const int FILE_ATTRIBUTE_NORMAL = 0x80;
            uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;

            return getIconHandleFromFilePathWithFlags(iconsize, filepath, FILE_ATTRIBUTE_NORMAL, ref shinfo,  flags);
        }

        private static IntPtr GetIconHandleFromFolderPath(string folderpath, IconSizeEnum iconsize)
        {
            var shinfo = new Shell.SHFILEINFO();

            const uint SHGFI_ICON = 0x100;
            const uint SHGFI_USEFILEATTRIBUTES = 0x10;
            const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
            uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;

            return getIconHandleFromFilePathWithFlags(iconsize, folderpath, FILE_ATTRIBUTE_DIRECTORY, ref shinfo, flags);
        }

        private static IntPtr getIconHandleFromFilePathWithFlags(
            IconSizeEnum iconsize, string filepath, int fileAttributeFlag, ref Shell.SHFILEINFO shinfo, uint flags)
        {
            const int ILD_TRANSPARENT = 1;
            var retval = SHGetFileInfo(filepath, fileAttributeFlag, ref shinfo, Marshal.SizeOf(shinfo), flags);
            if (retval == 0) throw (new FileNotFoundException());
            var iconIndex = shinfo.iIcon;
            var iImageListGuid = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
            Shell.IImageList iml;
            var hres = SHGetImageList((int)iconsize, ref iImageListGuid, out iml);
            var hIcon = IntPtr.Zero;
            hres = iml.GetIcon(iconIndex, ILD_TRANSPARENT, ref hIcon);
            return hIcon;
        }

    }
}
