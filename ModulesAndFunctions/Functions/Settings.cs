using DKCommunicationNET. Core;
using DKCommunicationNET. Protocols;

namespace DKCommunicationNET. ModulesAndFunctions. Functions;

public class Settings : IFuncSettings
{
    /// <summary>
    /// 发送报文，获取并校验下位机的回复报文的委托方法
    /// </summary>
    private readonly Func<byte[ ] , bool , OperateResult<byte[ ]>> _methodOfCheckResponse;

    /// <summary>
    /// 定义交流源模块对象
    /// </summary>
    private readonly IEncoder_Settings _packetsBuilder;

    /// <summary>
    /// 定义解码器对象
    /// </summary>
    private readonly IDecoder_Settings _decoder;

    internal Settings ( IEncoder_Settings packetsBuilder_Settings , IDecoder_Settings decoder_Settings , Func<byte[ ] , bool , OperateResult<byte[ ]>> methodOfCheckResponse )
    {
        //编码器
        _packetsBuilder = packetsBuilder_Settings;

        //解码器
        _decoder = decoder_Settings;

        //接收执行报文发送接收的委托方法        
        _methodOfCheckResponse = methodOfCheckResponse;       
    }

    /// <summary>
    /// 联机命令，在实例化本通讯类库后，必须先执行该方法方可解锁设备功能，否则无法发送命令
    /// </summary>
    /// <returns></returns>
    public OperateResult<byte[ ]> HandShake ( )
    {
        //执行命令
        var actionResult= CommandAction. Action ( _packetsBuilder. Packet_HandShake ( ) , _methodOfCheckResponse );

        //执行命令失败则返回执行结果
        if ( !actionResult.IsSuccess )
        {
            return actionResult;
        }

        //如果执行命令成功则解码
        var decodeResult = _decoder. DecodeHandShake ( actionResult. Content );

        //如果解码失败
        if ( !decodeResult.IsSuccess )
        {
            actionResult. Message = decodeResult. Message;
        }
        return actionResult;
    }
}
