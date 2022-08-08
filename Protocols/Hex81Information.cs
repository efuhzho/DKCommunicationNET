using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;


namespace DKCommunicationNET. Protocols;

/// <summary>
/// Hex81协议信息类
/// </summary>
internal class Hex81Information
{
    #region 【CommandCodes】[系统设置]

    /// <summary>
    /// 报文头
    /// </summary>
    internal const byte FrameID = 0x81;

    /// <summary>
    /// 系统应答命令
    /// </summary>
    internal const byte OK = 0x4B;
    internal const ushort OKLength = 8;

    /// <summary>
    /// 发送故障代码，带枚举数据
    /// </summary>
    internal const byte ErrorCode = 0x52;
    internal const byte ErrorCodeLength = 8;

    #region CommandCodes ==> [系统设置]

    /// <summary>
    /// 联机命令，读取终端型号和版本号
    /// </summary>
    internal const byte HandShake = 0x4C;
    internal const ushort HandShakeCommandLength = 7;

    /// <summary>
    /// 设置系统模式
    /// </summary>
    internal const byte SetSystemMode = 0x44;
    internal const ushort SetSystemModeCommandLength = 8;

    /// <summary>
    /// 设置当前终端显示界面
    /// </summary>
    internal const byte SetDisplayPage = 0x4A;
    internal const ushort SetDisplayPageCommandLength = 8;

    #endregion CommandCodes ==> [系统设置]

    #endregion 【CommandCodes】[系统]



    #region 【Internal Methods】
    /// <summary>
    /// 获取对应的数据的CRC校验码（异或和）
    /// </summary>
    /// <param name="sendBytes">需要校验的数据，不包含CRC字节，包含报文头0x81</param>
    /// <returns>返回CRC校验码</returns>
    internal static byte CRCcalculator ( byte [ ] sendBytes )
    {
        byte crc = 0;

        //从第二个字节开始执行异或:忽略报文头
        for ( int i = 1 ; i < sendBytes. Length ; i++ )
        {
            crc ^= sendBytes [ i ];
        }
        return crc;
    }

    #endregion Internal Methods

    #region 【枚举类型】
    public enum SystemModes : byte
    {
        /// <summary>
        /// 标准源模式
        /// </summary>
        ModeDefault = 0,

        /// <summary>
        /// 标准表模式
        /// </summary>
        ModeStandardMeter = 1,

        /// <summary>
        /// 标准表（钳表）模式
        /// </summary>
        ModeStandardMeterClamp = 2,

        /// <summary>
        /// 标准源校准模式
        /// </summary>
        ModeStandardSourceCalibrate = 10,

        /// <summary>
        /// 标准表校准模式
        /// </summary>
        ModeStandardMeterCalibrate = 11,

        /// <summary>
        /// 钳表校准模式
        /// </summary>
        ModeStandardClampCalibrate = 12,

        /// <summary>
        /// 直流源校准模式
        /// </summary>
        ModeDCSourceCalibrate = 13,

        /// <summary>
        /// 直流表校准模式
        /// </summary>
        ModeDCMeterCalibrate = 14
    }

    /// <summary>
    /// 故障码定义：枚举。此为获取故障信息的第二种方式
    /// </summary>
    [Flags]
    public enum ErrorCodes : byte
    {
        ErrorUa = 0b_0000_0001,    // 0x01 // 1
        ErrorUb = 0b_0000_0010,    // 0x02 // 2
        ErrorUc = 0b_0000_0100,    // 0x04 // 4
        ErrorIa = 0b_0000_1000,    // 0x08 // 8
        ErrorIb = 0b_0001_0000,    // 0x10 // 16
        ErrorIc = 0b_0010_0000,    // 0x20 // 32
        ErrorDC = 0b_0100_0000     // 0x40 // 64
    }
    #endregion 枚举类型
}
