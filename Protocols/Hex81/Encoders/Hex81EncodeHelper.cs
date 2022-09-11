using System;
using System. Collections. Generic;
using System. Linq;
using System. Security. Cryptography;
using System. Text;
using System. Threading. Tasks;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. Protocols. Hex81. Encoders;

/// <summary>
/// Hex81协议报文创建类
/// </summary>
[Model ( Models. Hex81 )]
internal class Hex81EncodeHelper : IEncodeHelper
{
    readonly ushort _id;

    public Hex81EncodeHelper ( ushort id )
    {
        _id = id;
    }

    /// <summary>
    /// 创建完整指令长度的【指令头】，长度大于7的报文不带CRC校验码，不可直接发送给串口，长度为7的无参命令则带校验码可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    /// <returns>带指令信息的结果：完整指令长度</returns>
    public OperateResult<byte[ ]> EncodeShell ( byte commandCode , ushort commandLength )
    {
        //尝试预创建报文
        try
        {
            byte[ ] id = AnalysisID ( _id );
            byte[ ] length = BitConverter. GetBytes ( commandLength );

            byte[ ] buffer = new byte[commandLength];
            buffer[0] = Hex81. FrameID;
            buffer[1] = id[0];
            buffer[2] = id[1];
            buffer[3] = length[0];
            buffer[4] = length[1];
            buffer[5] = commandCode;
            if ( commandLength == 7 )
            {
                buffer[6] = Hex81. CRCcalculator ( buffer );    //如果是不带数据的命令则加上校验码
            }
            return OperateResult. CreateSuccessResult ( buffer );
        }

        //发生异常回报当前代码位置和异常信息
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( ex. Message );
        }
    }

    /// <summary>
    /// 带参数的完整报文可直接发送给串口
    /// </summary>
    /// <param name="commandCode">命令码</param>
    /// <param name="commandLength">指令长度</param>
    /// <param name="data">参数</param>
    /// <returns>带指令信息的结果：完整指令长度</returns>
    public OperateResult<byte[ ]> EncodeHelper ( byte commandCode , ushort commandLength , byte[ ] data )
    {
        try
        {
            OperateResult<byte[ ]> shell = EncodeShell ( commandCode , commandLength );
            if ( !shell. IsSuccess || shell. Content == null )
            {
                return shell;
            }

            Array. Copy ( data , 0 , shell. Content , 6 , data. Length );

            shell. Content[commandLength - 1] = Hex81. CRCcalculator ( shell. Content );
            return shell;
        }
        catch ( Exception ex )
        {
            return new OperateResult<byte[ ]> ( ex. Message );
        }
    }

    /// <summary>
    /// 无参命令报文创建
    /// </summary>
    /// <param name="commandCode"></param>
    /// <returns></returns>
    public OperateResult<byte[ ]> EncodeHelper ( byte commandCode )
    {
        return EncodeShell ( commandCode , 7 );
    }

    #region 【Private Methods】
    /// <summary>
    /// 解析ID,转换为两个字节
    /// </summary>
    /// <param name="id">设备ID</param>
    /// <returns>返回带有信息的结果</returns>
    private static byte[ ] AnalysisID ( ushort id )
    {

        byte[ ] twoBytesID = BitConverter. GetBytes ( id );  //低位在前            
        return twoBytesID;
    }

    #endregion Private Methods
}
