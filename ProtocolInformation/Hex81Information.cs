using DKCommunicationNET. BaseClass;
using DKCommunicationNET. Interface;
using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;


namespace DKCommunicationNET. ProtocolInformation;

internal class Hex81Information
{
    #region 【CommandCodes】[系统]

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

    #region 【Internal Methods】[创建报文格式]

    #region Internal Methods ==> [创建报文格式]

    /// <summary>
    /// 创建完整指令长度的【指令头】，长度大于7的报文不带CRC校验码，不可直接发送给串口，长度为7的无参命令则带校验码可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    ///  /// <param name="id">可选参数：设备ID</param>
    /// <returns>带指令信息的结果：完整指令长度</returns>
    internal static OperateResult<byte [ ]> CreateCommandHelper ( byte commandCode , ushort commandLength , ushort id = 0 )
    {
        byte _RxID;
        byte _TxID;

        if ( AnalysisID ( id ). IsSuccess )
        {
            _RxID = AnalysisID ( id ). Content [ 0 ];
            _TxID = AnalysisID ( id ). Content [ 1 ];
        }
        else
        {
            return AnalysisID ( id );
        }

        //尝试预创建报文
        try
        {
            byte [ ] buffer = new byte [ commandLength ];
            buffer [ 0 ] = FrameID;
            buffer [ 1 ] = _RxID;
            buffer [ 2 ] = _TxID;
            buffer [ 3 ] = BitConverter. GetBytes ( commandLength ) [ 0 ];
            buffer [ 4 ] = BitConverter. GetBytes ( commandLength ) [ 1 ];
            buffer [ 5 ] = commandCode;
            if ( commandLength == 7 )
            {
                buffer [ 6 ] = CRCcalculator ( buffer );    //如果是不带数据的命令则加上校验码
            }
            return OperateResult. CreateSuccessResult ( buffer );
        }

        //发生异常回报当前代码位置和异常信息
        catch ( Exception ex )
        {
            return new OperateResult<byte [ ]> ( StringResources. GetLineNum ( ) , ex. Message + "【From】" + StringResources. GetCurSourceFileName ( ) );
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
    internal static OperateResult<byte [ ]> CreateCommandHelper ( byte commandCode , ushort commandLength , byte [ ] data , ushort id = 0 )
    {
        try
        {
            OperateResult<byte [ ]> dataBytesWithoutData = CreateCommandHelper ( commandCode , commandLength , id );
            if ( dataBytesWithoutData. IsSuccess )
            {
                Array. Copy ( data , 0 , dataBytesWithoutData. Content , 6 , data. Length );
                dataBytesWithoutData. Content [ commandLength - 1 ] = CRCcalculator ( dataBytesWithoutData. Content );
                return dataBytesWithoutData;
            }
            else
            {
                return dataBytesWithoutData;
            }
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte [ ]> ( StringResources. GetLineNum ( ) , ex. Message + "From:" + StringResources. GetCurSourceFileName ( ) );
        }
    }
    #endregion Internal Methods ==> [创建报文格式]

    #endregion 【Internal Methods】

    #region 【Private Methods】
    /// <summary>
    /// 获取对应的数据的CRC校验码（异或和）
    /// </summary>
    /// <param name="sendBytes">需要校验的数据，不包含CRC字节，包含报文头0x81</param>
    /// <returns>返回CRC校验码</returns>
    private static byte CRCcalculator ( byte [ ] sendBytes )
    {
        byte crc = 0;

        //从第二个字节开始执行异或:忽略报文头
        for ( int i = 1 ; i < sendBytes. Length ; i++ )
        {
            crc ^= sendBytes [ i ];
        }
        return crc;
    }
    /// <summary>
    /// 解析ID,转换为两个字节
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <returns>返回带有信息的结果</returns>
    private static OperateResult<byte [ ]> AnalysisID ( ushort id )
    {
        try
        {
            byte [ ] twoBytesID = BitConverter. GetBytes ( id );  //低位在前            
            return OperateResult. CreateSuccessResult ( twoBytesID );
        }
        catch ( Exception )
        {
            return new OperateResult<byte [ ]> ( 1001 , "请输入正确的ID!" );
        }
    }
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
