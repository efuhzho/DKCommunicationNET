using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. Threading. Tasks;

namespace DKCommunicationNET. Core
{
    internal class CheckResponse
    {
        ////public static OperateResult<byte[ ]> CheckResponse ( byte[ ] send )
        ////{
        ////    // 发送报文并获取回复报文
        ////    OperateResult<byte[ ]> response = ReadBase ( send );

        ////    //获取回复不成功
        ////    if ( !response. IsSuccess )
        ////    {
        ////        return response;
        ////    }

        ////    // 长度校验
        ////    if ( response. Content. Length < 7 )
        ////    {
        ////        return new OperateResult<byte[ ]> ( 811300 , StringResources. Language. ReceiveDataLengthTooShort + "811300" );
        ////    }

        ////    // 检查crc
        ////    if ( !DK81CommunicationInfo. CheckCRC ( response. Content ) )
        ////    {
        ////        return new OperateResult<byte[ ]> ( StringResources. Language. CRCCheckFailed + SoftBasic. ByteToHexString ( response. Content , ' ' ) );
        ////    }

        ////    //回复OK
        ////    if ( response. Content[5] == DK81CommunicationInfo. OK && response. Content[6] == send[5] )
        ////    {
        ////        return response;
        ////    }

        ////    // 检查是否报故障：是     //TODO 随时主动报故障的问题
        ////    if ( response. Content[5] == DK81CommunicationInfo. ErrorCode )
        ////    {
        ////        return new OperateResult<byte[ ]> ( response. Content[6] , ( ( ErrorCode ) response. Content[6] ). ToString ( ) ); //TODO 测试第二种故障码解析:/*DK81CommunicationInfo.GetErrorMessageByErrorCode(response.Content[6])*/
        ////    }

        ////    //检查命令码：命令码不一致且不是OK命令
        ////    if ( send[5] != response. Content[5] )
        ////    {
        ////        return new OperateResult<byte[ ]> ( response. Content[5] , $"Receive CommandCode Check Failed:SendCode is {send[5]},ReceivedCode is {response. Content[5]}" );
        ////    }

        ////    return response;
        ////}

        /////// <summary>
        /////// 读取串口的数据
        /////// </summary>
        /////// <param name="send">发送的原始字节数据</param>
        /////// <returns>带接收字节的结果对象</returns>
        ////public OperateResult<byte[ ]> ReadBase ( byte[ ] send )
        ////{
        ////    hybirdLock. Enter ( );

        ////    //是否先清空缓存
        ////    if ( IsClearCacheBeforeRead )
        ////    {
        ////        ClearSerialCache ( );
        ////    }

        ////    //发送报文
        ////    OperateResult sendResult = SPSend ( _SerialPort , send );

        ////    //发送报文失败
        ////    if ( !sendResult. IsSuccess )
        ////    {
        ////        hybirdLock. Leave ( );
        ////        return OperateResult. CreateFailedResult<byte[ ]> ( sendResult );
        ////    }

        ////    //发送报文成功则接收数据
        ////    OperateResult<byte[ ]> receivedResult = SPReceived ( _SerialPort , true );
        ////    hybirdLock. Leave ( );

        ////    return receivedResult;
        //}
    }
}
