using DKCommunicationNET. Core;

/**************************************************************************************************
 * 
 *  【Hex5AA5协议的信息类】 版本：V 1.0.0   Author:  Fuhong Zhou   2022年9月8日 22点43分  
 *  
 *  支持的协议为DK-PTS系列通讯协议V2018 修订时间：2021年06月 作者：苏老师
 *
 *************************************************************************************************/

namespace DKCommunicationNET. Protocols. Hex5A;

/// <summary>
/// Hex5A协议的固态信息
/// </summary>
internal class Hex5AInformation
{
    #region 《系统设置
    /// <summary>
    /// 报文头
    /// </summary>
    internal const byte Sync0 = 0x5A;
    internal const byte Sync1 = 0xA5;
    internal const int DataStartIndex = 8;

    /// <summary>
    /// 【命令码】报文尾
    /// </summary>
    internal const byte End = 0x96;

    /// <summary>
    /// 【命令码】联机命令，读取终端型号和版本号
    /// </summary>
    internal const byte HandShake = 0x11;

    /// <summary>
    /// 【命令码】设置系统模式
    /// </summary>
    internal const byte SetSystemMode = 0x44;
    internal const ushort SetSystemModeCommandLength = 8;

    /// <summary>
    /// 【命令码】设置当前终端显示界面
    /// </summary>
    internal const byte SetDisplayPage = 0x4A;
    internal const ushort SetDisplayPageCommandLength = 8;

    /// <summary>
    /// 【命令码】设置设备信息
    /// </summary>
    public const byte SetDeviceInfo = 0x17;
    public const byte SetDeviceInfo_L = 42;

    /// <summary>
    /// 【命令码】设置波特率
    /// </summary>
    public const byte SetBaudRate = 0x18;
    public const byte SetBaudRate_L = 13;
    #endregion 系统设置》

    #region 《交流源 ACS
    /// <summary>
    /// 【命令码】获取交流源档位
    /// </summary>
    internal const byte GetRanges_ACS = 0x12;
    internal const byte GetRanges_ACS_Len = 12;

    /// <summary>
    /// 【命令码】设置模式及档位
    /// </summary>
    public const byte SetSystemModeAndRanges = 0x31;
    public const byte SetSystemModeAndRanges_L = 25;

    /// <summary>
    /// 【命令码】设置标准源参数
    /// </summary>
    public const byte SetStandardSource = 0x32;

    /// <summary>
    /// 【命令码】设置谐波参数
    /// </summary>
    public const byte SetHarmonics = 0x33;
    public const byte SetHarmonics_Clear_L = 13;

    /// <summary>
    /// 【命令码】查询交流源数据
    /// </summary>
    public const byte ReadData_ACS = 0x40;
    #endregion 交流源 ACS》

    #region 《对时模块
    /// <summary>
    /// 【命令码】对时命令码
    /// </summary>
    public const byte CompareTime = 0x13;
    public const byte CompareTime_L = 17;

    /// <summary>
    /// 【命令码】读对时模块数据命令码
    /// </summary>
    public const byte ReadData_PPS = 0x14;
    public const byte ReadData_PPS_L = 28;
    #endregion 对时模块》

    #region Private Methods ==> [校验码计算器]

    /// <summary>
    /// 获取对应的数据的CRC校验码（和）
    /// </summary>
    /// <param name="sendBytes">需要校验的数据，不包含CRC字节，包含报文头0x81</param>
    /// <returns>返回CRC校验码</returns>
    internal static byte[ ] CRCcalculator ( byte[ ] sendBytes )
    {
        ushort crc = 0;

        //从第三个字节开始执行代数和:忽略报文头
        for ( int i = 2 ; i < sendBytes. Length - 2 ; i++ )
        {
            crc += sendBytes[i];
        }
        return BitConverter. GetBytes ( crc );
    }
    #endregion Private Methods ==> [校验码计算器]
}
#region 【枚举类型】
/// <summary>
/// 交流源工作模式
/// </summary>
public enum ACSMode : byte
{
    /// <summary>
    /// 
    /// </summary>
    标准源 = 0,
    /// <summary>
    /// 
    /// </summary>
    功耗测试 = 3
}

