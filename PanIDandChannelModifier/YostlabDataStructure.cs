using System;
[global::System.AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
sealed class BitfieldLengthAttribute : Attribute
{
    uint length;

    public BitfieldLengthAttribute(uint length)
    {
        this.length = length;
    }

    public uint Length { get { return length; } }
}

public struct DataPacket {
    float gyroX;
    float gyroY;
    float gyroZ;
    float accX;
    float accY;
    float accZ;
}

/*
 typedef struct _COMMTIMEOUTS {
  DWORD ReadIntervalTimeout;
  DWORD ReadTotalTimeoutMultiplier;
  DWORD ReadTotalTimeoutConstant;
  DWORD WriteTotalTimeoutMultiplier;
  DWORD WriteTotalTimeoutConstant;
} COMMTIMEOUTS, *LPCOMMTIMEOUTS;
*/

[Serializable]
public struct COMMTTIMEOUTS {
    uint ReadIntervalTimeout;
    uint ReadTotalTimeoutMultiplier;
    uint ReadTotalTimeoutConstant;
    uint WriteTotalTimeoutMultiplier;
    uint WriteTotalTimeoutConstant;
}

/*
 typedef struct _DCB {
  DWORD DCBlength;
  DWORD BaudRate;
  DWORD fBinary : 1;
  DWORD fParity : 1;
  DWORD fOutxCtsFlow : 1;
  DWORD fOutxDsrFlow : 1;
  DWORD fDtrControl : 2;
  DWORD fDsrSensitivity : 1;
  DWORD fTXContinueOnXoff : 1;
  DWORD fOutX : 1;
  DWORD fInX : 1;
  DWORD fErrorChar : 1;
  DWORD fNull : 1;
  DWORD fRtsControl : 2;
  DWORD fAbortOnError : 1;
  DWORD fDummy2 : 17;
  WORD  wReserved;
  WORD  XonLim;
  WORD  XoffLim;
  BYTE  ByteSize;
  BYTE  Parity;
  BYTE  StopBits;
  char  XonChar;
  char  XoffChar;
  char  ErrorChar;
  char  EofChar;
  char  EvtChar;
  WORD  wReserved1;
} DCB, *LPDCB;
     */

[Serializable]
public struct DCB {
    uint DCBlength;
    uint BaudRate;
    [BitfieldLength(1)]
    uint fBinary; //1
    [BitfieldLength(1)]
    uint fParity; // 1
    [BitfieldLength(1)]
    uint fOutxCtsFlow; // 1
    [BitfieldLength(1)]
    uint fOutxDsrFlow; // 1
    [BitfieldLength(2)]
    uint fDtrControl; // 2
    [BitfieldLength(1)]
    uint fDsrSensitivity; // 1
    [BitfieldLength(1)]
    uint fTXContinueOnXoff;// 1
    [BitfieldLength(1)]
    uint fOutX; // 1
    [BitfieldLength(1)]
    uint fInX; // 1
    [BitfieldLength(1)]
    uint fErrorChar; // 1
    [BitfieldLength(1)]
    uint fNull; // 1
    [BitfieldLength(2)]
    uint fRtsControl; // 2
    [BitfieldLength(1)]
    uint fAbortOnError; // 1
    [BitfieldLength(17)]
    uint fDummy2; // 17
    ushort wReserved;
    ushort XonLim;
    ushort XoffLim;
    byte ByteSize;
    byte Parity;
    byte StopBits;
    char XonChar;
    char XoffChar;
    char ErrorChar;
    char EofChar;
    char EvtChar;
    ushort wReserved1;
}