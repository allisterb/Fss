using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fss.Rasp.Win32
{
    public partial class UnsafeNativeMethods
    {
        public const int FILE_ATTRIBUTE_DIRECTORY = 16;

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct WIN32_FIND_DATA
        {
            public int dwFileAttributes;
            public int ftCreationTime_dwLowDateTime;
            public int ftCreationTime_dwHighDateTime;
            public int ftLastAccessTime_dwLowDateTime;
            public int ftLastAccessTime_dwHighDateTime;
            public int ftLastWriteTime_dwLowDateTime;
            public int ftLastWriteTime_dwHighDateTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr FindFirstFile(string pFileName, ref WIN32_FIND_DATA pFindFileData);

        [DllImport("kernel32.dll")]
        public static extern bool FindNextFile(IntPtr hFindFile, ref WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll")]
        public static extern bool FindClose(IntPtr hFindFile);

    }
}
