namespace DKCommunicationNET. Protocols. Hex81;

/// <summary>
/// Hex81协议信息类
/// </summary>
internal class Hex81Information
{
    /// <summary>
    /// 报文头
    /// </summary>
    internal const byte FrameID = 0x81;
    internal const int DataStartIndex = 6;

    /// <summary>
    /// 联机命令，读取终端型号和版本号
    /// </summary>
    internal const byte HandShake = 0x4C;
    internal const ushort HandShake_Length = 7;
    internal static readonly byte[ ] HandShakePacket = new byte[7] { 0x81 , 0x00 , 0x00 , 0x07 , 0x00 , 0x4C , 0x4B };

    #region 【CommandCodes】>>>[系统设置]
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

    /// <summary>
    /// 设置系统模式
    /// </summary>
    internal const byte SetSystemMode = 0x44;
    internal const ushort SetSystemMode_Length = 8;

    /// <summary>
    /// 设置当前终端显示界面
    /// </summary>
    internal const byte SetDisplayPage = 0x4A;
    internal const ushort SetDisplayPage_Length = 8;
    #endregion CommandCodes ==> [系统设置]

    #region 【CommandCodes】>>>[交流源/表]
    /// <summary>
    /// 交流源关闭命令
    /// </summary>
    public const byte CloseACS = 0x4F;

    /// <summary>
    /// 交流源打开命令
    /// </summary>
    public const byte OpenACS = 0x54;

    /// <summary>
    /// 读取交流标准源档位信息
    /// </summary>
    public const byte GetRanges_ACS = 0x11;

    /// <summary>
    /// 设置交流源档位参数 
    /// </summary>
    public const byte SetRanges_ACS = 0x31;
    public const ushort SetRanges_ACS_Length = 16;  //!51F具备IPa,IPb,IPc

    /// <summary>
    /// 设置源幅度参数
    /// </summary>
    public const byte SetAmplitude_ACS = 0x32;
    public const ushort SetAmplitude_ACS_Length = 43; //!51F具备IPa,IPb,IPc

    /// <summary>
    /// 设置源相位参数
    /// </summary>
    public const byte SetPhase = 0x33;
    public const ushort SetPhaseLength = 31;

    /// <summary>
    /// 设置源频率参数:当 Fa=Fb!=Fc 时，Flag=2；Fa=Fb=Fc 时，Flag=3,只设置Fa则三相同频
    /// </summary>
    public const byte SetFrequency = 0x34;    //2022年7月8日 12点34分
    public const ushort SetFrequencyLength = 20;//注意：设置时 Fa=Fb，Fc 可以设置为与 AB 相不同的频率
                                                //也可以只设置 Fa，则默认为三相同频，用于兼容以前的设备通讯程序

    /// <summary>
    /// 设置源接线模式:
    /// </summary>
    public const byte SetWireMode = 0x35;
    public const ushort SetWireModeLength = 8;

    /// <summary>
    /// 闭环控制使能命令：HarmonicMode ：谐波模式，0-以真有效值的百分比输入谐波（有效值恒定）；1-以基波值的百分比输入谐波（基波恒定）
    /// </summary>
    public const byte SetClosedLoop = 0x36;
    public const ushort SetClosedLoopLength = 9;

    /// <summary>
    /// 设置谐波参数：注意：建议协议长度不超过 256，超过 256 个字节建议分批发送。
    /// </summary>
    public const byte SetHarmonics = 0x58;

    /// <summary>
    /// 设置有功功率
    /// </summary>
    public const byte SetWattPower = 0x50;
    public const ushort SetWattPowerLength = 12;

    /// <summary>
    /// 设置无功功率
    /// </summary>
    public const byte SetWattlessPower = 0x51; //TODO 确认协议描述是否有误
    public const byte SetWattlessPowerLength = 12;

    #endregion CommandCodes ==> [交流源/表]

    #region 【CommandCodes】>>>交流标准表

