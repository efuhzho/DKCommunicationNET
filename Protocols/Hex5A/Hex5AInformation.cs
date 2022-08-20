namespace DKCommunicationNET. Protocols. Hex5A;

internal class Hex5AInformation
{
    #region 【CommandCodes】[系统]

    /// <summary>
    /// 报文头
    /// </summary>
    internal const byte Sync0 = 0x5A;
    internal const byte Sync1 = 0xA5;

    /// <summary>
    /// 报文尾
    /// </summary>
    internal const byte End = 0x96;

    /// <summary>
    /// 系统应答命令
    /// </summary>
    internal const byte OK = 0x11;
    internal const ushort OKLength = 11;

    /// <summary>
    /// 发送故障代码，带枚举数据
    /// </summary>
    internal const byte ErrorCode = 0x52;
    internal const byte ErrorCodeLength = 8;
    
    #region CommandCodes ==> [系统设置]

    /// <summary>
    /// 联机命令，读取终端型号和版本号
    /// </summary>
    internal const byte HandShake = 0x11;
    internal const ushort HandShakeCommandLength = 11;
    public static readonly byte[ ] HandShakePacket = new byte[11] { 0x5A , 0xA5 , 0x0B , 0x00 , 0x00 , 0x00 , 0x01 , 0x11 , 0x1D , 0x00 , 0x96 };

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

    #region 【Internal Methods】[创建报文格式]

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

    #region 【Private Methods】 [校验码计算器][解析ID][帧类型和报文类型的字典]

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

    #endregion Private Methods

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

    public enum GetRangeType : byte
    {
        ACS = 1,
        ACM_Cap = 2,
        DCS = 3,
        DCM = 4,
        ACM = 7,
           
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