/// <summary>
/// 交流源输出状态
/// </summary>
public enum ACSStatus:byte
{
    /// <summary>
    /// 
    /// </summary>
    源停止=0b_0000_0000,
    /// <summary>
    /// 
    /// </summary>
    源输出=0b_0000_0001,
    /// <summary>
    /// 
    /// </summary>
    源稳定=0b_0000_0010,
    /// <summary>
    /// 
    /// </summary>
    故障停止=0b_0001_0000,
    //TODO D7的作用是什么？表状态是ACM的情况？
}

/// <summary>
/// 获取档位类型
/// </summary>
public enum Type_Module : byte
{
    /// <summary>
    /// 交流源
    /// </summary>
    ACS = 1,

    /// <summary>
    /// 钳表
    /// </summary>
    ACM_Cap = 2,

    /// <summary>
    /// 直流源
    /// </summary>
    DCS = 3,

    /// <summary>
    /// 直流表
    /// </summary>
    DCM = 4,

    /// <summary>
    /// 交流表
    /// </summary>
    ACM = 7
}

/// <summary>
/// 故障码定义：枚举。此为获取故障信息的第二种方式
/// </summary>
[Flags]
public enum ErrorCodes : byte
{
    /// <summary>
    /// 
    /// </summary>
    ErrorUa = 0b_0000_0001,    // 0x01 // 1
    /// <summary>
    /// 
    /// </summary>
    ErrorUb = 0b_0000_0010,    // 0x02 // 2
    /// <summary>
    /// 
    /// </summary>
    ErrorUc = 0b_0000_0100,    // 0x04 // 4
    /// <summary>
    /// 
    /// </summary>
    ErrorIa = 0b_0000_1000,    // 0x08 // 8
    /// <summary>
    /// 
    /// </summary>
    ErrorIb = 0b_0001_0000,    // 0x10 // 16
    /// <summary>
    /// 
    /// </summary>
    ErrorIc = 0b_0010_0000,    // 0x20 // 32
    /// <summary>
    /// 
    /// </summary>
    ErrorDC = 0b_0100_0000     // 0x40 // 64
}

/// <summary>
/// 对时方式
/// </summary>
public enum Type_CompareTime : byte
{
    /// <summary>
    /// 手动设置（B-RS485 出）
    /// </summary>
    Manual_B485 = 0,

    /// <summary>
    /// GPS 入/B-RS485 出
    /// </summary>
    GPS = 1,
    /// <summary>
    /// B-RS485 入出
    /// </summary>
    B_485 = 2,

    /// <summary>
    /// B-RS232 入出
    /// </summary>
    B_232 = 3,

    /// <summary>
    /// TTL 秒脉冲入出
    /// </summary>
    Manual_TTL = 4
}

/// <summary>
/// 设置模式及档位的【参数】：类型标识
/// </summary>
[Flags]
public enum Flag_SetType : byte
{
    /// <summary>
    /// 设置工作模式
    /// </summary>
    SetWorkMode = 0b_0000_0001,
    /// <summary>
    /// 设置接线模式
    /// </summary>
    SetWireMode = 0B_0000_0010,
    /// <summary>
    /// 设置控制模式
    /// </summary>
    SetCloseLoopMode = 0b_0000_0100,
    /// <summary>
    /// 设置谐波模式
    /// </summary>
    SetHarmonicMode = 0b_0000_1000,
    /// <summary>
    /// 设置无功计算方法
    /// </summary>
    SetWattLessMethod = 0b_0001_0000,
    /// <summary>
    /// 设置档位
    /// </summary>
    SetRanges = 0b_0010_0000
}