    /// <summary>
    /// 读交流标准表参数/数据：读标准源输出值
    /// </summary>
    public const byte ReadData_ACS = 0x4D;

    /// <summary>
    /// 读系统状态位：Flag=0表示输出稳定，Flag=1表示输出未稳定。：读标准源输出状态
    /// </summary>
    public const byte GetStatus_ACS = 0x4E;

    #endregion 【CommandCodes】>>>交流标准表

    #region 【CommandCodes】>>>直流表
    /// <summary>
    /// 读取直流表档位/量程信息
    /// </summary>
    public const byte GetRanges_DCM = 0x13;

    /// <summary>
    /// 设置直流表量程:0-直流电压；1-直流电流；2-纹波电压；3-纹波电流。
    /// </summary>
    public const byte SetRange_DCM = 0x61;
    public const byte SetRange_DCM_Length = 9;

    /// <summary>
    /// 【无参】读直流表测量参数/数据。
    /// </summary>
    public const byte ReadData_DCM = 0x62;

    ///// <summary>
    ///// 设置直流表测量类型：0-直流；1-纹波。
    ///// </summary>
    //public const byte SetDCMeterMesureType = 0x63;         
    //TODO NotImplemented.设置直流表测量类型：0-直流；1-纹波。

    ///// <summary>
    ///// 【适用于双通道】设置直流表测量参数/数据。
    ///// </summary>
    //public const byte SetDCMeterDataWithTwoCh = 0x64;     
    //TODO NotImplemented.【适用于双通道】设置直流表测量参数/数据。

    ///// <summary>
    ///// 【适用于双通道】读直流表测量参数/数据。
    ///// </summary>
    //public const byte ReadDCMeterDataWithTwoCh = 0x65;    
    //TODO NotImplemented.【适用于双通道】读直流表测量参数/数据。
    #endregion 【CommandCodes】>>>直流表

    #region 【CommandCodes】>>>直流源
    /// <summary>
    /// 读取直流源档位信息
    /// </summary>
    public const byte GetRanges_DCS = 0x12;

    /// <summary>
    /// 设置直流源档位参数
    /// </summary>
    public const byte SetRange_DCS = 0x66;
    public const byte SetRange_DCS_Length = 9;

    /// <summary>
    /// 打开直流源
    /// </summary>
    public const byte Open_DCS = 0x67;
    public const byte Open_DCS_Length = 8;

    /// <summary>
    /// 关闭直流源
    /// </summary>
    public const byte Stop_DCS = 0x68;
    public const byte Stop_DCS_Length = 8;

    /// <summary>
    /// 设置直流源幅值
    /// </summary>
    public const byte SetAmplitude_DCS = 0x69;
    public const byte SetAmplitude_DCS_Length = 13;

    /// <summary>
    /// 读直流源参数/数据
    /// </summary>
    public const byte ReadData_DCS = 0x79;
    public const byte ReadData_DCS_Length = 8;
    #endregion 【CommandCodes】>>>直流源

    #region 【CommandCodes】>>>电能
    /// <summary>
    /// 读电能误差
    /// </summary>
    public const byte ReadData_EPQ = 0x45;

    /// <summary>
    /// 设置电能校验参数并启动电能校验
    /// </summary>
    public const byte SetElectricity = 0x37;
    public const byte SetElectricity_Length = 32;

    #endregion 【CommandCodes】>>>电能

    #region 【CommandCodes】>>>校准

    /// <summary>
    /// 清空校准参数，恢复初始状态
    /// </summary>
    public const byte Calibrate_ClearData = 0x20;
    public const byte Calibrate_ClearDataLength = 10;

    /// <summary>
    /// 切换交流校准档位
    /// </summary>
    public const byte Calibrate_SwitchACRange = 0x21;
    public const byte Calibrate_SwitchACRangeLength = 9;

    /// <summary>
    /// 切换交流源校准点命令
    /// </summary>
    public const byte Calibrate_SwitchACPoint = 0x22;
    public const byte Calibrate_SwitchACPointLength = 34;

