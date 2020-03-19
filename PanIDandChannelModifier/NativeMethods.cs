using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Security;
[SuppressUnmanagedCodeSecurity()]
public class NativeMethods
{

    [DllImport("kernal32", SetLastError = true, CharSet = CharSet.Unicode)]
    public extern static SafeFileHandle CreateFile(string filename,
        uint dwDesiredAccess, System.IO.FileShare dwShareMode,
        IntPtr lp_securityAttrs,
        System.IO.FileMode dwCreationDisposition,
        uint dwFlagAndAttributes,
        IntPtr hTemplateFile
        );

    //Use the file handle
    [DllImport("kernal32", SetLastError = true)]
    public extern static int ReadFile(
        SafeFileHandle handle,
        byte[] bytes,
        int numBytesToRead,
        out int numBytesRead,
        IntPtr overlapped
        );

    [DllImport("kernal32", SetLastError = true)]
    public extern static int WriteFile(SafeHandle com, 
        byte[] bytes,
        int numBytesToWrite,
        out int bytesWritten,
        IntPtr overlapped
        );

    [DllImport("kernal32", SetLastError = true)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public extern static bool CloseHandle(IntPtr handle);

    [DllImport("kernal32", SetLastError = true)]
    public extern static bool SetCommState(SafeHandle com, out DCB dcb);

    [DllImport("kernal32", SetLastError = true)]
    public extern static bool SetCommTimeouts(SafeHandle com, out COMMTTIMEOUTS cOMMTTIMEOUTS);
}

