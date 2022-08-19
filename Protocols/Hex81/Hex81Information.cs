using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;


namespace DKCommunicationNET. Protocols. Hex81;

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
    internal static readonly byte[ ] HandShakePacket = new byte[7] { 0x81 , 0x00 , 0x00 , 0x07 , 0x00 , 0x4C , 0x4B };

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

    #region CommandCodes ==> [交流源/表]
    /// <summary>
    /// 交流源关闭命令
    /// </summary>
    public const byte CloseACS = 0x4F;  //2022年7月7日
    public const ushort CloseACSLength = 7;

    /// <summary>
    /// 交流源打开命令
    /// </summary>
    public const byte OpenACS = 0x54; //2022年7月7日
    public const ushort OpenACSLength = 7;

    /// <summary>
    /// 读取交流标准源档位信息
    /// </summary>
    public const byte GetRangesOfACS = 0x11;
    public const byte GetRangesOfACSLength = 7;

    /// <summary>
    /// 设置交流源档位参数 
    /// </summary>
    public const byte SetACSourceRanges = 0x31; //2022年7月7日
    public const ushort SetRangesLength = 16;  //!51F具备IPa,IPb,IPc

    /// <summary>
    /// 设置源幅度参数
    /// </summary>
    public const byte SetACSAmplitude = 0x32;    //2022年7月7日
    public const ushort SetACSAmplitudeLength = 43; //!51F具备IPa,IPb,IPc

    /// <summary>
    /// 设置源相位参数
    /// </summary>
    public const byte WritePhase = 0x33;    //2022年7月8日 10点22分
    public const ushort WritePhaseLength = 31;

    /// <summary>
    /// 设置源频率参数:当 Fa=Fb!=Fc 时，Flag=2；Fa=Fb=Fc 时，Flag=3,只设置Fa则三相同频
    /// </summary>
    public const byte WriteFrequency = 0x34;    //2022年7月8日 12点34分
    public const ushort WriteFrequencyLength = 20;//注意：设置时 Fa=Fb，Fc 可以设置为与 AB 相不同的频率
                                                  //也可以只设置 Fa，则默认为三相同频，用于兼容以前的设备通讯程序

    /// <summary>
    /// 设置源接线模式:
    /// </summary>
    public const byte SetWireMode = 0x35;   //2022年7月8日 19点31分
    public const ushort SetWireModeLength = 8;

    /// <summary>
    /// 闭环控制使能命令：HarmonicMode ：谐波模式，0-以真有效值的百分比输入谐波（有效值恒定）；1-以基波值的百分比输入谐波（基波恒定）
    /// </summary>
    public const byte SetClosedLoop = 0x36;     //2022年7月9日
    public const ushort SetClosedLoopLength = 9;

    /// <summary>
    /// 设置谐波参数：注意：建议协议长度不超过 256，超过 256 个字节建议分批发送。
    /// </summary>
    public const byte WriteHarmonics = 0x58; //2022年7月10日
    public const ushort WriteHarmonicsClearLength = 9;

    /// <summary>
    /// 设置有功功率
    /// </summary>
    public const byte WriteWattPower = 0x50;
    public const ushort WriteWattPowerLength = 12;

    /// <summary>
    /// 设置无功功率
    /// </summary>
    public const byte WriteWattlessPower = 0x51; //TODO 确认协议描述是否有误
    public const byte WriteWattlessPowerLength = 12;

    /// <summary>
    /// 读交流标准表参数/数据：读标准源输出值
    /// </summary>
    public const byte ReadACSourceData = 0x4D;
    public const byte ReadACSourceDataLength = 7;

    /// <summary>
    /// 读系统状态位：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    public const byte ReadACStatus = 0x4E;
    public const byte ReadACStatusLength = 7;
    #endregion CommandCodes ==> [交流源/表]

    #endregion 【CommandCodes】[系统]

    #region 【Internal Methods】
    /// <summary>
    /// 获取对应的数据的CRC校验码（异或和）
    /// </summary>
    /// <param name="sendBytes">需要校验的数据，不包含CRC字节，包含报文头0x81</param>
    /// <returns>返回CRC校验码</returns>
    internal static byte CRCcalculator ( byte[ ] sendBytes )
    {
        byte crc = 0;
        
        //从第二个字节开始执行异或:忽略报文头
        for ( int i = 1 ; i < sendBytes. Length ; i++ )
        {
            crc ^= sendBytes[i];
        }
        return crc;
    }

    #endregion Internal Methods 
}

#region 【枚举类型】
/// <summary>
/// 系统模式
/// </summary>
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

/// <summary>
/// //0x01:ACS;0x02:ACM;0x04:DCS;0x08:DCM;0x10:PQ 
/// </summary>
[Flags]
public enum FuncB
{
    ACS = 0B_0000_0001,
    ACM = 0B_0000_0010,
    DCS = 0B_0000_0100,
    DCM = 0B_0000_1000,
    EPQ = 0B_0001_0000,
}

/// <summary>
/// D0：双频输出，D1：保护电流，D2：闪变输出，D3：遥信功能，D4：400Hz 高频输出，D5：电机控制
/// </summary>
[Flags]
public enum FuncS
{
    双频输出 = 0B_0000_0001,
    保护电流 = 0B_0000_0010,
    闪变输出 = 0B_0000_0100,
    遥信功能 = 0B_0000_1000,
    高频输出 = 0B_0001_0000,
    电机控制 = 0B_0010_0000,
}
#endregion 枚举类型