    /// <summary>
    /// 确认执行当前校准点的校准数据：在输入标准表数据后执行*！0x22还是0x23存疑，协议文档前后不一致 
    /// </summary>
    public const byte Calibrate_DoAC = 0x23;    //TODO 核实命令码
    public const byte Calibrate_DoACLength = 34;

    /// <summary>
    /// 保存校准参数
    /// </summary>
    public const byte Calibrate_Save = 0x24;
    public const byte Calibrate_SaveLength = 10;

    /// <summary>
    /// 执行交流标准表和钳形表校准命令
    /// </summary>
    public const byte Calibrate_DoACMeter = 0x25;
    public const byte Calibrate_DoACMeterlength = 9;

    /// <summary>
    /// 切换直流源校准点
    /// </summary>
    public const byte Calibrate_SwitchDCPoint = 0x26;
    public const byte Calibrate_SwitchDCPointLength = 14;

    /// <summary>
    /// 执行直流源校准
    /// </summary>
    public const byte Calibrate_DoDC = 0x27;
    public const byte Calibrate_DoDClength = 14;

    /// <summary>
    /// 执行直流表校准
    /// </summary>
    public const byte Calibrate_DoDCMeter = 0x28;
    public const byte Calibrate_DoDCMeterLength = 14;

    #endregion 【CommandCodes】>>>校准

    #region 【Internal Methods】>>> CRC校验码计算器
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
/// 显示页面枚举
/// </summary>
public enum DisplayPage : byte
{
    #region 【标准源模式下有效】
    /// <summary>
    /// 【标准源模式下有效】Menu功能选择界面：仅彩屏有效，黑白屏无效。需先切换到Menu界面才能继续操作显示其他界面
    /// </summary>
    PageMenu = 0,

    /// <summary>
    /// 【标准源模式下有效】默认的开机界面：交流源输出界面
    /// </summary>
    PageDefault = 1,

    /// <summary>
    /// 【标准源模式下有效】波形显示界面：彩屏是波形显示；黑白屏是相位输出
    /// </summary>
    PageWave = 2,

    /// <summary>
    /// 【标准源模式下有效】矢量显示界面
    /// </summary>
    PagePhasor = 3,

    /// <summary>
    /// 【标准源模式下有效】谐波设置界面
    /// </summary>
    PageHarmony = 4,

    /// <summary>
    /// 【标准源模式下有效】电能校验界面
    /// </summary>
    PageElectricity = 8,

    /// <summary>
    /// 【标准源模式下有效】直流测量界面
    /// </summary>
    PageDCMeter = 5,

    /// <summary>
    /// 【标准源模式下有效】直流输出界面
    /// </summary>
    PageDC = 6,
    #endregion 标准源模式下有效

    #region 【标准表模式下有效】
    /// <summary>
    /// 【标准表模式下有效】参数测量界面
    /// </summary>
    PageStandardMeterData = 9,

    /// <summary>
    /// 【标准表模式下有效】相位测量界面：彩屏是波形显示,黑白屏是相位显示
    /// </summary>
    PageStandardMeterWave = 10,

    /// <summary>
    /// 【标准表模式下有效】矢量显示界面
    /// </summary>
    PageStandardMeterPhasor = 11,
    #endregion 【标准表模式下有效】

    #region 钳表模式下有效
    /// <summary>
    /// 【钳表模式下有效】钳表测量界面
    /// </summary>
    PageClampData = 12,

    /// <summary>
    /// 【钳表模式下有效】钳表相位测量界面
    /// </summary>
    PageClampPhase = 13, //黑白屏，彩屏为波形显示

    /// <summary>
    /// 【钳表模式下有效】钳表测试矢量显示界面
    /// </summary>
    PageClampPhasor = 14,
    #endregion
}

/// <summary>
/// 接线方式枚举
/// </summary>
public enum WireMode : byte
{
    /// <summary>
    /// 三相四线制
    /// </summary>
    WireMode_3P4L = 00,

