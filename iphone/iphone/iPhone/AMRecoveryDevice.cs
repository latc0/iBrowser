using System;
using System.Runtime.InteropServices;

namespace iphone
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMRecoveryDevice
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown0;			/* 0 */
        public DeviceRestoreNotificationCallback callback;		/* 8 */
        public IntPtr user_info;			/* 12 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] unknown1;			/* 16 */
        public uint readwrite_pipe;		/* 28 */
        public byte read_pipe;          /* 32 */
        public byte write_ctrl_pipe;    /* 33 */
        public byte read_unknown_pipe;  /* 34 */
        public byte write_file_pipe;    /* 35 */
        public byte write_input_pipe;   /* 36 */
    };
}