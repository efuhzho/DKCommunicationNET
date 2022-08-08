using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Interface. IPacketBuilder;

namespace DKCommunicationNET. Protocols;

internal class Hex81PacketBuilder : IPacketBuilder
{
    /// <summary>
    /// 创建完整指令长度的【指令头】，长度大于7的报文不带CRC校验码，不可直接发送给串口，长度为7的无参命令则带校验码可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    ///  /// <param name="id">可选参数：设备ID</param>
    /// <returns>带指令信息的结果：完整指令长度</returns>
    public OperateResult<byte[]> PacketBuilder ( byte commandCode , ushort commandLength , ushort id = 0 )
    {
        byte _RxID;
        byte _TxID;

        if ( AnalysisID ( id ). IsSuccess )
        {
            _RxID = AnalysisID ( id ). Content[0];
            _TxID = AnalysisID ( id ). Content[1];
        }
        else
        {
            return AnalysisID ( id );
        }

        //尝试预创建报文
        try
        {
            byte[] buffer = new byte[commandLength];
            buffer[0] = Hex81Information. FrameID;
            buffer[1] = _RxID;
            buffer[2] = _TxID;
            buffer[3] = BitConverter. GetBytes ( commandLength )[0];
            buffer[4] = BitConverter. GetBytes ( commandLength )[1];
            buffer[5] = commandCode;
            if ( commandLength == 7 )
            {
                buffer[6] = Hex81Information. CRCcalculator ( buffer );    //如果是不带数据的命令则加上校验码
            }
            return OperateResult. CreateSuccessResult ( buffer );
        }

        //发生异常回报当前代码位置和异常信息
        catch ( Exception ex )
        {
            return new OperateResult<byte[]> ( StringResources. GetLineNum ( ) , ex. Message + "【From】" + StringResources. GetCurSourceFileName ( ) );
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
    public OperateResult<byte[]> PacketBuilder ( byte commandCode , ushort commandLength , byte[] data , ushort id = 0 )
    {
        try
        {
            OperateResult<byte[]> shell = PacketBuilder ( commandCode , commandLength , id );
            if ( !shell. IsSuccess )
            {
                return shell;
            }

            Array. Copy ( data , 0 , shell. Content , 6 , data. Length );

            shell. Content[commandLength - 1] = Hex81Information. CRCcalculator ( shell. Content );
            return shell;
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte[]> ( StringResources. GetLineNum ( ) , "From:" + StringResources. GetCurSourceFileName ( ) + ex. Message );
        }
    }

    #region 【Private Methods】
    /// <summary>
    /// 解析ID,转换为两个字节
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <returns>返回带有信息的结果</returns>
    private OperateResult<byte[]> AnalysisID ( ushort id )
    {
        try
        {
            byte[] twoBytesID = BitConverter. GetBytes ( id );  //低位在前            
            return OperateResult. CreateSuccessResult ( twoBytesID );
        }
        catch ( Exception )
        {
            return new OperateResult<byte[]> ( 1001 , "请输入正确的ID!" );
        }
    }
    #endregion Private Methods
}