    /// <summary>
    /// 三相三线制
    /// </summary>
    WireMode_3P3L = 01,

    /// <summary>
    /// 单相
    /// </summary>
    WireMode_1P1L = 02,

    /// <summary>
    /// 二线两元件（两个互感器）
    /// </summary>
    WireMode_2Component = 03,

    /// <summary>
    /// 二线三元件（三个互感器）
    /// </summary>
    WireMode_3Component = 04,
}

/// <summary>
/// 闭环控制定义
/// </summary>
public enum CloseLoopMode : byte
{
    /// <summary>
    /// 闭环
    /// </summary>
    CloseLoop = 0,

    /// <summary>
    /// 开环
    /// </summary>
    OpenLoop = 1
}

/// <summary>
/// 谐波模式
/// </summary>
public enum HarmonicMode : byte
{
    /// <summary>
    /// 以真有效值的百分比输入谐波
    /// </summary>
    ValidValuesConstant = 0,

    /// <summary>
    /// 以基波值的百分比输入谐波
    /// </summary>
    FundamentalConstant = 1
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
/// //0x01=ACS；0x02=ACM；0x04=DCS；0x08=DCM；0x10=PQ 
/// </summary>
[Flags]
internal enum FuncB
{
    Enabled_ACS = 0B_0000_0001,
    Enabled_ACM = 0B_0000_0010,
    Enabled_DCS = 0B_0000_0100,
    Enabled_DCM = 0B_0000_1000,
    Enabled_EPQ = 0B_0001_0000,
}

/// <summary>
/// D0=双频输出，D1=保护电流，D2=闪变输出，D3=遥信功能，D4=400Hz 高频输出，D5=电机控制
/// </summary>
[Flags]
internal enum FuncS
{
    Enabled_DualFreqs = 0B_0000_0001,
    Enabled_IProtect = 0B_0000_0010,
    Enabled_PST = 0B_0000_0100,
    Enabled_YX = 0B_0000_1000,
    Enabled_HF = 0B_0001_0000,
    Enabled_PWM = 0B_0010_0000,
}

/// <summary>
/// 谐波设置通道选择
/// </summary>
[Flags]
public enum ChannelsHarmonic : byte
{
    /// <summary>
    /// A相电压
    /// </summary>
    Channel_Ua = 0b_0000_0001,  // 0x01 // 1

    /// <summary>
    /// B相电压
    /// </summary>
    Channel_Ub = 0b_0000_0010,  // 0x02 // 2

    /// <summary>
    /// C相电压
    /// </summary>
    Channel_Uc = 0b_0000_0100,  // 0x04 // 4

    /// <summary>
    /// A相电流
    /// </summary>
    Channel_Ia = 0b_0000_1000,  // 0x08 // 8

    /// <summary>
    /// B相电流
    /// </summary>
    Channel_Ib = 0b_0001_0000,  // 0x10 // 16

    /// <summary>
    /// C相电流
    /// </summary>
    Channel_Ic = 0b_0010_0000,  // 0x20 // 32

    /// <summary>
    /// 所有相电压
    /// </summary>
    Channel_U = Channel_Ua | Channel_Ub | Channel_Uc,    // 0x07 // 7

    /// <summary>
    /// 所有相电流
    /// </summary>
    Channel_I = Channel_Ia | Channel_Ib | Channel_Ic,   // 0x38 // 56

    /// <summary>
    /// 电压所有相和电流所有相
    /// </summary>
    Channel_All = Channel_U | Channel_I   // 0x3F // 63
}

/// <summary>
/// 设置有功功率通道枚举
/// </summary>
public enum Channel_WattPower : byte
{
    /// <summary>
    /// A相有功功率
    /// </summary>
    Channel_Pa = 0,
    /// <summary>
    /// B相有功功率
    /// </summary>
    Channel_Pb = 1,
    /// <summary>
    /// C相有功功率
    /// </summary>
    Channel_Pc = 2,
    /// <summary>
    /// 总有功功率
    /// </summary>
    Channel_Psum = 3
}

/// <summary>
/// 设置有功功率通道枚举
/// </summary>
public enum Channel_WattLessPower : byte
{
    /// <summary>
    /// A相无功功功率
    /// </summary>
    Channel_Qa = 0,