/// <summary>
/// 设置模式及档位的【参数】：SetWorkMode
/// </summary>
public enum WorkMode : byte
{
    /// <summary>
    /// 标准源
    /// </summary>
    StandardSource = 0b_0000_0001,

    /// <summary>
    /// 
    /// </summary>
    Consumption = 0b_0000_0100
}

/// <summary>
/// 设置标准源的参数:类型
/// </summary>
public enum Type_SetStandardSource : byte
{
    /// <summary>
    /// 幅值
    /// </summary>
    Amplitude = 1,
    /// <summary>
    /// 相位
    /// </summary>
    Phase = 2,
    /// <summary>
    /// 频率
    /// </summary>
    Freqency = 3,
    /// <summary>
    /// 有功
    /// </summary>
    WattPower = 4,
    /// <summary>
    /// 无功
    /// </summary>
    WattlessPower = 5,
}

/// <summary>
/// 通道号
/// </summary>
[Flags]
public enum Channels : byte
{
    /// <summary>
    /// A相电压
    /// </summary>
    Channel_Ua = 0b_0000_0001,

    /// <summary>
    /// B相电压
    /// </summary>
    Channel_Ub = 0b_0000_0100,

    /// <summary>
    /// C相电压
    /// </summary>
    Channel_Uc = 0b_0001_0000,

    /// <summary>
    /// A相电流
    /// </summary>
    Channel_Ia = 0b_0000_0010,

    /// <summary>
    /// B相电流
    /// </summary>
    Channel_Ib = 0b_0000_1000,

    /// <summary>
    /// C相电流
    /// </summary>
    Channel_Ic = 0b_0010_0000,

    /// <summary>
    /// X相电压
    /// </summary>
    Channel_Ux = 0b_0100_0000,

    /// <summary>
    /// X相电流
    /// </summary>
    Channel_Ix = 0b_1000_0000,

    /// <summary>
    /// 所有相电压[不含X相]
    /// </summary>
    Channel_U = Channel_Ua | Channel_Ub | Channel_Uc,

    /// <summary>
    /// 所有相电流[不含X相]
    /// </summary>
    Channel_I = Channel_Ia | Channel_Ib | Channel_Ic,

    /// <summary>
    /// 所有电压电流通道
    /// </summary>
    Channel_All = 0xFF
}

/// <summary>
/// 频率通道
/// </summary>
[Flags]
public enum SetStandardSource_Channels_Freq : byte
{
    /// <summary>
    /// 频率
    /// </summary>
    Freq = 0b_0000_0001,

    /// <summary>
    /// X相频率
    /// </summary>
    Freq_X = 0b_0100_0000
}

/// <summary>
/// 设置有功功率通道枚举
/// </summary>
[Flags]
public enum Channel_WattPower : byte    //TODO 功率通道选择需核实
{
    /// <summary>
    /// 总有功功率
    /// </summary>
    P_Sum = 0b_1000_0000
}

/// <summary>
/// 设置无功功率通道枚举
/// </summary>
public enum Channel_WattLessPower : byte
{

}

#endregion 枚举类型

#region 结构体
/// <summary>
/// 设置标准源参数
/// </summary>
public struct SetStandardSourceArgs
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="channels"></param>
    /// <param name="value"></param>
    public SetStandardSourceArgs ( Enum channels , float value )
    {
        Channels = channels;
        Value = value;
    }
    /// <summary>
    /// 通道
    /// </summary>
    public Enum Channels { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// 解数据
    /// </summary>
    /// <param name="setSourceArgs"></param>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    public static OperateResult<byte[ ]> SourceArgsToBytes ( SetStandardSourceArgs setSourceArgs , IByteTransform byteTransform )
    {
        try
        {
            byte[ ] bytes = new byte[5];
            bytes[0] = Convert. ToByte ( setSourceArgs. Channels );
            byteTransform. TransByte ( setSourceArgs. Value ). CopyTo ( bytes , 1 );
            return OperateResult. CreateSuccessResult ( bytes );
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( ex. Message );
        }
    }
}
#endregion
