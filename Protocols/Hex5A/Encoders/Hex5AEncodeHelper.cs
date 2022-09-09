
/**************************************************************************************************
 *
 *  【Hex5A协议创建报文规则】   版本：Ver 1.0.0    Author: Fuhong Zhou     2022年9月8日 22点47分
 *
 *  支持的协议为DK-PTS系列通讯协议V2018 修订时间：2021年06月 作者：苏老师
 *
 *************************************************************************************************/

namespace DKCommunicationNET. Protocols. Hex5A. Encoders;

[Model ( Models. Hex5A )]
internal class Hex5AEncodeHelper : IEncodeHelper
{
    readonly ushort _id;
    internal Hex5AEncodeHelper ( ushort id )
    {
        _id = id;
        InitDic ( );
    }

    /// <summary>
    /// 无参指令报文创建
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <returns>带指令信息的结果：完整指令长度</returns>
    public OperateResult<byte[ ]> EncodeHelper ( byte commandCode )
    {
        return PacketShellBuilderHelper ( commandCode , 11 );
    }

    /// <summary>
    /// 带参数的完整报文可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    /// <param name="data">参数</param>
    /// <returns></returns>
    public OperateResult<byte[ ]> EncodeHelper ( byte commandCode , ushort commandLength , byte[ ] data )
    {
        OperateResult<byte[ ]> dataBytesWithoutData = PacketShellBuilderHelper ( commandCode , commandLength );
        try
        {

            if ( dataBytesWithoutData. IsSuccess && dataBytesWithoutData. Content != null )
            {
                Array. Copy ( data , 0 , dataBytesWithoutData. Content , 8 , data. Length );


                dataBytesWithoutData. Content[commandLength - 3] = Hex5AInformation. CRCcalculator ( dataBytesWithoutData. Content )[0];
                dataBytesWithoutData. Content[commandLength - 2] = Hex5AInformation. CRCcalculator ( dataBytesWithoutData. Content )[1];
                return dataBytesWithoutData;
            }
            else
            {
                return dataBytesWithoutData;
            }
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( ex. Message );
        }
    }

    #region  私有方法>>>PacketShellBuilderHelper
    /// <summary>
    /// 创建完整指令长度的【指令头】，长度大于7的报文不带CRC校验码，不可直接发送给串口，长度为7的无参命令则带校验码可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    /// <returns>带指令信息的结果：完整指令长度</returns>
    private OperateResult<byte[ ]> PacketShellBuilderHelper ( byte commandCode , ushort commandLength )
    {
        //尝试预创建报文
        try
        {
            byte ID = AnalysisID ( _id );
            byte[ ] buffer = new byte[commandLength];
            buffer[0] = Hex5AInformation. Sync0;
            buffer[1] = Hex5AInformation. Sync1;
            buffer[2] = BitConverter. GetBytes ( commandLength )[0];
            buffer[3] = BitConverter. GetBytes ( commandLength )[1];
            buffer[4] = ID;
            buffer[5] = 0x00;
            buffer[6] = DicFrameType[commandCode];
            buffer[7] = commandCode;
            buffer[commandLength - 1] = Hex5AInformation. End;

            if ( commandLength == 11 )
            {
                buffer[8] = Hex5AInformation. CRCcalculator ( buffer )[0];    //如果是不带数据的命令则加上校验码
                buffer[9] = Hex5AInformation. CRCcalculator ( buffer )[1];    //如果是不带数据的命令则加上校验码
            }
            return OperateResult. CreateSuccessResult ( buffer );
        }

        //发生异常回报当前代码位置和异常信息
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( ex. Message );
        }
    }
    #endregion

    #region Private Methods ==> [解析ID]
    /// <summary>
    /// 解析ID，转换为1个字节
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <returns>返回带有信息的结果</returns>
    private static byte AnalysisID ( ushort id )
    {
        byte[ ] oneByteID = BitConverter. GetBytes ( id ); ;  //低位在前
        return oneByteID[0];
    }
    #endregion Private Methods ==> [解析ID]

    #region Private Methods ==> [帧类型和报文类型的字典]

    /// <summary>
    /// 字典用于自动判定帧类型
    /// </summary>
    private readonly Dictionary<byte , byte> DicFrameType = new ( );

    /// <summary>
    /// 字典初始化
    /// </summary>
    private void InitDic ( )
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
}