    /// <summary>
    /// B相无功功率
    /// </summary>
    Channel_Qb = 1,

    /// <summary>
    /// C相无功功率
    /// </summary>
    Channel_Qc = 2,

    /// <summary>
    /// 总无功功率
    /// </summary>
    Channel_Qsum = 3
}

/// <summary>
/// 电能校验类型（ 电能测量）
/// </summary>
public enum ElectricityType : byte
{
    /// <summary>
    /// 有功功率
    /// </summary>
    P = ( byte ) 'P', //0x50,

    /// <summary>
    /// 无功功率
    /// </summary>
    Q = ( byte ) 'Q' //0x51
}

/// <summary>
/// 直流表测量类型
/// </summary>
public enum DCMerterMeasureType : byte
{
    /// <summary>
    /// 直流电压
    /// </summary>
    DCM_Voltage = 0,

    /// <summary>
    /// 直流电流
    /// </summary>
    DCM_Current = 1,

    /// <summary>
    /// 纹波电压
    /// </summary>
    DCM_VoltageRipple = 2,

    /// <summary>
    /// 纹波电流
    /// </summary>
    DCM_CurrentRipple = 3
}

/// <summary>
/// 直流源输出类型
/// </summary>
public enum DCS_Type : byte
{
    /// <summary>
    /// 直流源电压输出
    /// </summary>
    DCS_Type_U = ( byte ) 'U', //85;0x55

    /// <summary>
    /// 直流源电流输出
    /// </summary>
    DCS_Type_I = ( byte ) 'I', //73;0x49

    /// <summary>
    /// 直流电阻输出
    /// </summary>
    DCS_Type_R = ( byte ) 'R'  //82;0x52
}

/// <summary>
/// 校准时的操作类型
/// </summary>
public enum CalibrateType : byte
{
    /// <summary>
    /// 校准标准源
    /// </summary>
    标准源 = 0,

    /// <summary>
    /// 校准标准表
    /// </summary>
    标准表 = 1,

    /// <summary>
    /// 校准钳形表
    /// </summary>
    钳形表 = 2,

    /// <summary>
    /// 校准直流源
    /// </summary>
    直流源 = 3,

    /// <summary>
    /// 校准直流表
    /// </summary>
    直流表 = 4,
}

/// <summary>
/// 当前校准点
/// </summary>
public enum CalibrateLevel : byte
{
    /// <summary>
    /// 零点
    /// </summary>
    零点 = 0,

    /// <summary>
    /// 20%校准点
    /// </summary>
    校准点20 = 1,

    /// <summary>
    /// 100%校准点
    /// </summary>
    校准点100 = 2,

    /// <summary>
    /// 相位校准
    /// </summary>
    相位校准 = 3,
}

/// <summary>
/// 直流源校准类型
/// </summary>
public enum Calibrate_DCSourceType : byte
{
    /// <summary>
    /// 直流电压校准
    /// </summary>
    直流电压 = ( byte ) 'U',

    /// <summary>
    /// 直流电流校准
    /// </summary>
    直流电流 = ( byte ) 'I'
}

/// <summary>
/// 直流表校准类型
/// </summary>
public enum Calibrate_DCMeterType : byte
{
    /// <summary>
    /// 直流电压校准
    /// </summary>
    直流电压 = 0,

    /// <summary>
    /// 直流电流校准
    /// </summary>
    直流电流 = 1,

    /// <summary>
    /// 纹波电压校准
    /// </summary>
    纹波电压 = 2,

    /// <summary>
    /// 纹波电流校准
    /// </summary>
    纹波电流 = 3
}
#endregion 枚举类型


