using DKCommunicationNET. Core;

namespace DKCommunicationNET. Protocols. Hex5A;

internal class Hex5AInformation
{
    #region CommandCodes>>> 系统
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
    //public static readonly byte[ ] HandShakePacket = new byte[11] { 0x5A , 0xA5 , 0x0B , 0x00 , 0x00 , 0x00 , 0x01 , 0x11 , 0x1D , 0x00 , 0x96 };

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
    /// 设置设备信息
    /// </summary>
    public const byte SetDeviceInfo = 0x17;
    public const byte SetDeviceInfo_L = 42;

    /// <summary>
    /// 设置波特率
    /// </summary>
    public const byte SetBaudRate = 0x18;
    public const byte SetBaudRate_L = 13;

    #endregion CommandCodes>>>系统

    #region CommandCodes>>>ACS
    /// <summary>
    /// 【命令码】获取交流源档位
    /// </summary>
    internal const byte GetRanges_ACS = 0x12;
    internal const byte GetRanges_ACS_Len = 12;

    /// <summary>
    /// 设置模式及档位
    /// </summary>
    public const byte SetSystemModeAndRanges = 0x31;
    public const byte SetSystemModeAndRanges_L = 25;

    /// <summary>
    /// 设置标准源参数
    /// </summary>
    public const byte SetStandardSource = 0x32;

    /// <summary>
    /// 设置谐波参数
    /// </summary>
    public const byte SetHarmonics = 0x33;
    public const byte SetHarmonics_Clear_L = 13;

    /// <summary>
    /// 查询交流源数据
    /// </summary>
    public const byte ReadData_ACS = 0x40;


    #endregion CommandCodes>>>ACS

    #region CommandCodes>>>PPS
    /// <summary>
    /// 对时
    /// </summary>
    public const byte CompareTime = 0x13;
    public const byte CompareTime_L = 17;

    public const byte ReadData_PPS = 0x14;
    public const byte ReadData_PPS_L = 28;
    #endregion

    #region 【Internal Methods】

    #region Internal Methods ==> [创建报文格式]

    /// <summary>
    /// 创建完整指令长度的【指令头】，长度大于7的报文不带CRC校验码，不可直接发送给串口，长度为7的无参命令则带校验码可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    ///  /// <param name="id">可选参数：设备ID</param>
    /// <returns>带指令信息的结果：完整指令长度</returns>
    internal static OperateResult<byte[ ]> CreateCommandHelper ( byte commandCode , ushort commandLength , ushort id = 0 )
    {
        InitDic ( );
        byte ID;
        if ( AnalysisID ( id ). IsSuccess )
        {
            ID = AnalysisID ( id ). Content[0];
        }
        else
        {
            return AnalysisID ( id );
        }
        //尝试预创建报文
        try
        {
            byte[ ] buffer = new byte[commandLength];
            buffer[0] = Sync0;
            buffer[1] = Sync1;
            buffer[2] = BitConverter. GetBytes ( commandLength )[0];
            buffer[3] = BitConverter. GetBytes ( commandLength )[1];
            buffer[4] = ID;
            buffer[5] = 0x00;
            buffer[6] = DicFrameType[commandCode];
            buffer[7] = commandCode;
            buffer[commandLength - 1] = End;

            if ( commandLength == 11 )
            {
                buffer[8] = CRCcalculator ( buffer )[0];    //如果是不带数据的命令则加上校验码
                buffer[9] = CRCcalculator ( buffer )[1];    //如果是不带数据的命令则加上校验码
            }
            return OperateResult. CreateSuccessResult ( buffer );
        }

        //发生异常回报当前代码位置和异常信息
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( StringResources. GetLineNum ( ) , ex. Message + "【From】" + StringResources. GetCurSourceFileName ( ) );
        }
    }

    /// <summary>
    /// 带参数的完整报文可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    /// <param name="data">参数</param>
    /// <param name="id">可选参数：设备ID</param>
    /// <returns></returns>
    internal static OperateResult<byte[ ]> CreateCommandHelper ( byte commandCode , ushort commandLength , byte[ ] data , ushort id = 0 )
    {
        try
        {
            OperateResult<byte[ ]> dataBytesWithoutData = CreateCommandHelper ( commandCode , commandLength , id );
            if ( dataBytesWithoutData. IsSuccess )
            {
                Array. Copy ( data , 0 , dataBytesWithoutData. Content , 8 , data. Length );
                dataBytesWithoutData. Content[commandLength - 3] = CRCcalculator ( dataBytesWithoutData. Content )[0];
                dataBytesWithoutData. Content[commandLength - 2] = CRCcalculator ( dataBytesWithoutData. Content )[1];
                return dataBytesWithoutData;
            }
            else
            {
                return dataBytesWithoutData;
            }
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( StringResources. GetLineNum ( ) , ex. Message + "From:" + StringResources. GetCurSourceFileName ( ) );
        }
    }
    #endregion Internal Methods ==> [创建报文格式]

    #endregion 【Internal Methods】

    #region 【Private Methods】 

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

    #region Private Methods ==> [解析ID]
    /// <summary>
    /// 解析ID，转换为1个字节
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <returns>返回带有信息的结果</returns>
    private static OperateResult<byte[ ]> AnalysisID ( ushort id )
    {
        try
        {
            byte[ ] oneByteID = BitConverter. GetBytes ( id ); ;  //低位在前
            return OperateResult. CreateSuccessResult ( oneByteID );
        }
        catch ( Exception )
        {
            return new OperateResult<byte[ ]> ( 1001 , "请输入正确的ID!" );
        }
    }
    #endregion Private Methods ==> [解析ID]

    #region Private Methods ==> [帧类型和报文类型的字典]

    private static Dictionary<byte , byte> DicFrameType = new Dictionary<byte , byte> ( );

    /// <summary>
    /// 字典初始化
    /// </summary>
    private static void InitDic ( )
    {
        for ( byte i = 0x11 ; i < 0x19 ; i++ )
        {
            DicFrameType[i] = 0x01;
        }

        for ( byte i = 0x31 ; i < 0x39 ; i++ )
        {
            DicFrameType[i] = 0x02;
        }
        for ( byte i = 0x40 ; i < 0x44 ; i++ )
        {
            DicFrameType[i] = 0x02;
        }

        for ( byte i = 0x51 ; i < 0x54 ; i++ )
        {
            DicFrameType[i] = 0x03;
        }
        for ( byte i = 0x61 ; i < 0x65 ; i++ )
        {
            DicFrameType[i] = 0x04;
        }
        for ( byte i = 0xB1 ; i < 0xBA ; i++ )
        {
            DicFrameType[i] = 0x09;
        }
    }

    #endregion Private Methods ==> [帧类型和报文类型的字典]

    #endregion 【Private Methods】


}
#region 【枚举类型】

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
    Channel_All=0xFF
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
public enum Channel_WattPower  : byte    //TODO 功率通道选择需核实
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
